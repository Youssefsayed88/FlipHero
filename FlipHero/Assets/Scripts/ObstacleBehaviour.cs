using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    float ObstacleSpeed;

    [SerializeField] float platformEdge;
    [SerializeField] float damageAmount = 5f;

    bool stopMoving = false;

    private void Awake()
    {
        PlayerBehaviour.onPlayerDeath += StopMoving;
        ObstacleSpeed = GameManager.Instance.playerSpeed;
    }

    private void Start()
    {
        GameManager.onSpeedChange += ChangeGameSpeed;
    }

    void Update()
    {
        if (!stopMoving)
        {
            transform.Translate(Vector3.left * ObstacleSpeed * Time.deltaTime);
        }

        if (transform.position.x <= platformEdge)
        {
            Destroy(gameObject);
        }
    }

    void ChangeGameSpeed(float newSpeed)
    {
        ObstacleSpeed = GameManager.Instance.playerSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehaviour>().TakeDamage(damageAmount);
        }
    }

    void StopMoving()
    {
        stopMoving = true;
    }
}
