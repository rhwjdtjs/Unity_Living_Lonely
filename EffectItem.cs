using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class iEffect
{
    public string itemName;  //아이템의 이름
    public string[] part;  // 효과.
    public int[] num;  // 얼마나 상승하냐
}

public class EffectItem : MonoBehaviour
{
    [SerializeField]private QuickSlot theQuick; //컴포넌트 불러오기
    [SerializeField]private ToolTip theToolTip;//컴포넌트 불러오기
    [SerializeField] private iEffect[] Effects; //컴포넌트 불러오기
    [SerializeField]private StatusControllor theStatus; //컴포넌트 불러오기
    [SerializeField] private WeaponManager theWeaponManager; //컴포넌트 불러오기
    private const string HP = "HP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY";
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
            theToolTip.ShowToolTip(_item, _pos);
    }
    public void HideToolTip()
    {
            theToolTip.HideToolTip();
    }
    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment) //아이템을 사용하면 불러오는 함수 만약 그 아이템이 무기라면
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO(_item.weaponType, _item.itemName)); //무기교체 시작
        if (_item.itemType == Item.ItemType.Used) //소모 아이템이라면
        {
            for (int i = 0; i < Effects.Length; i++) //배열안에서 찾는다
            {
                if (Effects[i].itemName == _item.itemName) //배열안에 있는 아이템 이름과 비교하려는 아이템의 이름과 같다면
                {
                    for (int j = 0; j < Effects[i].part.Length; j++) //효과명을 찾음
                    {
                        switch (Effects[i].part[j]) //만약 그 효과명이
                        {
                            case HP: //체력이라면 체력을 증가시킴
                                theStatus.IncreaseHP(Effects[i].num[j]);
                                break;
                            case THIRSTY:
                                theStatus.IncreaseThirsty(Effects[i].num[j]);
                                break;
                            case HUNGRY:
                                theStatus.IncreaseHungry(Effects[i].num[j]);
                                break;
                            default:
                                break;
                        }
                    }
                    return;
                }
            }
        }
    }
    public void ActivateQuick(int _num)
    {
        theQuick.Activatequick(_num);
    }
}
