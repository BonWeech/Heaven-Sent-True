using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;
    private Vector2 spawnPos;
    private float startDelay = 1f;
    private int repeatRate = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        spawnPos = new Vector2(Player.transform.position.x + 100f, Player.transform.position.y + 1f);

        if(Player.transform.position.x > 123)
        {
            repeatRate = 3;
        }

        if (Player.transform.position.x > 304.5)
        {
            repeatRate = 2;
        }

        if (Player.transform.position.x > 486.8)
        {
            repeatRate = 1;
        }

        if (Player.transform.position.x > 670.8)
        {
            CancelInvoke();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(Enemy, spawnPos, Enemy.transform.rotation);
    }

    
    }
