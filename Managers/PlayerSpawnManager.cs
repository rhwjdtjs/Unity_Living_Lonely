using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject theplayer; // �÷��̾� ���� ������Ʈ�� ������ ����
    [SerializeField] private Transform[] playerspawnpoint; // �÷��̾ ������ �� �ִ� ���� ������ �����ϴ� �迭

    void Start()
    {
        RandomSelectSpawnPoint(); // �������� ���� ������ �����Ͽ� �÷��̾ �����Ѵ�.
    }

    public void RandomSelectSpawnPoint()
    {
        int number = Random.Range(0, playerspawnpoint.Length); // ���� ���� �迭���� �������� �ε����� �����Ѵ�.
        theplayer.transform.position = playerspawnpoint[number].transform.position; // ���õ� ���� ������ ��ġ�� �÷��̾ �̵���Ų��.
    }

    
}
