using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUtil : MonoBehaviour
{
    [SerializeField] private Image imageItem; // �巡���� ������ �̹����� ǥ���ϱ� ���� Image ������Ʈ

    static public SlotUtil instance; // SlotUtil Ŭ������ �ν��Ͻ��� �����ϱ� ���� ���� ����
    public Slot slotUtil; // ���� Ȱ��ȭ�� ������ �����ϱ� ���� ����

    void Start()
    {
        instance = this; // �ν��Ͻ��� �ڱ� �ڽ��� �����Ͽ� �̱��� ������ ����
    }

    // �巡���� �������� �̹����� �����ϴ� �޼���
    public void SetImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite; // ���޹��� ������ �̹����� �̹��� ����
        SetColor(1); // ������ 1�� �����Ͽ� �̹����� ���̵��� ��
    }

    // �̹����� ������ �����ϴ� �޼���
    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha; // ���޹��� ���� ������ �̹����� ���İ��� ����
        imageItem.color = color;
    }
}
