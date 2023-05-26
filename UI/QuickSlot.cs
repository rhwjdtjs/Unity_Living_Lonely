using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private EffectItem theEffectItem; // EffectItem ��ũ��Ʈ�� �����ϴ� ����
    [SerializeField] Image activeimage1; // �����Կ� ǥ�õǴ� ������ �̹����� ǥ���ϱ� ���� Image ������Ʈ ����
    [SerializeField] public Slot[] quickSlots; // �����Ե��� ��� ���� Slot �迭
    [SerializeField] private Transform tf_parent; // �����Ե��� �θ� ������Ʈ�� �����ϴ� ����
    [SerializeField] public GameObject thebase; // �������� �⺻ ������ ��� �ִ� ���� ������Ʈ
    [SerializeField] private GameObject go_SelectedImage; // ���õ� �������� ǥ���ϴ� �̹��� ���� ������Ʈ
    [SerializeField] private GameObject activeimage; // �����Կ� �������� �ִ��� ���θ� ǥ���ϱ� ���� �̹��� ���� ������Ʈ
    [SerializeField] private WeaponManager theWeaponManager; // WeaponManager ��ũ��Ʈ�� �����ϴ� ����

    private int selectedSlot; // ���õ� �������� �ε���

    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>(); // �ڽ� ������Ʈ�� Slot ������Ʈ�� ��� ã�� �迭�� ����
        selectedSlot = 0; // �ʱ� ���õ� ������ �ε����� 0���� ����
    }

    void Update()
    {
        TryInputNumber(); // ���� �Է��� �����Ͽ� �������� �����ϴ� �޼��� ȣ��
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Ű���� ���� 1�� ������
            ChangeSlot(0); // �ε��� 0�� �ش��ϴ� ���������� ����
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Ű���� ���� 2�� ������
            ChangeSlot(1); // �ε��� 1�� �ش��ϴ� ���������� ����
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Ű���� ���� 3�� ������
            ChangeSlot(2); // �ε��� 2�� �ش��ϴ� ���������� ����
        else if (Input.GetKeyDown(KeyCode.Alpha4)) // Ű���� ���� 4�� ������
            ChangeSlot(3); // �ε��� 3�� �ش��ϴ� ���������� ����
        else if (Input.GetKeyDown(KeyCode.Alpha5)) // Ű���� ���� 5�� ������
            ChangeSlot(4); // �ε��� 4�� �ش��ϴ� ���������� ����
        else if (Input.GetKeyDown(KeyCode.Alpha6)) // Ű���� ���� 6�� ������
            ChangeSlot(5); // �ε��� 5�� �ش��ϴ� ���������� ����
    }

    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num); // ���õ� �������� �����ϴ� �޼��� ȣ��
        Execute(); // ���õ� �����Կ� ���� ���� ����
    }

    private void SelectedSlot(int _num)
    {
        selectedSlot = _num; // ���õ� ������ �ε����� ���޹��� �ε����� ����
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position; // ���õ� ������ �̹����� ��ġ�� ����� ������ ��ġ�� �̵�
    }

    public void EatItem()
    {
        theEffectItem.UseItem(quickSlots[selectedSlot].item); // ���õ� �������� �������� ����Ͽ� ȿ�� ����
        quickSlots[selectedSlot].SetSlotCount(-1); // ���õ� �������� ������ ������ ���ҽ�Ŵ
    }

    private void Execute()
    {
        if (quickSlots[selectedSlot].item != null) // ���õ� �����Կ� �������� �ִ� ���
        {
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Equipment) // �������� ����� ���
            {
                StartCoroutine(theWeaponManager.CHANGEWEAPONCO(quickSlots[selectedSlot].item.weaponType, quickSlots[selectedSlot].item.itemName)); // ���� ���� �ڷ�ƾ ����
                activeimage.SetActive(true); // �����Կ� �������� �ִ��� ǥ���ϴ� �̹��� Ȱ��ȭ
                activeimage1.sprite = quickSlots[selectedSlot].item.itemImage; // �����Կ� ������ �̹��� ǥ��
            }
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used) // �������� �Ҹ�ǰ�� ���
            {
                EatItem(); // ������ ��� �� ���� ����
            }
            else // �� ���� ��� (�������� ��Ÿ�� ���)
            {
                StartCoroutine(theWeaponManager.CHANGEWEAPONCO("HAND", "Hand")); // �⺻ ����� �����ϴ� �ڷ�ƾ ����
                activeimage.SetActive(false); // �����Կ� �������� �ִ��� ǥ���ϴ� �̹��� ��Ȱ��ȭ
            }
        }
        else // ���õ� �����Կ� �������� ���� ���
        {
            StartCoroutine(theWeaponManager.CHANGEWEAPONCO("HAND", "Hand")); // �⺻ ����� �����ϴ� �ڷ�ƾ ����
            activeimage.SetActive(false); // �����Կ� �������� �ִ��� ǥ���ϴ� �̹��� ��Ȱ��ȭ
        }
    }

    public void Activatequick(int _num)
    {
        if (selectedSlot == _num) // �̹� ���õ� �������� �ٽ� ������ ���
        {
            Execute(); // ���� ����
            return;
        }
        if (SlotUtil.instance != null)
        {
            if (SlotUtil.instance.slotUtil != null)
            {
                if (SlotUtil.instance.slotUtil.GetQuickSlotNumber() == selectedSlot) // �̹� ���õ� �������� �ٽ� ������ ���
                {
                    Execute(); // ���� ����
                    return;
                }
            }
        }
    }
}
