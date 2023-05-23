using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnobject;
    public BoxCollider spawncollider;
    public Action<int> OnSpawnEvent;
    [SerializeField]
    private GameObject zombie1;
    [SerializeField]
    private GameObject zombie2;
    [SerializeField]
    private GameObject zombie3;
    [SerializeField]
    private GameObject zombie4;
    [SerializeField]
    private GameObject zombie5;
    [SerializeField]
    private GameObject zombie6;
    [SerializeField]
    int _monsterCount = 0;
    [SerializeField]
    private GameObject strongmonster;
    private bool isspawn = false;
  
    public static int _reserveCount = 0;
    [SerializeField]
    int _keepMonsterCount = 0;
    [SerializeField]
    float _spawnTime = 5.0f;
    private int randomint = 0;
    public void AddMonsterCount(int value) { _monsterCount += value; }
    private void Start()
    {
        spawncollider = spawnobject.GetComponent<BoxCollider>();
        OnSpawnEvent -= AddMonsterCount;
        OnSpawnEvent += AddMonsterCount;
        
    }
    void Update()
    {
        
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine(RandomRespawn_Coroutine());
            
        }
        if(isspawn==false)
        {
            strongmonsterins();
        }
    }

    Vector3 RandPos()
    {
        Vector3 originPosition = spawnobject.transform.position;
        float range_X = spawncollider.bounds.size.x;
        float range_Z = spawncollider.bounds.size.z;

        range_X = UnityEngine.Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = UnityEngine.Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 randomOffset = new Vector3(range_X, 1f, range_Z);

        Vector3 respawnPosition = originPosition + randomOffset;

        // 콜라이더 내에서 스폰 위치를 조정
        respawnPosition.x = Mathf.Clamp(respawnPosition.x, spawncollider.bounds.min.x, spawncollider.bounds.max.x);
        respawnPosition.z = Mathf.Clamp(respawnPosition.z, spawncollider.bounds.min.z, spawncollider.bounds.max.z);

        return respawnPosition;
    }
    private void strongmonsterins()
    {
        if(TotalGameManager.survivaltimesecond>=611)
        {
            for(int i=0; i<15; i++)
            {
                Instantiate(strongmonster, RandPos(),Quaternion.identity);
            }
            isspawn = true;
        }
    }


    IEnumerator RandomRespawn_Coroutine()
    {
            _reserveCount++;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, _spawnTime));
        if (OnSpawnEvent != null)
            OnSpawnEvent.Invoke(1);
        randomint = UnityEngine.Random.Range(0, 6);
        switch(randomint)
        {
            case 0:
                Instantiate(zombie1, RandPos(), Quaternion.identity);
                break;
            case 1:
                Instantiate(zombie2, RandPos(), Quaternion.identity);
                break;
            case 2:
                Instantiate(zombie3, RandPos(), Quaternion.identity);
                break;
            case 3:
                Instantiate(zombie3, RandPos(), Quaternion.identity);
                break;
            case 4:
                Instantiate(zombie4, RandPos(), Quaternion.identity);
                break;
            case 5:
                Instantiate(zombie5, RandPos(), Quaternion.identity);
                break;
        }
       
           // Debug.Log("Spawn"+ _reserveCount);
            _reserveCount--;
    }
}
