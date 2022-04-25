using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Demon;
    public GameObject Player;
    private Vector2 spawnPos;
    private float startDelay = 1f;
    private int repeatRate = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {


        
        InvokeRepeating("SpawnDemon", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        spawnPos = new Vector2(Player.transform.position.x + 100f, 43.60306f);

        if(Player.transform.position.x > 123)
        {
            repeatRate = 4;
        }

        if (Player.transform.position.x > 304.5)
        {
            repeatRate = 3;
        }

        if (Player.transform.position.x > 486.8)
        {
            repeatRate = 2;
        }

        if (Player.transform.position.x > 670.8)
        {
            CancelInvoke();
        }
    }

    void SpawnDemon()
    {
        Instantiate(Demon, spawnPos, Demon.transform.rotation);
    }

    
    }