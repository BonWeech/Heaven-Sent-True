using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
    private BoxCollider2D playerBox2D;
    public float horizontalInput;
    public float speed = 50f;
    public float jumpHeight = 10f;
    public float gravityModifier = 1f;
    public bool gameOver = false;
    public bool isOnGround = true;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip kickSound;
    private AudioSource playerAudio;
    public AudioClip punchSound;
    public GameObject punchPose;
    public GameObject kickPose;
    public GameObject BlueBarry;
    public GameObject SpawnManager;
    public GameObject DOOR;
    public GameObject Demon;
    public GameObject Body;
    public GameObject Angel;
    public GameObject deathPose;
    public BoxCollider2D demonBox2D;
    private Vector2 startPos;
    public PhysicsScene2D heavenLevel;




    // Start is called before the first frame update
    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerBox2D = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim.SetBool("IsWalking", false);
        playerAnim.SetBool("IsJumping", false);
        playerAnim.SetBool("IsDead", false);
        punchPose.SetActive(false);
        kickPose.SetActive(false);
        Body.SetActive(true);
        startPos = transform.position;
        playerBox2D.enabled = true;
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
            Body.SetActive(false);

        }

        else
        {
            Body.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !playerAnim.GetBool("IsJumping"))
        {
            playerAnim.SetTrigger("Attack_trig");
            playerAudio.PlayOneShot(punchSound, 1.0f);
            punchPose.SetActive(true);
            Body.SetActive(false);
        }

        else
        {
            punchPose.SetActive(false);
            Body.SetActive(true);
        }

        if (horizontalInput != 0)
        {
            playerAnim.SetBool("IsWalking", true);
        }

        else
        {
            playerAnim.SetBool("IsWalking", false);
        }

        if (transform.position.y > 43.2)
        {
            playerAnim.SetBool("IsJumping", true);
            playerAnim.SetBool("IsWalking", false);
            isOnGround = false;
            playerBox2D.enabled = false;
        }

        else
        {

            playerAnim.SetBool("IsJumping", false);
            isOnGround = true;
            playerBox2D.enabled = true;
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
            kickPose.SetActive(true);
            Body.SetActive(false);
            playerBox2D.enabled = false;
        }

        else
        {
            kickPose.SetActive(false);
            Body.SetActive(true);
        }


        if (playerAnim.GetBool("IsDead"))
        {
            
            playerAnim.SetBool("IsWalking", false);
            playerAnim.SetBool("IsJumping", false);
            punchPose.SetActive(false);
            kickPose.SetActive(false);
            Body.SetActive(false);
            playerBox2D.enabled = false;
            deathPose.SetActive(true);
            
        }

        if (gameOver == true)
        {
            Debug.Log("Game Over!");
            Destroy(SpawnManager);
            Destroy(playerAudio);



        }



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fork"))
        {
            playerAnim.SetBool("IsDead", true);
            
        }

        if (other.gameObject.CompareTag("DOOR"))
        {
            SceneManager.LoadScene("HeavenLevel");
            Debug.Log("Door Detected");
            playerAnim.SetBool("IsDead", false);
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
