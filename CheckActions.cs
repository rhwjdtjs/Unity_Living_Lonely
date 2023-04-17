using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckActions : MonoBehaviour
{
    [SerializeField]private float range;
    [SerializeField] private Inventory theInventory;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Text actionText;
    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    private RaycastHit hitInfo;
    public Item item;
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) //e를 누를경우 
        {
            CheckItem(); //아이템을 매프레임 확인하고
            PickUp(); //주울 수 있게함
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)) //레이어 마스크가 아이템인지 확인하고, 플레이어 위치에서 앞으로 빔을 쏨
        {
            if (hitInfo.transform.tag == "Item") //레이어마스크도 일치하고 태그도 아이템이면
            {
                ShowtheInfo(); //아이템 정보를 보여줌
            }
        }
        else
            HideInfo(); //그외의 경우 아이템 정보를 숨김
    }

    private void ShowtheInfo()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "<color=yellow>"+ "(E)" +"</color>"+"  GET  " +hitInfo.transform.GetComponent<PickUP>().item.itemName; //아이템 정보 텍스트
    }

    private void HideInfo()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false); //아이템을 불러오질 않을때는 텍스트 비활성화
    }

    private void PickUp()
    {
        if (pickupActivated) //아이템이 중앙에 인식했을때
        {
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.GetComponent<PickUP>().item.itemType==Item.ItemType.ammo) //만약 아이템 타입이 탄약이라면
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 20); //20개를 주움
                    Destroy(hitInfo.transform.gameObject); //줍고나면 필드에 있는 아이템은 삭제
                    HideInfo();
                }
                else
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 1); //그외의 경우는 하나씩 아이템을 집는다
                    Destroy(hitInfo.transform.gameObject);
                    HideInfo();
                }
                
            }
        }
    }
}
