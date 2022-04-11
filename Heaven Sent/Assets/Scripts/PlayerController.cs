using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb2D;
    private CapsuleCollider2D playerCapsule2D;
    private BoxCollider2D playerBox2D;
    public float horizontalInput;
    public float speed = 50f;
    public float jumpHeight = 10f;
    public float gravityModifier = 1f;
    public bool gameOver = false;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip kickSound;
    private AudioSource playerAudio;
    public AudioClip punchSound;
    public GameObject punchPose;
    public GameObject kickPose;



    // Start is called before the first frame update
    void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerCapsule2D = GetComponent<CapsuleCollider2D>();
        playerBox2D = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        playerAnim.SetBool("IsWalking", false);
        playerAnim.SetBool("IsJumping", false);
        playerAnim.SetBool("IsDead", false);
        punchPose.SetActive(false);
        kickPose.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);


        if (Input.GetKeyUp(KeyCode.W) && !gameOver)
        {
            playerRb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);

        }

        else
        {
            
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && !gameOver && !playerAnim.GetBool("IsJumping"))
        {
        playerAnim.SetTrigger("Attack_trig");
        playerAudio.PlayOneShot(punchSound, 1.0f);
        punchPose.SetActive(true);
        }

        else
        {
            punchPose.SetActive(false);
        }

        if(horizontalInput != 0)
        {
        playerAnim.SetBool("IsWalking", true);
        }

        else
        {
        playerAnim.SetBool("IsWalking", false);
        }

        if(transform.position.y > 43.7)
        {
            playerAnim.SetBool("IsJumping", true);
            playerAnim.SetBool("IsWalking", false);
        }

        else
        {
           
            playerAnim.SetBool("IsJumping", false);
        }

        if(transform.position.y > 93)
        {
             transform.position = new Vector2(transform.position.x, 93);
        }
        if(transform.position.x <= -50)
        {
            Destroy(gameObject);
           
            gameOver = true;
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerAnim.GetBool("IsJumping"))
        
        {
            
            playerAnim.SetTrigger("Kick_trig");
            playerAudio.PlayOneShot(kickSound, 1.0f);
            kickPose.SetActive(true);
        }

        else
        {
            
            kickPose.SetActive(false);
        }


        if(playerAnim.GetBool("IsDead"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            Destroy(playerCapsule2D);
            Destroy(punchPose);
            Destroy(kickPose);
        }

            

    }

    private void OnCollisionEnter2D(Collision2D collision2D)
        {


        if (collision2D.gameObject.CompareTag("Fork"))
        {
            Debug.Log("Fork Detected");
            playerAnim.SetBool("IsDead", true);
            
}
    
        }

}
