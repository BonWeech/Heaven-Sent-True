using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject demon;
    private Vector2 spawnPos = new Vector2(35,43);
    private float startDelay = 2;
    private int repeatRate = 5;
    private float leftBoundary;
    


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDemon", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBoundary && gameObject.CompareTag("Demon"))
        {
            Destroy(gameObject);
        }
        

        
    }


    void SpawnDemon()
    {
        Instantiate(demon, spawnPos, demon.transform.rotation);
    }
}
