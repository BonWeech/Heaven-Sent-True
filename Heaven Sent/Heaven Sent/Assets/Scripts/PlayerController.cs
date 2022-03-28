using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
    public float horizontalInput;
    public float speed = 50f;
    public float jumpHeight = 50f;
    public float gravityModifier = 1f;
    public bool isOnGround = true;
    public float attackPush = 5f;

    // Start is called before the first frame update
    void Start()
    {
         playerRb2D = GetComponent<Rigidbody2D>();
         Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);


        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
           {
            playerRb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

            Debug.Log("The Jump line went off");
           } 

        if(transform.position.y > 21)
        {
            isOnGround = false;
        }

        if(transform.position.y < 21)
        {
            isOnGround = true;
        }
        
        if(Input.GetKeyDown(KeyCode.E))
           {
            playerRb2D.AddForce(Vector2.right * attackPush, ForceMode2D.Impulse);
            }

        if(Input.GetKeyDown(KeyCode.Q))
           {
            playerRb2D.AddForce(Vector2.left * attackPush, ForceMode2D.Impulse);
            }
    }
}
