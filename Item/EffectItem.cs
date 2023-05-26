using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class iEffect
{
    public string itemName;  // 아이템의 이름 (Item name)
    public string[] part;  // 효과 (Effects)
    public int[] num;  // 얼마나 상승하냐 (Amount of increase)
}

public class EffectItem : MonoBehaviour
{
    [SerializeField] private QuickSlot theQuick; // QuickSlot 컴포넌트 (QuickSlot component)
    [SerializeField] private ToolTip theToolTip; // ToolTip 컴포넌트 (ToolTip component)
    [SerializeField] private iEffect[] Effects; // iEffect 배열 (Array of iEffect)
    [SerializeField] private StatusControllor theStatus; // StatusControllor 컴포넌트 (StatusControllor component)
    [SerializeField] private WeaponManager theWeaponManager; // WeaponManager 컴포넌트 (WeaponManager component)
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
        if (_item.itemType == Item.ItemType.Equipment) // 아이템이 장비인 경우
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO(_item.weaponType, _item.itemName)); // 무기를 변경하는 코루틴 시작

        if (_item.itemType == Item.ItemType.Used) // 아이템이 사용 아이템인 경우
        {
            for (int i = 0; i < Effects.Length; i++)
            {
                if (Effects[i].itemName == _item.itemName) // 아이템의 이름이 일치하는 경우
                {
                    for (int j = 0; j < Effects[i].part.Length; j++)
                    {
                        switch (Effects[i].part[j])
                        {
                            case HP: // HP에 영향을 줄 경우
                                theStatus.IncreaseHP(Effects[i].num[j]); // HP를 증가시킴
                                break;
                            case THIRSTY: // THIRSTY에 영향을 줄 경우
                                theStatus.IncreaseThirsty(Effects[i].num[j]); // THIRSTY를 증가시킴
                                break;
                            case HUNGRY: // HUNGRY에 영향을 줄 경우
                                theStatus.IncreaseHungry(Effects[i].num[j]); // HUNGRY를 증가시킴
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
