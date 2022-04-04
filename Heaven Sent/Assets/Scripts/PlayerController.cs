using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
    public float horizontalInput;
    public float speed = 50f;
    public float jumpHeight = 10f;
    public float gravityModifier = 1f;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
         playerRb2D = GetComponent<Rigidbody2D>();
         playerAnim = GetComponent<Animator>();
         Physics.gravity *= gravityModifier;
         playerAnim.SetBool("IsIdle", true);
         playerAnim.SetBool("IsWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);


        if(Input.GetKeyUp(KeyCode.Space) && isOnGround && !gameOver)
           {
            playerRb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
           } 
        
        if(Input.GetKeyDown(KeyCode.E) && !gameOver)
        {
        playerAnim.SetTrigger("Attack_trig");
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
        playerAnim.SetBool("IsWalking", true);
        Debug.Log("input arrow");
        }

        else
        {
            playerAnim.SetBool("IsWalking", false);
        }

        if(transform.position.y > 44)
        {
            isOnGround = false;
        }

        else
        {
            isOnGround = true;
        }

        if(transform.position.y > 61)
        {
             transform.position = new Vector2(transform.position.x, 61);
        }
        if(transform.position.x <= -32)
        {
            Destroy(gameObject);
            Debug.Log("Game Over!");
            gameOver = true;
            
        }

/*
        
        if(Input.GetKeyDown(KeyCode.E))
           {
            playerRb2D.AddForce(Vector2.right * attackPush, ForceMode2D.Impulse);
            }

        if(Input.GetKeyDown(KeyCode.Q))
           {
            playerRb2D.AddForce(Vector2.left * attackPush, ForceMode2D.Impulse);
            }
        */
    }
}
