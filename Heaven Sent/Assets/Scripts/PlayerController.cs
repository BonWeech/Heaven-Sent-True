using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
    private BoxCollider2D playerBox2D;
    public CircleCollider2D playerCircle2D;
    public CapsuleCollider2D playerCapsule2D;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip kickSound;
    private AudioSource playerAudio;
    public AudioClip punchSound;
    public GameObject deathPose;
    private Vector2 startPos;
    public float horizontalInput;
    public float speed = 50f;
    public float jumpHeight = 10f;
    public float gravityModifier = 1f;
    public bool gameOver = false;
    public bool isOnGround = true;
    private Vector2 heavenPos = new Vector2((float) -27.8, (float) 493.3);





    // Start is called before the first frame update
    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerBox2D = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();
        playerCapsule2D = GetComponent<CapsuleCollider2D>();
        playerCircle2D = GetComponent<CircleCollider2D>();
        playerCapsule2D.enabled = true;
        playerCircle2D.enabled = true;
        playerBox2D.enabled = true;
        playerAnim.SetBool("IsWalking", false);
        playerAnim.SetBool("IsJumping", false);
        playerAnim.SetBool("IsDead", false);
        startPos = transform.position;
        deathPose.SetActive(false);




    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
        }




        if (Input.GetKeyUp(KeyCode.W) && !gameOver && isOnGround)
        {
            playerRb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            
        }

        else
        {
           
        }

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !playerAnim.GetBool("IsJumping"))
        {
            playerAnim.SetTrigger("Attack_trig");
            playerAudio.PlayOneShot(punchSound, 1.0f);
            playerCapsule2D.enabled = false;

        }

        else
        {
            playerCapsule2D.enabled = true;
        }

        if (horizontalInput != 0)
        {
            playerAnim.SetBool("IsWalking", true);
        }

        else
        {
            playerAnim.SetBool("IsWalking", false);
        }


        if (transform.position.y > 93)
        {
            transform.position = new Vector2(transform.position.x, 93);
        }
        if (transform.position.x <= -45)
        {
            transform.position = new Vector2(-45, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerAnim.GetBool("IsJumping"))

        {

            playerAnim.SetTrigger("Kick_trig");
            playerAudio.PlayOneShot(kickSound, 1.0f);
        }

        else
        {
          
        }


        if (playerAnim.GetBool("IsDead"))
        {
            
            playerAnim.SetBool("IsWalking", false);
            playerAnim.SetBool("IsJumping", false);
            playerCapsule2D.enabled = false;
            playerBox2D.enabled = false;
            deathPose.SetActive(true);
        }

        if (gameOver == true)
        {
            Debug.Log("Game Over!");
            Destroy(playerAudio);
        }



    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        while (other.gameObject.CompareTag("Ground"))
        {
            playerAnim.SetBool("IsJumping", false);
            isOnGround = true;
            playerBox2D.enabled = true;
            playerCapsule2D.enabled = true;
        }

        if (other.gameObject.CompareTag("Fork"))
        {
            playerAnim.SetBool("IsDead", true);
            
        }

        if (other.gameObject.CompareTag("DOOR"))
        {
            Debug.Log("Door Detected");
            playerAnim.SetBool("IsDead", false);
            transform.position = heavenPos;
        }

        if (other.gameObject.CompareTag("Wing"))
        {
            Debug.Log("Wing Detected");
            playerAnim.SetBool("IsDead", true);
            
        }

        if (deathPose)
        {
            Physics2D.IgnoreLayerCollision(0, 6);
            gameOver = true;
        }
    

    }
}
