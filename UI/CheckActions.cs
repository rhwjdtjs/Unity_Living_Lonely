using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckActions : MonoBehaviour
{
    [SerializeField] private float range; // ���� ������ ����
    [SerializeField] private Inventory theInventory; // �κ��丮 ��ü
    [SerializeField] private LayerMask layerMask; // ���̾� ����ũ
    [SerializeField] private Text actionText; // ��ȣ�ۿ� �׼� �ؽ�Ʈ
    private bool pickupActivated = false; // ������ ���� ���� ����
    private RaycastHit hitInfo; // ����ĳ��Ʈ�� ���� ����
    public Item item; // ���� ������

    void Update()
    {
        CheckItem(); // ������ üũ
        TryAction(); // ��ȣ�ۿ� �õ�
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // E Ű�� ������ ��
        {
            CheckItem(); // ������ üũ
            PickUp(); // ������ ���� �õ�
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)) // �÷��̾� �������� ����ĳ��Ʈ �߻��Ͽ� ������ ����
        {
            if (hitInfo.transform.tag == "Item") // ����ĳ��Ʈ�� ���� ��ü�� �±װ� "Item"�� ���
            {
                ShowTheInfo(); // ������ ���� ǥ��
            }
        }
        else
        {
            HideInfo(); // ������ ���� ����
        }
    }

    private void ShowTheInfo()
    {
        pickupActivated = true; // ������ ���� ���� ���·� ����
        actionText.gameObject.SetActive(true); // ��ȣ�ۿ� �׼� �ؽ�Ʈ Ȱ��ȭ
        actionText.text = "<color=yellow>" + "(E)" + "</color>" + "  GET  " + hitInfo.transform.GetComponent<PickUP>().item.itemName; // ��ȣ�ۿ� �׼� �ؽ�Ʈ ���� ����
    }

    private void HideInfo()
    {
        pickupActivated = false; // ������ ���� �Ұ��� ���·� ����
        actionText.gameObject.SetActive(false); // ��ȣ�ۿ� �׼� �ؽ�Ʈ ��Ȱ��ȭ
    }

    private void PickUp()
    {
        if (pickupActivated) // ������ ���� ������ ������ ���
        {
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.GetComponent<PickUP>().item.itemType == Item.ItemType.ammo) // �������� Ÿ���� ź���� ���
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 20); // �κ��丮�� ������ �߰�
                    Destroy(hitInfo.transform.gameObject); // ������ ������Ʈ ����
                    HideInfo(); // ������ ���� ����
                }
                else // ź���� �ƴ� �ٸ� �������� ���
                {
                    theInventory.GetItem(hitInfo.transform.GetComponent<PickUP>().item, 1); // �κ��丮�� ������ �߰�
                    Destroy(hitInfo.transform.gameObject); // ������ ������Ʈ ����
                    HideInfo(); // ������ ���� ����
                }
            }
        }
    }
}
