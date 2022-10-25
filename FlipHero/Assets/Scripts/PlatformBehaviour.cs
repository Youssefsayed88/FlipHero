using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float platformEdge;

    bool rightFlipped = false;
    bool leftFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        if (transform.position.x <= platformEdge)
        {
            transform.position = new Vector3(200, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!rightFlipped)
            {
                transform.DORotate(new Vector3(-180, 0, 0), 1f);
                rightFlipped = !rightFlipped;
                Debug.Log("right");
            }
            else if(rightFlipped)
            {
                transform.DORotate(new Vector3(0, 0, 0), 1f);
                rightFlipped = !rightFlipped;
                leftFlipped = false;
                Debug.Log("right again");
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!leftFlipped)
            {
                transform.DORotate(new Vector3(180, 0, 0), 1f);
                leftFlipped = !leftFlipped;
                Debug.Log("left");
            }
            else if(leftFlipped)
            {
                transform.DORotate(new Vector3(360, 0, 0), 1f);
                leftFlipped = !leftFlipped;
                rightFlipped = false;
                Debug.Log("left again");
            }
        }
    }
}
