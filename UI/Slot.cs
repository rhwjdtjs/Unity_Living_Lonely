using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private bool isQuickSlot;  // 해당 슬롯이 퀵슬롯인지 여부를 판단하는 변수
    [SerializeField] private int quickSlotNumber;  // 퀵슬롯 번호
    [SerializeField] private Text text_Count;
    [SerializeField] private GameObject go_CountImage;
    [SerializeField] RectTransform quickSlotBaseRect; // 퀵슬롯 영역의 RectTransform 컴포넌트
    [SerializeField] private RectTransform baseRect;

    private InputUI theInputNumber;
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템 이미지
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

    // 해당 슬롯의 퀵슬롯 번호를 반환하는 메서드
    public int GetQuickSlotNumber()
    {
        return quickSlotNumber;
    }

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
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

    // 해당 슬롯의 아이템 개수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    // 마우스 오른쪽 클릭 시 실행되는 이벤트 핸들러
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    // 장비 아이템인 경우 무기를 변경
                    StartCoroutine(theWeaponManager.CHANGEWEAPONCO(item.weaponType, item.itemName));
                    activeimagebase.SetActive(true);
                    activeimage.sprite = item.itemImage;
                }

                // 소비 아이템인 경우 아이템 사용
                if (item.itemType == Item.ItemType.Used)
                {
                    theEffectItem.UseItem(item);
                    SetSlotCount(-1);
                }

                // 장비 아이템이 아닌 경우 액티브 이미지 비활성화
                if (!(item.itemType == Item.ItemType.Equipment))
                {
                    activeimagebase.SetActive(false);
                }
            }
        }
    }

    // 드래그 시작 시 실행되는 이벤트 핸들러
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            SlotUtil.instance.slotUtil = this;
            SlotUtil.instance.SetImage(itemImage);
            SlotUtil.instance.transform.position = eventData.position;
        }
    }

    // 드래그 중일 때 실행되는 이벤트 핸들러
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            SlotUtil.instance.transform.position = eventData.position;
    }

    // 드래그 종료 시 실행되는 이벤트 핸들러
    public void OnEndDrag(PointerEventData eventData)
    {
        // 인벤토리 또는 퀵슬롯 영역에서 드래그가 끝났을 경우
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
                theInputNumber.Call(); // 아이템 개수 입력 UI 호출
        }
        // 인벤토리 또는 퀵슬롯 영역 내에서 드래그가 끝났을 경우
        else
        {
            SlotUtil.instance.SetColor(0);
            SlotUtil.instance.slotUtil = null;
        }
    }

    // 다른 슬롯으로 아이템을 드롭했을 때 실행되는 이벤트 핸들러
    public void OnDrop(PointerEventData eventData)
    {
        if (SlotUtil.instance.slotUtil != null)
        {
            ChangeSlot();

            if (isQuickSlot)  // 인벤토리 -> 퀵슬롯 또는 퀵슬롯 -> 퀵슬롯
                theEffectItem.ActivateQuick(quickSlotNumber);
            else  // 인벤토리 -> 인벤토리, 퀵슬롯 -> 인벤토리
            {
                if (SlotUtil.instance.slotUtil.isQuickSlot)  // 퀵슬롯 -> 인벤토리
                    theEffectItem.ActivateQuick(SlotUtil.instance.slotUtil.quickSlotNumber);
            }
        }
    }

    // 슬롯에 마우스를 올렸을 때 실행되는 이벤트 핸들러
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theEffectItem.ShowToolTip(item, transform.position);
    }

    // 슬롯에서 마우스가 벗어났을 때 실행되는 이벤트 핸들러
    public void OnPointerExit(PointerEventData eventData)
    {
        theEffectItem.HideToolTip();
    }
}
