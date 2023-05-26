using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] items; // 아이템 배열
    [SerializeField] private BoxCollider SpawnLocation; // 스폰 위치를 제한하는 박스 콜라이더
    [SerializeField] private GameObject[] bullets; // 총알 배열
    [SerializeField] private GameObject[] weapons; // 무기 배열

    void Start()
    {
        SpawnItems(); // 아이템 스폰 메서드 호출
    }

    private void SpawnItems()
    {
        List<int> itemIndices = new List<int>(); // 중복되지 않은 아이템 인덱스를 추적하기 위한 리스트
        List<int> BulletsIndices = new List<int>(); // 중복되지 않은 총알 인덱스를 추적하기 위한 리스트
        List<int> WeaponsIndices = new List<int>(); // 중복되지 않은 무기 인덱스를 추적하기 위한 리스트

        int itemCount = Random.Range(2, 4); // 2에서 3 사이의 아이템 개수
        itemCount = Mathf.Min(itemCount, items.Length); // 아이템 개수가 아이템 배열의 길이를 초과하지 않도록 제한

        int weaponcount = Random.Range(1, 3); // 1에서 2 사이의 무기 개수
        weaponcount = Mathf.Min(weaponcount, weapons.Length); // 무기 개수가 무기 배열의 길이를 초과하지 않도록 제한

        int bulletCount = Random.Range(2, 5); // 2에서 4 사이의 총알 개수
        bulletCount = Mathf.Min(bulletCount, bullets.Length); // 총알 개수가 총알 배열의 길이를 초과하지 않도록 제한

        // 총알 아이템 생성
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = BulletGetRandomItemIndex(BulletsIndices);
            BulletsIndices.Add(randomIndex);
            GameObject spawnedBullet = Instantiate(bullets[randomIndex], randomPosition, Quaternion.identity);
            spawnedBullet.transform.parent = transform;
        }

        // 무기 아이템 생성
        for (int i = 0; i < weaponcount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = weaponGetRandomItemIndex(WeaponsIndices);
            WeaponsIndices.Add(randomIndex);
            GameObject spawnweapon = Instantiate(weapons[randomIndex], randomPosition, Quaternion.identity);
            spawnweapon.transform.parent = transform;
        }

        // 나머지 아이템 생성
        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = GetRandomItemIndex(itemIndices);
            itemIndices.Add(randomIndex);

            Vector3 randomPosition = GetRandomPosition();
            GameObject spawnedItem = Instantiate(items[randomIndex], randomPosition, Quaternion.identity);
            spawnedItem.transform.parent = transform;
        }
    }
    // 아래 함수는 아이템의 무작위 인덱스를 생성 단, 이 인덱스는 excludedIndices 리스트에 포함되어 있지 않음
    // items 배열의 길이 범위 내에서 무작위 인덱스를 생성하고, 이미 excludedIndices 리스트에 포함되어 있는지 확인

    private int GetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, items.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, items.Length);
        }
        return randomIndex;
    }
    // 아래 함수는 총알에 대한 무작위 인덱스를 생성. 단, 이 인덱스는 excludedIndices 리스트에 포함되어 있지 않아야 함

    private int BulletGetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, bullets.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, bullets.Length);
        }
        return randomIndex;
    }
    //마찬가지
    private int weaponGetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, weapons.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, weapons.Length);
        }
        return randomIndex;
    }
    // 아래 함수는 무작위 위치를 생성하여 반환. SpawnLocation의 중심(center)과 크기(size)를 기반으로 무작위로 위치를 생성
    private Vector3 GetRandomPosition()
    {
        Vector3 center = SpawnLocation.bounds.center;
        Vector3 size = SpawnLocation.bounds.size;
        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);
        float z = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);
        return new Vector3(x, y, z);
    }
}
