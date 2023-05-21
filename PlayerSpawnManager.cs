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
    public void RandomSelectSpawnPoint() //플레이어를 특정 위치중에 램덤으로 스폰시킴
    {
        int number = Random.Range(0, playerspawnpoint.Length);
        theplayer.transform.position = playerspawnpoint[number].transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
