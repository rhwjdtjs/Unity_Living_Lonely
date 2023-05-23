using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuickSlot : MonoBehaviour
{
    [SerializeField] private EffectItem theEffectItem;
    [SerializeField] Image activeimage1;
    [SerializeField] public Slot[] quickSlots;  // Äü½½·Ôµé 
    [SerializeField] private Transform tf_parent;  // Äü½½·ÔµéÀÇ ºÎ¸ð ¿ÀºêÁ§Æ®
    [SerializeField] public GameObject thebase;
    [SerializeField] private GameObject go_SelectedImage;  // ¼±ÅÃµÈ Äü½½·Ô ÀÌ¹ÌÁö
    [SerializeField] private GameObject activeimage;
    [SerializeField] private WeaponManager theWeaponManager;
    //[SerializeField]private Image activefoodimage;
    private int selectedSlot;  // ¼±ÅÃµÈ Äü½½·ÔÀÇ ÀÎµ¦½º 
    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>();
        selectedSlot = 0;
    }

    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeSlot(5);
    }
    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);
        Execute();
    }
    private void SelectedSlot(int _num)
    {
        selectedSlot = _num;
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }
    public void EatItem()
    {
        theEffectItem.UseItem(quickSlots[selectedSlot].item);
        quickSlots[selectedSlot].SetSlotCount(-1);
    }
    private void Execute()
    {
        if (quickSlots[selectedSlot].item != null)
        {
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Equipment)
            {
                StartCoroutine(theWeaponManager.CHANGEWEAPONCO(quickSlots[selectedSlot].item.weaponType, quickSlots[selectedSlot].item.itemName));
                activeimage.SetActive(true);
                activeimage1.sprite = quickSlots[selectedSlot].item.itemImage;
            }
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
            {
                    EatItem();
            }
            else
            {
                StartCoroutine(theWeaponManager.CHANGEWEAPONCO("HAND", "Hand"));
                activeimage.SetActive(false);
            }
        }
        
        else
        {
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO("HAND", "Hand"));
            activeimage.SetActive(false);
        }
    }
    public void Activatequick(int _num)
    {
        if (selectedSlot == _num)
        {
            Execute();
            return;
        }
        if (SlotUtil.instance != null)
        {
            if (SlotUtil.instance.slotUtil != null)
            {
                if (SlotUtil.instance.slotUtil.GetQuickSlotNumber() == selectedSlot)
                {
                    Execute();
                    return;
                }
            }
        }
    }
}
