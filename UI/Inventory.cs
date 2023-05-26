using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject go_InventoryBase; // 인벤토리 기본 게임 오브젝트
    [SerializeField] private GameObject go_SlotsParent; // 슬롯 부모 게임 오브젝트
    private GunMainController theGun; // GunMainController 스크립트 참조
    public static bool inventoryActivated = false; // 인벤토리 활성화 여부
    private EffectItem itemDatabase; // EffectItem 스크립트 참조
    private QuickSlot theQuickSlot; // QuickSlot 스크립트 참조
    private Slot[] slots; // 슬롯 배열
    public Image crosshair; // 십자선 이미지
    public Slot[] GetSlots() { return slots; } // 슬롯 배열 반환 함수
    [SerializeField] private Item[] items; // 아이템 배열

    // 주어진 인덱스와 아이템 이름, 아이템 개수를 바탕으로 인벤토리에 아이템 로드
    public void LoadToInventory(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
                slots[_arrayNum].AddItem(items[i], _itemNum);
        }
    }

    void Start()
    {
        theGun = FindObjectOfType<GunMainController>(); // GunMainController 스크립트를 찾아 할당
        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
        Cursor.visible = false; // 커서 보이지 않도록 설정
        theQuickSlot = FindObjectOfType<QuickSlot>(); // QuickSlot 스크립트를 찾아 할당
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); // 슬롯 배열 초기화
        itemDatabase = FindObjectOfType<EffectItem>(); // EffectItem 스크립트를 찾아 할당
    }

    void Update()
    {
        // 일시 정지 상태가 아니고 플레이어가 죽지 않았을 때만 인벤토리 동작 수행
        if (!PauseScript.isPaused && !TotalGameManager.isPlayerDead)
        {
            TryOpenInventory(); // 인벤토리 열기 시도

            // 인벤토리가 활성화되어 있을 때 커서 표시 및 잠금 상태 조절
            if (inventoryActivated)
            {
                crosshair.gameObject.SetActive(false); // 십자선 이미지 비활성화
                Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
                Cursor.visible = true; // 커서 보이도록 설정
            }

            // 인벤토리가 비활성화되어 있을 때 커서 표시 및 잠금 상태 조절
            if (!inventoryActivated)
            {
                crosshair.gameObject.SetActive(true); // 십자선 이미지 활성화
                Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
                Cursor.visible = false; // 커서 숨김
            }
        }
    }

    // 인벤토리 열기 시도
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated; // 인벤토리 활성화 여부 토글

            if (inventoryActivated)
                OpenInventory(); // 인벤토리 열기
            else
                CloseInventory(); // 인벤토리 닫기
        }

        // 인벤토리가 열려있을 때 Escape 키를 누르면 인벤토리 닫기
        if (inventoryActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryActivated = false; // 인벤토리 비활성화
            CloseInventory(); // 인벤토리 닫기
        }
    }

    // 인벤토리 열기
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true); // 인벤토리 기본 게임 오브젝트 활성화
        theQuickSlot.thebase.SetActive(true); // 퀵슬롯 기본 게임 오브젝트 활성화
    }

    // 인벤토리 닫기
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false); // 인벤토리 기본 게임 오브젝트 비활성화
        theQuickSlot.thebase.SetActive(false); // 퀵슬롯 기본 게임 오브젝트 비활성화
        itemDatabase.HideToolTip(); // 툴팁 숨기기
    }

    // 아이템 획득
    public void GetItem(Item _item, int _count)
    {
        // 장비 아이템이 아닌 경우
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count); // 슬롯에 아이템 개수 설정
                        return;
                    }
                }
            }
        }

        // 빈 슬롯에 아이템 추가
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count); // 슬롯에 아이템 추가
                return;
            }
        }
    }
}
