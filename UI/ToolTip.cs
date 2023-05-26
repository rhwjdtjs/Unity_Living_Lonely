using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private GameObject go_Base;       // ToolTip�� �⺻ GameObject
    [SerializeField] private Text txt_ItemName;        // ������ �̸��� ǥ���ϴ� Text
    [SerializeField] private Text txt_ItemDesc;        // ������ ������ ǥ���ϴ� Text
    [SerializeField] private Text txt_ItemHowtoUsed;   // ������ ��� ����� ǥ���ϴ� Text

    // ToolTip�� �����ִ� �Լ�
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);    // ToolTip�� Ȱ��ȭ

        // ToolTip�� ��ġ ����
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 0.5f,
                            0);
        go_Base.transform.position = _pos;

        // ������ ���� ����
        txt_ItemName.text = _item.itemName;    // ������ �̸� ����
        txt_ItemDesc.text = _item.itemDesc;    // ������ ���� ����

        // ������ Ÿ�Կ� ���� ��� ��� ����
        if (_item.itemType == Item.ItemType.Equipment)
            txt_ItemHowtoUsed.text = "RMB-Equip";   // ��� �������̸� "RMB-Equip"���� ǥ��
        else if (_item.itemType == Item.ItemType.Used)
            txt_ItemHowtoUsed.text = "RMB-Eat";     // �Һ� �������̸� "RMB-Eat"���� ǥ��
        else
            txt_ItemHowtoUsed.text = "";            // �� ���� ���� �� ���ڿ��� ����
    }

    // ToolTip�� ����� �Լ�
    public void HideToolTip()
    {
        go_Base.SetActive(false);   // ToolTip�� ��Ȱ��ȭ
    }
}
