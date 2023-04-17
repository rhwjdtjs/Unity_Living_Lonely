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
    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 
    private RaycastHit hitInfo;
    public Item item;
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) //e�� ������� 
        {
            CheckItem(); //�������� �������� Ȯ���ϰ�
            PickUp(); //�ֿ� �� �ְ���
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)) //���̾� ����ũ�� ���������� Ȯ���ϰ�, �÷��̾� ��ġ���� ������ ���� ��
        {
            if (hitInfo.transform.tag == "Item") //���̾��ũ�� ��ġ�ϰ� �±׵� �������̸�
            {
                ShowtheInfo(); //������ ������ ������
            }
        }
        else
            HideInfo(); //�׿��� ��� ������ ������ ����
    }

    private void ShowtheInfo()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "<color=yellow>"+ "(E)" +"</color>"+"  GET  " +hitInfo.transform.GetComponent<PickUP>().item.itemName; //������ ���� �ؽ�Ʈ
    }

    private void HideInfo()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false); //�������� �ҷ����� �������� �ؽ�Ʈ ��Ȱ��ȭ
    }

    private void PickUp()
    {
        if (pickupActivated) //�������� �߾ӿ� �ν�������
        {
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.GetComponent<PickUP>().item.itemType==Item.ItemType.ammo) //���� ������ Ÿ���� ź���̶��
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 20); //20���� �ֿ�
                    Destroy(hitInfo.transform.gameObject); //�ݰ��� �ʵ忡 �ִ� �������� ����
                    HideInfo();
                }
                else
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 1); //�׿��� ���� �ϳ��� �������� ���´�
                    Destroy(hitInfo.transform.gameObject);
                    HideInfo();
                }
                
            }
        }
    }
}
