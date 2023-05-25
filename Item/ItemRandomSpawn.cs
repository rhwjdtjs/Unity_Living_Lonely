using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private BoxCollider SpawnLocation;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private GameObject[] weapons;
    void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        List<int> itemIndices = new List<int>();
        List<int> BulletsIndices = new List<int>();
        List<int> WeaponsIndices = new List<int>();
        int itemCount = Random.Range(2, 4); // 3에서 5 사이의 아이템 개수
        itemCount = Mathf.Min(itemCount, items.Length); // 아이템 개수가 아이템 배열의 길이를 초과하지 않도록 제한
        int weaponcount = Random.Range(1, 3); // 3에서 5 사이의 아이템 개수
        weaponcount = Mathf.Min(weaponcount, weapons.Length); // 아이템 개수가 아이템 배열의 길이를 초과하지 않도록 제한
        // bullets 아이템 생성
        int bulletCount = Random.Range(2, 5); // 1에서 2 사이의 총알 개수
        bulletCount = Mathf.Min(bulletCount, bullets.Length); // 총알 개수가 총알 배열의 길이를 초과하지 않도록 제한

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = BulletGetRandomItemIndex(BulletsIndices);
            BulletsIndices.Add(randomIndex);
            GameObject spawnedBullet = Instantiate(bullets[randomIndex], randomPosition, Quaternion.identity);
            spawnedBullet.transform.parent = transform;
        }
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

    private int GetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, items.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, items.Length);
        }
        return randomIndex;
    }
    private int BulletGetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, bullets.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, bullets.Length);
        }
        return randomIndex;
    }
    private int weaponGetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, weapons.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, weapons.Length);
        }
        return randomIndex;
    }

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
