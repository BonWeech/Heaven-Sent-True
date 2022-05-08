using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject SpawnManager;
    private Rigidbody2D playerRb2D;
    public GameObject Body;
    public GameObject Attack;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip kickSound;
    public AudioClip deathSound;
    private AudioSource playerAudio;
    public AudioClip punchSound;
    public BoxCollider2D playerBox2D;
    public Camera PlayerCam;
    public GameObject deathPose;
    public GameObject DeathScreen;
    public float horizontalInput;
    public float speed;
    public float jumpHeight;
    public bool gameOver;
    private bool isOnGround;
    private float Timer;
    public float Seconds;
    public string Heaven;



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
        Timer = Seconds;
        DeathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //movement key binding
        if (!gameOver)
        {
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
                JumpCycle();
            }
            if (transform.position.y >= 44)
            {
                isOnGround = false;
                playerAnim.SetBool("IsJumping", true);
            }
            if (transform.position.y <= 44)
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

            //Attacks
            if (Input.GetKey(KeyCode.Space) && Timer > 0)
            {
                Attack.SetActive(true);
                Timer -= Time.deltaTime;
                Physics2D.IgnoreLayerCollision(8, 8);
            }

            else
            {
                Attack.SetActive(false);
                Timer = Seconds;
            }

            if (Input.GetKeyDown(KeyCode.Space) && playerAnim.GetBool("IsJumping"))
            {
                KickCycle();
            }
            if (Input.GetKeyDown(KeyCode.Space) && !playerAnim.GetBool("IsJumping"))
            {
                PunchCycle();
            }
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Death Animation to Demon
        if (other.gameObject.CompareTag("Fork") && !gameOver)
        {
            GameOver();
        }
        //Death to Angels
        if (other.gameObject.CompareTag("Wing") && !gameOver)
        {
            GameOver();
        }
        //Teleport
        if (other.gameObject.CompareTag("DOOR") && !gameOver)
        {
            Debug.Log("Door Detected");
            playerAnim.SetBool("IsDead", false);
            UnityEngine.SceneManagement.SceneManager.LoadScene(Heaven);
        }
    }

    //Punching
    private void PunchCycle()
    {
        playerAudio.PlayOneShot(punchSound, 1.0f);
        playerAnim.SetTrigger("Attack_trig");
    }

    //Kicking
    private void KickCycle()
    {
        playerAudio.PlayOneShot(kickSound, 1.0f);
        playerAnim.SetTrigger("Kick_trig");
    }

    //Jump
    private void JumpCycle()
    {
        playerRb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    //Game Over
    private void GameOver()
    {
        Debug.Log("Game Over!");
        playerAudio.PlayOneShot(deathSound);
        gameOver = true;
        Destroy(SpawnManager);
        Destroy(playerBox2D);
        playerAnim.SetBool("IsDead", true);
        playerAnim.SetBool("IsWalking", false);
        playerAnim.SetBool("IsJumping", false);
        Attack.SetActive(false);
        Body.SetActive(false);
        deathPose.SetActive(true);
        Physics2D.IgnoreLayerCollision(0, 6);
        DeathScreen.SetActive(true);
    }
}
