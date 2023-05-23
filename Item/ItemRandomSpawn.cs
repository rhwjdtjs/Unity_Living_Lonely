using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private BoxCollider SpawnLocation;
    [SerializeField] private GameObject[] bullets;
    void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        int itemCount = Random.Range(3, 6); // 3���� 5 ������ ������ ����
        itemCount = Mathf.Min(itemCount, items.Length); // ������ ������ ������ �迭�� ���̸� �ʰ����� �ʵ��� ����

        List<int> itemIndices = new List<int>();
        List<int> BulletsIndices = new List<int>();

        // bullets ������ ����
        int bulletCount = Random.Range(1, 3); // 1���� 2 ������ �Ѿ� ����
        bulletCount = Mathf.Min(bulletCount, bullets.Length); // �Ѿ� ������ �Ѿ� �迭�� ���̸� �ʰ����� �ʵ��� ����

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = BulletGetRandomItemIndex(BulletsIndices);
            BulletsIndices.Add(randomIndex);
            GameObject spawnedBullet = Instantiate(bullets[randomIndex], randomPosition, Quaternion.identity);
            spawnedBullet.transform.parent = transform;
        }

        // ������ ������ ����
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