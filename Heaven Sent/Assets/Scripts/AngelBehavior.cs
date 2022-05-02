using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBehavior : MonoBehaviour
{
    public GameObject Wing;
    public GameObject AngelBody;
    public float AngelSpeed;
    private Animator angelAnim;
    public BoxCollider2D angelBox2D;








    // Start is called before the first frame update
    void Start()
    {


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
            Destroy(angelBox2D);
            Destroy(Wing);
            Destroy(AngelBody);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            angelAnim.SetBool("IsDead", true);
            Debug.Log("hit");
        }



    }

}
