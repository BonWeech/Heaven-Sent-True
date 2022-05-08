using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenSpawn : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;
    private Vector2 spawnPos;
    private float startDelay = 1f;
    private int repeatRate;
    private float heightRange;
   


    // Start is called before the first frame update
    void Start()
    {
        repeatRate = Random.Range(1, 5);

        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        spawnPos = new Vector2(Player.transform.position.x + 100f, heightRange);

        if (Player.transform.position.x > 123)
        {
            repeatRate = Random.Range(1, 4);
        }

        if (Player.transform.position.x > 304.5)
        {
            repeatRate = Random.Range(1, 3);
        }

        if (Player.transform.position.x > 486.8)
        {
            repeatRate = Random.Range(1, 2); ;
        }

        if (Player.transform.position.x > 670.8)
        {
            CancelInvoke();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(Enemy, spawnPos, Enemy.transform.rotation);
        heightRange = Random.Range(44, 60);
    }


}
