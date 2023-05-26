using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private EffectItem theEffectItem; // EffectItem 스크립트를 참조하는 변수
    [SerializeField] Image activeimage1; // 퀵슬롯에 표시되는 아이템 이미지를 표시하기 위한 Image 컴포넌트 변수
    [SerializeField] public Slot[] quickSlots; // 퀵슬롯들을 담기 위한 Slot 배열
    [SerializeField] private Transform tf_parent; // 퀵슬롯들의 부모 오브젝트를 참조하는 변수
    [SerializeField] public GameObject thebase; // 퀵슬롯의 기본 설정을 담고 있는 게임 오브젝트
    [SerializeField] private GameObject go_SelectedImage; // 선택된 퀵슬롯을 표시하는 이미지 게임 오브젝트
    [SerializeField] private GameObject activeimage; // 퀵슬롯에 아이템이 있는지 여부를 표시하기 위한 이미지 게임 오브젝트
    [SerializeField] private WeaponManager theWeaponManager; // WeaponManager 스크립트를 참조하는 변수

    private int selectedSlot; // 선택된 퀵슬롯의 인덱스

    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>(); // 자식 오브젝트인 Slot 컴포넌트를 모두 찾아 배열에 저장
        selectedSlot = 0; // 초기 선택된 퀵슬롯 인덱스를 0으로 설정
    }

    void Update()
    {
        TryInputNumber(); // 숫자 입력을 감지하여 퀵슬롯을 변경하는 메서드 호출
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 키보드 숫자 1을 누르면
            ChangeSlot(0); // 인덱스 0에 해당하는 퀵슬롯으로 변경
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 키보드 숫자 2를 누르면
            ChangeSlot(1); // 인덱스 1에 해당하는 퀵슬롯으로 변경
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // 키보드 숫자 3을 누르면
            ChangeSlot(2); // 인덱스 2에 해당하는 퀵슬롯으로 변경
        else if (Input.GetKeyDown(KeyCode.Alpha4)) // 키보드 숫자 4를 누르면
            ChangeSlot(3); // 인덱스 3에 해당하는 퀵슬롯으로 변경
        else if (Input.GetKeyDown(KeyCode.Alpha5)) // 키보드 숫자 5를 누르면
            ChangeSlot(4); // 인덱스 4에 해당하는 퀵슬롯으로 변경
        else if (Input.GetKeyDown(KeyCode.Alpha6)) // 키보드 숫자 6을 누르면
            ChangeSlot(5); // 인덱스 5에 해당하는 퀵슬롯으로 변경
    }

    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num); // 선택된 퀵슬롯을 변경하는 메서드 호출
        Execute(); // 선택된 퀵슬롯에 대한 동작 실행
    }

    private void SelectedSlot(int _num)
    {
        selectedSlot = _num; // 선택된 퀵슬롯 인덱스를 전달받은 인덱스로 변경
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position; // 선택된 퀵슬롯 이미지의 위치를 변경된 퀵슬롯 위치로 이동
    }

    public void EatItem()
    {
        theEffectItem.UseItem(quickSlots[selectedSlot].item); // 선택된 퀵슬롯의 아이템을 사용하여 효과 적용
        quickSlots[selectedSlot].SetSlotCount(-1); // 선택된 퀵슬롯의 아이템 개수를 감소시킴
    }

    private void Execute()
    {
        if (quickSlots[selectedSlot].item != null) // 선택된 퀵슬롯에 아이템이 있는 경우
        {
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Equipment) // 아이템이 장비인 경우
            {
                StartCoroutine(theWeaponManager.CHANGEWEAPONCO(quickSlots[selectedSlot].item.weaponType, quickSlots[selectedSlot].item.itemName)); // 무기 변경 코루틴 실행
                activeimage.SetActive(true); // 퀵슬롯에 아이템이 있는지 표시하는 이미지 활성화
                activeimage1.sprite = quickSlots[selectedSlot].item.itemImage; // 퀵슬롯에 아이템 이미지 표시
            }
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used) // 아이템이 소모품인 경우
            {
                EatItem(); // 아이템 사용 및 개수 감소
            }
            else // 그 외의 경우 (아이템이 기타인 경우)
            {
                StartCoroutine(theWeaponManager.CHANGEWEAPONCO("HAND", "Hand")); // 기본 무기로 변경하는 코루틴 실행
                activeimage.SetActive(false); // 퀵슬롯에 아이템이 있는지 표시하는 이미지 비활성화
            }
        }
        else // 선택된 퀵슬롯에 아이템이 없는 경우
        {
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO("HAND", "Hand")); // 기본 무기로 변경하는 코루틴 실행
            activeimage.SetActive(false); // 퀵슬롯에 아이템이 있는지 표시하는 이미지 비활성화
        }
    }

    public void Activatequick(int _num)
    {
        if (selectedSlot == _num) // 이미 선택된 퀵슬롯을 다시 선택한 경우
        {
            Execute(); // 동작 실행
            return;
        }
        if (SlotUtil.instance != null)
        {
            if (SlotUtil.instance.slotUtil != null)
            {
                if (SlotUtil.instance.slotUtil.GetQuickSlotNumber() == selectedSlot) // 이미 선택된 퀵슬롯을 다시 선택한 경우
                {
                    Execute(); // 동작 실행
                    return;
                }
            }
        }
    }
}
