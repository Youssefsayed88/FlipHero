using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Vector3 targetRotation; 
    public CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.gravity = new Vector3(0, -19.6f, 0);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        float moveDirection = Input.GetAxisRaw("Horizontal");
        rb.AddForce(moveDirection * Time.deltaTime * Vector3.back * speed, ForceMode.Impulse);
        #endregion

        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.gravity = Physics.gravity * -1;
            //transform.DORotate(new Vector3(180, 0, 0), 1f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Physics.gravity = Physics.gravity * -1;
            //transform.DORotate(new Vector3(0, 0, 0), 1f);
        }
    }
}
