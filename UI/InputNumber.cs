using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    private bool activated = false;

    [SerializeField] private Text text_Preview; // �̸����� �ؽ�Ʈ
    [SerializeField] private Text text_Input; // �Է� �ؽ�Ʈ
    [SerializeField] private InputField if_text; // �Է� �ʵ�

    [SerializeField] private GameObject go_Base; // �⺻ ���� ������Ʈ

    [SerializeField] private CheckActions thePlayer; // �÷��̾��� CheckActions ��ũ��Ʈ

    void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }

    // �������� ������ ����Ǵ� �Լ�
    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = SlotUtil.instance.slotUtil.itemCount.ToString();
    }

    // ��� ��ư ������ ȣ��Ǵ� �Լ�
    public void Cancel()
    {
        activated = false;
        SlotUtil.instance.SetColor(0);
        go_Base.SetActive(false);
        SlotUtil.instance.slotUtil = null;
    }

    // Ȯ�� ��ư ������ ȣ��Ǵ� �Լ�
    public void OK()
    {
        SlotUtil.instance.SetColor(0);

        int num;
        if (text_Input.text != "")
        {
            if (CheckNumber(text_Input.text))
            {
                num = int.Parse(text_Input.text);
                if (num > SlotUtil.instance.slotUtil.itemCount)
                    num = SlotUtil.instance.slotUtil.itemCount;
            }
            else
                num = 1;
        }
        else
            num = int.Parse(text_Preview.text);

        StartCoroutine(DropItemCoroutine(num));
    }

    // ������ ��� �ڷ�ƾ
    IEnumerator DropItemCoroutine(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            if (SlotUtil.instance.slotUtil.item.itemPrefab != null)
            {
                Instantiate(SlotUtil.instance.slotUtil.item.itemPrefab,
                            thePlayer.transform.position + thePlayer.transform.forward,
                            Quaternion.identity);
            }
            SlotUtil.instance.slotUtil.SetSlotCount(-1);
            yield return new WaitForSeconds(0.05f);
        }

        SlotUtil.instance.slotUtil = null;
        go_Base.SetActive(false);
        activated = false;
    }

    // ���� üũ �Լ�
    private bool CheckNumber(string _argString)
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;

        for (int i = 0; i < _tempCharArray.Length; i++)
        {
            if (_tempCharArray[i] >= 48 && _tempCharArray[i] <= 57)
                continue;
            isNumber = false;
        }
        return isNumber;
    }
}
