using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Make Item", menuName = "Make Item/New")]
public class Item : ScriptableObject 
{
    public enum ItemType  // ������ ����
    {
        Equipment,
        Used,
        Ingredient,
        ammo
    }
    public string itemName; // �������� �̸�
    public ItemType itemType; // ������ ����
    public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���)
    public GameObject itemPrefab;  // �������� ������
    [TextArea] public string itemDesc; // �������� ����
    public string weaponType;  // ���� ����
}
