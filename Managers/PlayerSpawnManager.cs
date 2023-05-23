using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
   
    [SerializeField] private GameObject theplayer;
    [SerializeField] private Transform[] playerspawnpoint;
    void Start()
    {
        RandomSelectSpawnPoint();
    }
    public void RandomSelectSpawnPoint()
    {
        int number = Random.Range(0, playerspawnpoint.Length);
        theplayer.transform.position = playerspawnpoint[number].transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
