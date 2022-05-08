using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehavior : MonoBehaviour
{
    public GameObject Fork;
    public GameObject Body;
    public float speed;
    private Animator demonAnim;
    public BoxCollider2D demonBox2D;
    public AudioSource demonSnort;
    public AudioSource demonDeath;
    public AudioClip DeathSound;








    // Start is called before the first frame update
    void Start()
    {
        
       
        demonAnim = GetComponent<Animator>();
        demonAnim.SetBool("IsDead", false);
        
}

    // Update is called once per frame
    void Update()
    {
        
        if(!demonAnim.GetBool("IsDead"))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        

        if(transform.position.y < 20)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < 43)
        {
            transform.position = new Vector2(transform.position.x, 43);
        }

        if(demonAnim.GetBool("IsDead"))
        {
            Destroy(demonBox2D);
            Destroy(Fork);
            Destroy(Body);
            Destroy(demonSnort);
            
        }
        

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BarryAttack")
        {
            demonAnim.SetBool("IsDead", true);
            demonDeath.PlayOneShot(DeathSound, 1f);
            Debug.Log("hit");
        }
        



    }

}



