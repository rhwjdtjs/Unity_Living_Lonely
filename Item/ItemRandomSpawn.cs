using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] items; // ������ �迭
    [SerializeField] private BoxCollider SpawnLocation; // ���� ��ġ�� �����ϴ� �ڽ� �ݶ��̴�
    [SerializeField] private GameObject[] bullets; // �Ѿ� �迭
    [SerializeField] private GameObject[] weapons; // ���� �迭

    void Start()
    {
        SpawnItems(); // ������ ���� �޼��� ȣ��
    }

    private void SpawnItems()
    {
        List<int> itemIndices = new List<int>(); // �ߺ����� ���� ������ �ε����� �����ϱ� ���� ����Ʈ
        List<int> BulletsIndices = new List<int>(); // �ߺ����� ���� �Ѿ� �ε����� �����ϱ� ���� ����Ʈ
        List<int> WeaponsIndices = new List<int>(); // �ߺ����� ���� ���� �ε����� �����ϱ� ���� ����Ʈ

        int itemCount = Random.Range(2, 4); // 2���� 3 ������ ������ ����
        itemCount = Mathf.Min(itemCount, items.Length); // ������ ������ ������ �迭�� ���̸� �ʰ����� �ʵ��� ����

        int weaponcount = Random.Range(1, 3); // 1���� 2 ������ ���� ����
        weaponcount = Mathf.Min(weaponcount, weapons.Length); // ���� ������ ���� �迭�� ���̸� �ʰ����� �ʵ��� ����

        int bulletCount = Random.Range(2, 5); // 2���� 4 ������ �Ѿ� ����
        bulletCount = Mathf.Min(bulletCount, bullets.Length); // �Ѿ� ������ �Ѿ� �迭�� ���̸� �ʰ����� �ʵ��� ����

        // �Ѿ� ������ ����
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = BulletGetRandomItemIndex(BulletsIndices);
            BulletsIndices.Add(randomIndex);
            GameObject spawnedBullet = Instantiate(bullets[randomIndex], randomPosition, Quaternion.identity);
            spawnedBullet.transform.parent = transform;
        }

        // ���� ������ ����
        for (int i = 0; i < weaponcount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = weaponGetRandomItemIndex(WeaponsIndices);
            WeaponsIndices.Add(randomIndex);
            GameObject spawnweapon = Instantiate(weapons[randomIndex], randomPosition, Quaternion.identity);
            spawnweapon.transform.parent = transform;
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
    // �Ʒ� �Լ��� �������� ������ �ε����� ���� ��, �� �ε����� excludedIndices ����Ʈ�� ���ԵǾ� ���� ����
    // items �迭�� ���� ���� ������ ������ �ε����� �����ϰ�, �̹� excludedIndices ����Ʈ�� ���ԵǾ� �ִ��� Ȯ��

    private int GetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, items.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, items.Length);
        }
        return randomIndex;
    }
    // �Ʒ� �Լ��� �Ѿ˿� ���� ������ �ε����� ����. ��, �� �ε����� excludedIndices ����Ʈ�� ���ԵǾ� ���� �ʾƾ� ��

    private int BulletGetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, bullets.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, bullets.Length);
        }
        return randomIndex;
    }
    //��������
    private int weaponGetRandomItemIndex(List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, weapons.Length);
        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, weapons.Length);
        }
        return randomIndex;
    }
    // �Ʒ� �Լ��� ������ ��ġ�� �����Ͽ� ��ȯ. SpawnLocation�� �߽�(center)�� ũ��(size)�� ������� �������� ��ġ�� ����
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
