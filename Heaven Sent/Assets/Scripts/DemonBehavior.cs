using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehavior : MonoBehaviour
{
    public GameObject Fork;
    public float speed;
    private int leftBound = -50;
    private Animator demonAnim;
    public CapsuleCollider2D demonCapsule2D;
    public float timeLeft = 5f;
    public BoxCollider2D demonBox2D;
   
    


    // Start is called before the first frame update
    void Start()
    {
        demonCapsule2D.GetComponent<CapsuleCollider2D>();
        demonAnim = GetComponent<Animator>();
        demonAnim.SetBool("IsDead", false);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if(!demonAnim.GetBool("IsDead"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        
        


        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
        
         


            }


           private void OnCollisionEnter2D(Collision2D collision2D)
            {
            

            if (collision2D.gameObject.CompareTag("Player"))
            {
                demonAnim.SetBool("IsDead", true);
                Destroy(demonBox2D);
                Destroy(Fork);
                Destroy(demonCapsule2D);
            }
            
        
    }

}



