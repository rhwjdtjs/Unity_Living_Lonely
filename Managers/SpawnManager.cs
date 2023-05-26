using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnobject; // 스폰 위치를 나타내는 게임 오브젝트
    public BoxCollider spawncollider; // 스폰 영역을 나타내는 박스 콜라이더
    public Action<int> OnSpawnEvent; // 몬스터 스폰 이벤트를 처리하는 액션
    [SerializeField]
    private GameObject zombie1; // 좀비 1 프리팹
    [SerializeField]
    private GameObject zombie2; // 좀비 2 프리팹
    [SerializeField]
    private GameObject zombie3; // 좀비 3 프리팹
    [SerializeField]
    private GameObject zombie4; // 좀비 4 프리팹
    [SerializeField]
    private GameObject zombie5; // 좀비 5 프리팹
    [SerializeField]
    private GameObject zombie6; // 좀비 6 프리팹
    [SerializeField]
    int _monsterCount = 0; // 현재 스폰된 몬스터 수
    [SerializeField]
    private GameObject strongmonster; // 강력한 몬스터 프리팹
    private bool isspawn = false; // 강력한 몬스터가 스폰되었는지 여부
    public static int _reserveCount = 0; // 예약된 몬스터 수
    [SerializeField]
    int _keepMonsterCount = 0; // 유지할 몬스터 수
    [SerializeField]
    float _spawnTime = 5.0f; // 스폰 간격
    private int randomint = 0; // 랜덤한 정수값
    public void AddMonsterCount(int value) { _monsterCount += value; } // 몬스터 수를 증가시키는 함수

    private void Start()
    {
        spawncollider = spawnobject.GetComponent<BoxCollider>();
        OnSpawnEvent -= AddMonsterCount;
        OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        // 예약된 몬스터와 현재 스폰된 몬스터 수를 비교하여 유지할 몬스터 수를 유지한다.
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine(RandomRespawn_Coroutine());
        }

        // 강력한 몬스터가 스폰되지 않았다면 스폰한다.
        if (isspawn == false)
        {
            strongmonsterins();
        }
    }

    Vector3 RandPos()
    {
        Vector3 originPosition = spawnobject.transform.position;
        float range_X = spawncollider.bounds.size.x;
        float range_Z = spawncollider.bounds.size.z;

        // 스폰 영역 내에서 랜덤한 위치를 생성한다.
        range_X = UnityEngine.Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = UnityEngine.Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 randomOffset = new Vector3(range_X, 1f, range_Z);

        Vector3 respawnPosition = originPosition + randomOffset;

        // 콜라이더 내에서 스폰 위치를 조정한다.
        respawnPosition.x = Mathf.Clamp(respawnPosition.x, spawncollider.bounds.min.x, spawncollider.bounds.max.x);
        respawnPosition.z = Mathf.Clamp(respawnPosition.z, spawncollider.bounds.min.z, spawncollider.bounds.max.z);

        return respawnPosition;
    }

    private void strongmonsterins()
    {
        if (TotalGameManager.survivaltimesecond >= 611) // 생존 시간이 611 이상일 때
        {
            for (int i = 0; i < 15; i++) // 15마리의 강력한 몬스터를 스폰
            {
                Instantiate(strongmonster, RandPos(), Quaternion.identity);
            }
            isspawn = true; // 강력한 몬스터가 스폰되었음을 표시
        }
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        _reserveCount++; // 예약된 몬스터 수 증가
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, _spawnTime)); // 임의의 대기 시간 후에 스폰

        if (OnSpawnEvent != null)
            OnSpawnEvent.Invoke(1); // 몬스터 스폰 이벤트 호출

        randomint = UnityEngine.Random.Range(0, 6); // 0부터 5까지의 랜덤한 정수 생성
        switch (randomint)
        {
            case 0:
                Instantiate(zombie1, RandPos(), Quaternion.identity); // 좀비 1 스폰
                break;
            case 1:
                Instantiate(zombie2, RandPos(), Quaternion.identity); // 좀비 2 스폰
                break;
            case 2:
                Instantiate(zombie3, RandPos(), Quaternion.identity); // 좀비 3 스폰
                break;
            case 3:
                Instantiate(zombie3, RandPos(), Quaternion.identity); // 좀비 3 스폰
                break;
            case 4:
                Instantiate(zombie4, RandPos(), Quaternion.identity); // 좀비 4 스폰
                break;
            case 5:
                Instantiate(zombie5, RandPos(), Quaternion.identity); // 좀비 5 스폰
                break;
        }

        _reserveCount--; // 예약된 몬스터 수 감소
    }
}
