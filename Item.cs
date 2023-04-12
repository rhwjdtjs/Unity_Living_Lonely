using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Make Item", menuName = "Make Item/New")]
public class Item : ScriptableObject 
{
    public enum ItemType  // 아이템 유형
    {
        Equipment,
        Used,
        Ingredient,
        ammo
    }
    public string itemName; // 아이템의 이름
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템의 이미지(인벤 토리 안에서 띄울)
    public GameObject itemPrefab;  // 아이템의 프리팹
    [TextArea] public string itemDesc; // 아이템의 설명
    public string weaponType;  // 무기 유형
}
