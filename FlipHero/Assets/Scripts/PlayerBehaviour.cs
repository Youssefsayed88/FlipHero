using UnityEngine;
using DG.Tweening;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] float speed;

    [SerializeField] float health;

    [SerializeField] float maxEndurance = 10f;
    float endurance = 10f; //counter before damaging he player

    public static event Action<float> playerTookDamage;
    public static event Action<float> onEnduranceChange;
    public static event Action onPlayerDeath;

    float changeCooldown = 2f;
    float canChangeGravity;

    public Animator animator;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            #region Movement
            float moveDirection = Input.GetAxisRaw("Horizontal");
            rb.AddForce(moveDirection * Time.deltaTime * Vector3.back * -Physics.gravity.y * speed, ForceMode.Impulse);
            ClampMovement();
            #endregion

            #region rotation

            if (Input.GetKeyDown(KeyCode.E) && Time.time >= canChangeGravity)
            {
                canChangeGravity = Time.time + changeCooldown;
                transform.DOBlendableRotateBy(new Vector3(-180, 0, 0), 1f);
                ChangeGravity();
            }
            if (Input.GetKeyDown(KeyCode.Q) && Time.time >= canChangeGravity)
            {
                canChangeGravity = Time.time + changeCooldown;
                transform.DOBlendableRotateBy(new Vector3(180, 0, 0), 1f);
                ChangeGravity();
            }
            #endregion

            #region Endurance
            if (isGrounded())
            {
                endurance -= Time.deltaTime;
            }
            else { if (endurance < maxEndurance) { endurance += Time.deltaTime; } }
            onEnduranceChange.Invoke(endurance);
            if (endurance < 0)
            {
                TakeDamage(25f);
                endurance += 2f;
                onEnduranceChange.Invoke(endurance);
            }
            #endregion
        }
        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            rb.velocity = Vector3.zero;
            onPlayerDeath.Invoke(); 
        }
    }
    public void TakeDamage(float damage)
    {
        playerTookDamage.Invoke(damage);
        health -= damage;
    }

    void ClampMovement()
    {
        if (transform.position.y <= -15f || transform.position.y >= 15f 
            || transform.position.z <= -15f || transform.position.z >= 15f) { rb.velocity = -transform.position; TakeDamage(10f);}
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.9f);
    }

    void ChangeGravity()
    {
        Physics.gravity = -1 * Physics.gravity;
        rb.velocity = Vector3.zero;
    }
}
