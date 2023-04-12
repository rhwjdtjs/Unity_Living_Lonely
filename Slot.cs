using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private bool isQuickSlot;  // �ش� ������ ���������� ���� �Ǵ�
    [SerializeField] private int quickSlotNumber;  // ������ �ѹ�
    [SerializeField] private Text text_Count;
    [SerializeField] private GameObject go_CountImage;
    [SerializeField] RectTransform quickSlotBaseRect; // �������� ����. ������ ������ ���Ե��� ���� �����ϴ� 'Content' ������Ʈ�� �Ҵ� ��.
    [SerializeField] private RectTransform baseRect;
    private InputUI theInputNumber;
    public Item item; // ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage;  // �������� �̹���
    public Image activeimage;
    public GameObject activeimagebase;
    private WeaponManager theWeaponManager;
    private EffectItem theEffectItem;
    private void Start()
    {
        theInputNumber = FindObjectOfType<InputUI>();
        theWeaponManager = FindObjectOfType<WeaponManager>();
        theEffectItem = FindObjectOfType<EffectItem>();
    }
    public int GetQuickSlotNumber()
    {
        return quickSlotNumber;
    }
    // ������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    // �ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    // ����
                    StartCoroutine(theWeaponManager.CHANGEWEAPONCO(item.weaponType, item.itemName));
                    activeimagebase.SetActive(true);
                    activeimage.sprite = item.itemImage;
                }
                if (item != null)
                {
                    theEffectItem.UseItem(item);

                    if (item.itemType == Item.ItemType.Used)
                        SetSlotCount(-1);
                }
                if(!(item.itemType==Item.ItemType.Equipment))
                {
                    activeimagebase.SetActive(false);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            SlotUtil.instance.slotUtil = this;
            SlotUtil.instance.SetImage(itemImage);
            SlotUtil.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            SlotUtil.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!((SlotUtil.instance.transform.localPosition.x > baseRect.rect.xMin
           && SlotUtil.instance.transform.localPosition.x < baseRect.rect.xMax
           && SlotUtil.instance.transform.localPosition.y > baseRect.rect.yMin
           && SlotUtil.instance.transform.localPosition.y < baseRect.rect.yMax)
           ||
           (SlotUtil.instance.transform.localPosition.x > quickSlotBaseRect.rect.xMin
           && SlotUtil.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax
           && SlotUtil.instance.transform.localPosition.y + baseRect.transform.localPosition.y > quickSlotBaseRect.rect.yMin + quickSlotBaseRect.transform.localPosition.y
           && SlotUtil.instance.transform.localPosition.y + baseRect.transform.localPosition.y < quickSlotBaseRect.rect.yMax + quickSlotBaseRect.transform.localPosition.y)))
        {
            if (SlotUtil.instance.slotUtil != null)
                theInputNumber.Call();
        }
        // �κ��丮 Ȥ�� ������ �������� �巡�װ� �����ٸ�
        else
        {
            SlotUtil.instance.SetColor(0);
            SlotUtil.instance.slotUtil = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (SlotUtil.instance.slotUtil != null)
        {
            ChangeSlot();

            if (isQuickSlot)  // �κ��丮->������ or ������->������
                theEffectItem.ActivateQuick(quickSlotNumber);
            else  // �κ��丮->�κ��丮. ������->�κ��丮
            {
                if (SlotUtil.instance.slotUtil.isQuickSlot)  // ������->�κ��丮
                    theEffectItem.ActivateQuick(SlotUtil.instance.slotUtil.quickSlotNumber);
            }
        }
    }
    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(SlotUtil.instance.slotUtil.item, SlotUtil.instance.slotUtil.itemCount);

        if (_tempItem != null)
            SlotUtil.instance.slotUtil.AddItem(_tempItem, _tempItemCount);
        else
            SlotUtil.instance.slotUtil.ClearSlot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theEffectItem.ShowToolTip(item, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theEffectItem.HideToolTip();
    }

}
