using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class iEffect
{
    public string itemName;  // �������� �̸� (Item name)
    public string[] part;  // ȿ�� (Effects)
    public int[] num;  // �󸶳� ����ϳ� (Amount of increase)
}

public class EffectItem : MonoBehaviour
{
    [SerializeField] private QuickSlot theQuick; // QuickSlot ������Ʈ (QuickSlot component)
    [SerializeField] private ToolTip theToolTip; // ToolTip ������Ʈ (ToolTip component)
    [SerializeField] private iEffect[] Effects; // iEffect �迭 (Array of iEffect)
    [SerializeField] private StatusControllor theStatus; // StatusControllor ������Ʈ (StatusControllor component)
    [SerializeField] private WeaponManager theWeaponManager; // WeaponManager ������Ʈ (WeaponManager component)
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
        if (_item.itemType == Item.ItemType.Equipment) // �������� ����� ���
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO(_item.weaponType, _item.itemName)); // ���⸦ �����ϴ� �ڷ�ƾ ����

        if (_item.itemType == Item.ItemType.Used) // �������� ��� �������� ���
        {
            for (int i = 0; i < Effects.Length; i++)
            {
                if (Effects[i].itemName == _item.itemName) // �������� �̸��� ��ġ�ϴ� ���
                {
                    for (int j = 0; j < Effects[i].part.Length; j++)
                    {
                        switch (Effects[i].part[j])
                        {
                            case HP: // HP�� ������ �� ���
                                theStatus.IncreaseHP(Effects[i].num[j]); // HP�� ������Ŵ
                                break;
                            case THIRSTY: // THIRSTY�� ������ �� ���
                                theStatus.IncreaseThirsty(Effects[i].num[j]); // THIRSTY�� ������Ŵ
                                break;
                            case HUNGRY: // HUNGRY�� ������ �� ���
                                theStatus.IncreaseHungry(Effects[i].num[j]); // HUNGRY�� ������Ŵ
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
