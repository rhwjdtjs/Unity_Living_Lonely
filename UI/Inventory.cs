using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject go_InventoryBase; // �κ��丮 �⺻ ���� ������Ʈ
    [SerializeField] private GameObject go_SlotsParent; // ���� �θ� ���� ������Ʈ
    private GunMainController theGun; // GunMainController ��ũ��Ʈ ����
    public static bool inventoryActivated = false; // �κ��丮 Ȱ��ȭ ����
    private EffectItem itemDatabase; // EffectItem ��ũ��Ʈ ����
    private QuickSlot theQuickSlot; // QuickSlot ��ũ��Ʈ ����
    private Slot[] slots; // ���� �迭
    public Image crosshair; // ���ڼ� �̹���
    public Slot[] GetSlots() { return slots; } // ���� �迭 ��ȯ �Լ�
    [SerializeField] private Item[] items; // ������ �迭

    // �־��� �ε����� ������ �̸�, ������ ������ �������� �κ��丮�� ������ �ε�
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
        theGun = FindObjectOfType<GunMainController>(); // GunMainController ��ũ��Ʈ�� ã�� �Ҵ�
        Cursor.lockState = CursorLockMode.Locked; // Ŀ�� ���
        Cursor.visible = false; // Ŀ�� ������ �ʵ��� ����
        theQuickSlot = FindObjectOfType<QuickSlot>(); // QuickSlot ��ũ��Ʈ�� ã�� �Ҵ�
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); // ���� �迭 �ʱ�ȭ
        itemDatabase = FindObjectOfType<EffectItem>(); // EffectItem ��ũ��Ʈ�� ã�� �Ҵ�
    }

    void Update()
    {
        // �Ͻ� ���� ���°� �ƴϰ� �÷��̾ ���� �ʾ��� ���� �κ��丮 ���� ����
        if (!PauseScript.isPaused && !TotalGameManager.isPlayerDead)
        {
            TryOpenInventory(); // �κ��丮 ���� �õ�

            // �κ��丮�� Ȱ��ȭ�Ǿ� ���� �� Ŀ�� ǥ�� �� ��� ���� ����
            if (inventoryActivated)
            {
                crosshair.gameObject.SetActive(false); // ���ڼ� �̹��� ��Ȱ��ȭ
                Cursor.lockState = CursorLockMode.None; // Ŀ�� ��� ����
                Cursor.visible = true; // Ŀ�� ���̵��� ����
            }

            // �κ��丮�� ��Ȱ��ȭ�Ǿ� ���� �� Ŀ�� ǥ�� �� ��� ���� ����
            if (!inventoryActivated)
            {
                crosshair.gameObject.SetActive(true); // ���ڼ� �̹��� Ȱ��ȭ
                Cursor.lockState = CursorLockMode.Locked; // Ŀ�� ���
                Cursor.visible = false; // Ŀ�� ����
            }
        }
    }

    // �κ��丮 ���� �õ�
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated; // �κ��丮 Ȱ��ȭ ���� ���

            if (inventoryActivated)
                OpenInventory(); // �κ��丮 ����
            else
                CloseInventory(); // �κ��丮 �ݱ�
        }

        // �κ��丮�� �������� �� Escape Ű�� ������ �κ��丮 �ݱ�
        if (inventoryActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryActivated = false; // �κ��丮 ��Ȱ��ȭ
            CloseInventory(); // �κ��丮 �ݱ�
        }
    }

    // �κ��丮 ����
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true); // �κ��丮 �⺻ ���� ������Ʈ Ȱ��ȭ
        theQuickSlot.thebase.SetActive(true); // ������ �⺻ ���� ������Ʈ Ȱ��ȭ
    }

    // �κ��丮 �ݱ�
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false); // �κ��丮 �⺻ ���� ������Ʈ ��Ȱ��ȭ
        theQuickSlot.thebase.SetActive(false); // ������ �⺻ ���� ������Ʈ ��Ȱ��ȭ
        itemDatabase.HideToolTip(); // ���� �����
    }

    // ������ ȹ��
    public void GetItem(Item _item, int _count)
    {
        // ��� �������� �ƴ� ���
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count); // ���Կ� ������ ���� ����
                        return;
                    }
                }
            }
        }

        // �� ���Կ� ������ �߰�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count); // ���Կ� ������ �߰�
                return;
            }
        }
    }
}
