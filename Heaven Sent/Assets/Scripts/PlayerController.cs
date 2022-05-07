using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
   
    public GameObject Body;
    public GameObject Attack;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip kickSound;
    private AudioSource playerAudio;
    public AudioClip punchSound;
    public GameObject deathPose;
    public float horizontalInput;
    public float speed;
    public float jumpHeight;
    public float gravityModifier;
    public bool gameOver;
    public bool isOnGround;
    private Vector2 heavenPos;
    public BoxCollider2D playerBox2D;
    public float animationTime;




    // Start is called before the first frame update
    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerBox2D = GetComponent<BoxCollider2D>();
        playerAnim.SetBool("IsWalking", false);
        playerAnim.SetBool("IsJumping", false);
        playerAnim.SetBool("IsDead", false);
        Attack.SetActive(true);
        Body.SetActive(true);
        isOnGround = true;
        gameOver = false;
        gravityModifier = 1f;
        jumpHeight = 10f;
        speed = 50f;
        heavenPos = new Vector2((float)-27.8, (float)493.3);
        animationTime = 2f;
        
       




    }

    // Update is called once per frame
    void Update()
    {


        //movement key binding
        if (!gameOver)
        {

            Attack.SetActive(false);
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);

            //Player Limits
            if (transform.position.y > 93)
            {
                transform.position = new Vector2(transform.position.x, 93);
            }
            if (transform.position.x <= -45)
            {
                transform.position = new Vector2(-45, transform.position.y);
            }
            if (Input.GetKeyUp(KeyCode.W) && isOnGround)
            {
                playerRb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
            if(transform.position.y >= 45)
            {
                isOnGround = false;
                playerAnim.SetBool("IsJumping", true);
            }
            if (transform.position.y <= 45)
            {
                isOnGround = true;
                playerAnim.SetBool("IsJumping", false);
            }

            //Walk Animation
            if (horizontalInput != 0 && isOnGround)
            {
                playerAnim.SetBool("IsWalking", true);
            }
            else
            {
                playerAnim.SetBool("IsWalking", false);
            }

        }
        
        //Punch Attack
        if (Input.GetKey(KeyCode.Space) && !playerAnim.GetBool("IsJumping"))
        {
            playerAnim.SetTrigger("Attack_trig");
            Attack.SetActive(true);
        }
        else
        {
            Attack.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !playerAnim.GetBool("IsJumping"))
        {
            playerAudio.PlayOneShot(punchSound, 1.0f);
        }
        //Kick Attack
        if (Input.GetKey(KeyCode.Space) && playerAnim.GetBool("IsJumping"))
        {
            playerAnim.SetTrigger("Kick_trig");
            Attack.SetActive(true);
        }
        else
        {
            Attack.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerAnim.GetBool("IsJumping"))
        {
            playerAudio.PlayOneShot(kickSound, 1.0f);
        }

        //Game Over
        if (gameOver == true)
        {
            Debug.Log("Game Over!");
            Destroy(playerAudio);
            Destroy(playerBox2D);
            playerAnim.SetBool("IsDead", true);
            playerAnim.SetBool("IsWalking", false);
            playerAnim.SetBool("IsJumping", false);
            Attack.SetActive(false);
            Body.SetActive(false);
            deathPose.SetActive(true);
            Physics2D.IgnoreLayerCollision(0, 6);
            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Death Animation to Demon
        if (other.gameObject.CompareTag("Fork"))
        {
            gameOver = true;
        }
        //Teleport
        if (other.gameObject.CompareTag("DOOR"))
        {
            Debug.Log("Door Detected");
            playerAnim.SetBool("IsDead", false);
            transform.position = heavenPos;
        }
        //Death to Angels
        if (other.gameObject.CompareTag("Wing"))
        {
            gameOver = true;
        }
    }
}
