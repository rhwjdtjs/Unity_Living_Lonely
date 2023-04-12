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
        if (_item.itemType == Item.ItemType.Equipment)
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO(_item.weaponType, _item.itemName));
        if (_item.itemType == Item.ItemType.Used)
        {
            for (int i = 0; i < Effects.Length; i++)
            {
                if (Effects[i].itemName == _item.itemName)
                {
                    for (int j = 0; j < Effects[i].part.Length; j++)
                    {
                        switch (Effects[i].part[j])
                        {
                            case HP:
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
