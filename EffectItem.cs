using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class iEffect
{
    public string itemName;  //�������� �̸�
    public string[] part;  // ȿ��.
    public int[] num;  // �󸶳� ����ϳ�
}

public class EffectItem : MonoBehaviour
{
    [SerializeField]private QuickSlot theQuick; //������Ʈ �ҷ�����
    [SerializeField]private ToolTip theToolTip;//������Ʈ �ҷ�����
    [SerializeField] private iEffect[] Effects; //������Ʈ �ҷ�����
    [SerializeField]private StatusControllor theStatus; //������Ʈ �ҷ�����
    [SerializeField] private WeaponManager theWeaponManager; //������Ʈ �ҷ�����
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
        if (_item.itemType == Item.ItemType.Equipment) //�������� ����ϸ� �ҷ����� �Լ� ���� �� �������� ������
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO(_item.weaponType, _item.itemName)); //���ⱳü ����
        if (_item.itemType == Item.ItemType.Used) //�Ҹ� �������̶��
        {
            for (int i = 0; i < Effects.Length; i++) //�迭�ȿ��� ã�´�
            {
                if (Effects[i].itemName == _item.itemName) //�迭�ȿ� �ִ� ������ �̸��� ���Ϸ��� �������� �̸��� ���ٸ�
                {
                    for (int j = 0; j < Effects[i].part.Length; j++) //ȿ������ ã��
                    {
                        switch (Effects[i].part[j]) //���� �� ȿ������
                        {
                            case HP: //ü���̶�� ü���� ������Ŵ
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
