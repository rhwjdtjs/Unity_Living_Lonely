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
            if (invectoryActivated) //�κ��丮 Ȱ��ȭ ���϶�
            {
                Cursor.lockState = CursorLockMode.None; //Ŀ�� Ȱ��ȭ
                Cursor.visible = true;
            }
                
            if(!invectoryActivated) //�κ��丮 Ȱ��ȭ���� �ƴҶ�
            { 
                Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ�� ���
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

    public void GetItem(Item _item, int _count) //�������� ��� �Լ�
    {
        if (Item.ItemType.Equipment != _item.itemType) //�������� ��� Ÿ���� �ƴ϶��
        {
            for (int i = 0; i < slots.Length; i++) //���Ե��� Ȯ����
            {
                if (slots[i].item != null) 
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count); //���Կ� �������� �߰��Ѵ�
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count); //������ �߰�
                return;
            }
        }
    }
}
