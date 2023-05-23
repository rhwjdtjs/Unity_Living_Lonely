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
    public Slot[] GetSlots() { return slots; }
    [SerializeField] private Item[] items;
    public void LoadToInven(int _arrayNum,string _itemname, int _itemnum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemname)
                slots[_arrayNum].AddItem(items[i], _itemnum);
        }
    }

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
            if (invectoryActivated)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
                
            if(!invectoryActivated)
            { 
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        

    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
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

    public void GetItem(Item _item, int _count)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null) 
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
