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
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            PickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ShowtheInfo();
            }
        }
        else
            HideInfo();
    }

    private void ShowtheInfo()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "<color=yellow>"+ "(E)" +"</color>"+"  GET  " +hitInfo.transform.GetComponent<PickUP>().item.itemName;
    }

    private void HideInfo()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void PickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.GetComponent<PickUP>().item.itemType==Item.ItemType.ammo)
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 20);
                    Destroy(hitInfo.transform.gameObject);
                    HideInfo();
                }
                else
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 1);
                    Destroy(hitInfo.transform.gameObject);
                    HideInfo();
                }
                
            }
        }
    }
}
