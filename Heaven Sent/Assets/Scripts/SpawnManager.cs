using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject demon;
    private Vector2 spawnPos = new Vector2(110,46);
    private float startDelay = 2f;
    private int repeatRate = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDemon", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    { 

    }

    void SpawnDemon()
    {
        Instantiate(demon, spawnPos, demon.transform.rotation);
    }
}
