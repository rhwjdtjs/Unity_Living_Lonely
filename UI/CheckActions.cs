using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckActions : MonoBehaviour
{
    [SerializeField] private float range; // 습득 가능한 범위
    [SerializeField] private Inventory theInventory; // 인벤토리 객체
    [SerializeField] private LayerMask layerMask; // 레이어 마스크
    [SerializeField] private Text actionText; // 상호작용 액션 텍스트
    private bool pickupActivated = false; // 아이템 습득 가능 여부
    private RaycastHit hitInfo; // 레이캐스트에 맞은 정보
    public Item item; // 현재 아이템

    void Update()
    {
        CheckItem(); // 아이템 체크
        TryAction(); // 상호작용 시도
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // E 키를 눌렀을 때
        {
            CheckItem(); // 아이템 체크
            PickUp(); // 아이템 습득 시도
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)) // 플레이어 앞쪽으로 레이캐스트 발사하여 아이템 감지
        {
            if (hitInfo.transform.tag == "Item") // 레이캐스트에 맞은 객체의 태그가 "Item"인 경우
            {
                ShowTheInfo(); // 아이템 정보 표시
            }
        }
        else
        {
            HideInfo(); // 아이템 정보 숨김
        }
    }

    private void ShowTheInfo()
    {
        pickupActivated = true; // 아이템 습득 가능 상태로 설정
        actionText.gameObject.SetActive(true); // 상호작용 액션 텍스트 활성화
        actionText.text = "<color=yellow>" + "(E)" + "</color>" + "  GET  " + hitInfo.transform.GetComponent<PickUP>().item.itemName; // 상호작용 액션 텍스트 내용 설정
    }

    private void HideInfo()
    {
        pickupActivated = false; // 아이템 습득 불가능 상태로 설정
        actionText.gameObject.SetActive(false); // 상호작용 액션 텍스트 비활성화
    }

    private void PickUp()
    {
        if (pickupActivated) // 아이템 습득 가능한 상태인 경우
        {
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.GetComponent<PickUP>().item.itemType == Item.ItemType.ammo) // 아이템의 타입이 탄약인 경우
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 20); // 인벤토리에 아이템 추가
                    Destroy(hitInfo.transform.gameObject); // 아이템 오브젝트 삭제
                    HideInfo(); // 아이템 정보 숨김
                }
                else // 탄약이 아닌 다른 아이템인 경우
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 1); // 인벤토리에 아이템 추가
                    Destroy(hitInfo.transform.gameObject); // 아이템 오브젝트 삭제
                    HideInfo(); // 아이템 정보 숨김
                }
            }
        }
    }
}
