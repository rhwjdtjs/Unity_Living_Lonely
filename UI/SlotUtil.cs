using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUtil : MonoBehaviour
{
    [SerializeField] private Image imageItem; // 드래그한 아이템 이미지를 표시하기 위한 Image 컴포넌트

    static public SlotUtil instance; // SlotUtil 클래스의 인스턴스를 저장하기 위한 정적 변수
    public Slot slotUtil; // 현재 활성화된 슬롯을 저장하기 위한 변수

    void Start()
    {
        instance = this; // 인스턴스에 자기 자신을 저장하여 싱글톤 패턴을 구현
    }

    // 드래그한 아이템의 이미지를 설정하는 메서드
    public void SetImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite; // 전달받은 아이템 이미지로 이미지 설정
        SetColor(1); // 투명도를 1로 설정하여 이미지를 보이도록 함
    }

    // 이미지의 투명도를 조절하는 메서드
    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha; // 전달받은 투명도 값으로 이미지의 알파값을 조절
        imageItem.color = color;
    }
}
