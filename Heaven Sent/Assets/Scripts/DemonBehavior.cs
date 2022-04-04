using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehavior : MonoBehaviour
{

    public float speed;
    private int leftBound = -50;
    public BoxCollider2D fork;
    public BoxCollider2D back;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);


        if(transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }

    }
}



