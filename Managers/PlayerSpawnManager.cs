using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject theplayer; // 플레이어 게임 오브젝트를 참조할 변수
    [SerializeField] private Transform[] playerspawnpoint; // 플레이어가 생성될 수 있는 스폰 지점을 저장하는 배열

    void Start()
    {
        RandomSelectSpawnPoint(); // 무작위로 스폰 지점을 선택하여 플레이어를 생성한다.
    }

    public void RandomSelectSpawnPoint()
    {
        int number = Random.Range(0, playerspawnpoint.Length); // 스폰 지점 배열에서 무작위로 인덱스를 선택한다.
        theplayer.transform.position = playerspawnpoint[number].transform.position; // 선택된 스폰 지점의 위치로 플레이어를 이동시킨다.
    }

    
}
