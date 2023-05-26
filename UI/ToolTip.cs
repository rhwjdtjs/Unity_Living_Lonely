using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private GameObject go_Base;       // ToolTip의 기본 GameObject
    [SerializeField] private Text txt_ItemName;        // 아이템 이름을 표시하는 Text
    [SerializeField] private Text txt_ItemDesc;        // 아이템 설명을 표시하는 Text
    [SerializeField] private Text txt_ItemHowtoUsed;   // 아이템 사용 방법을 표시하는 Text

    // ToolTip을 보여주는 함수
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);    // ToolTip을 활성화

        // ToolTip의 위치 설정
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 0.5f,
                            0);
        go_Base.transform.position = _pos;

        // 아이템 정보 설정
        txt_ItemName.text = _item.itemName;    // 아이템 이름 설정
        txt_ItemDesc.text = _item.itemDesc;    // 아이템 설명 설정

        // 아이템 타입에 따라 사용 방법 설정
        if (_item.itemType == Item.ItemType.Equipment)
            txt_ItemHowtoUsed.text = "RMB-Equip";   // 장비 아이템이면 "RMB-Equip"으로 표시
        else if (_item.itemType == Item.ItemType.Used)
            txt_ItemHowtoUsed.text = "RMB-Eat";     // 소비 아이템이면 "RMB-Eat"으로 표시
        else
            txt_ItemHowtoUsed.text = "";            // 그 외의 경우는 빈 문자열로 설정
    }

    // ToolTip을 숨기는 함수
    public void HideToolTip()
    {
        go_Base.SetActive(false);   // ToolTip을 비활성화
    }
}
