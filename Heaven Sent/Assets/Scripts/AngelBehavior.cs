using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBehavior : MonoBehaviour
{
    public GameObject Wing;
    public GameObject AngelBody;
    public float AngelSpeed;
    private Animator angelAnim;
    public Rigidbody2D angelRb2D;
    public AudioSource angelDeath;
    public AudioClip DeathSound;







    // Start is called before the first frame update
    void Start()
    {

        angelRb2D = GetComponent<Rigidbody2D>();
        angelAnim = GetComponent<Animator>();
        angelAnim.SetBool("IsDead", false);

    }

    // Update is called once per frame
    void Update()
    {

        if (!angelAnim.GetBool("IsDead"))
        {
            transform.Translate(Vector2.left * Time.deltaTime * AngelSpeed);
        }



        if (transform.position.y < 20)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }

        if (angelAnim.GetBool("IsDead"))
        {
            Destroy(Wing);
            Destroy(AngelBody);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BarryAttack")
        {
            angelAnim.SetBool("IsDead", true);
            angelRb2D.constraints = RigidbodyConstraints2D.None;
            angelDeath.PlayOneShot(DeathSound, 1f);
            Debug.Log("hit");
        }



    }

}
