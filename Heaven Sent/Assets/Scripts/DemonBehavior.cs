using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehavior : MonoBehaviour
{
    public GameObject Fork;
    public float speed;
    private Animator demonAnim;
    public CapsuleCollider2D demonCapsule2D;
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
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        

        if(transform.position.y < 20)
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



