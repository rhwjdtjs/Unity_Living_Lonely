using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject go_InventoryBase;
    [SerializeField] private GameObject go_SlotsParent;
    private GunMainController thegun;
    public static bool invectoryActivated = false;
    private EffectItem itemdatabase;
    private QuickSlot thequick;
    private Slot[] slots;  

    void Start()
    {
        thegun = FindObjectOfType<GunMainController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        thequick = FindObjectOfType<QuickSlot>();
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        itemdatabase = FindObjectOfType<EffectItem>();
    }

    void Update()
    {
        if (!TotalGameManager.isPlayerDead)
        {
            TryOpenInventory();
            if (invectoryActivated) //인벤토리 활성화 중일때
            {
                Cursor.lockState = CursorLockMode.None; //커서 활성화
                Cursor.visible = true;
            }
                
            if(!invectoryActivated) //인벤토리 활성화중이 아닐때
            { 
                Cursor.lockState = CursorLockMode.Locked; //마우스 커서 잠금
                Cursor.visible = false;
            }
        }
        

    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invectoryActivated = !invectoryActivated;
            if (invectoryActivated)
                OpenInventory();
            else
                CloseInventory();

        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        thequick.thebase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        thequick.thebase.SetActive(false);
        itemdatabase.HideToolTip();
    }

    public void GetItem(Item _item, int _count) //아이템을 얻는 함수
    {
        if (Item.ItemType.Equipment != _item.itemType) //아이템이 장비 타입이 아니라면
        {
            for (int i = 0; i < slots.Length; i++) //슬롯들을 확인후
            {
                if (slots[i].item != null) 
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count); //슬롯에 아이템을 추가한다
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count); //아이템 추가
                return;
            }
        }
    }
}
