using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputUI : MonoBehaviour //아이템을 버릴때 불러오는 함수
{
    [SerializeField] private Text text_Preview;
    [SerializeField] private Text text_Input;
    [SerializeField] private InputField if_text;
    [SerializeField] private GameObject go_Base;
    [SerializeField] private CheckActions thePlayer;
    private bool activated = false;
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

    public void Call() //UI를 불러오는함수
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = SlotUtil.instance.slotUtil.itemCount.ToString();
    }

    public void Cancel() //취소
    {
        activated = false;
        SlotUtil.instance.SetColor(0);
        go_Base.SetActive(false);
        SlotUtil.instance.slotUtil = null;
    }

    public void OK() //아이템 버릴때 확인하는 함수
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

        StartCoroutine(DropItemCO(num));
    }

    IEnumerator DropItemCO(int _num) //아이템을 버릴때
    {
        for (int i = 0; i < _num; i++)
        {
            if (SlotUtil.instance.slotUtil.item.itemPrefab != null)
            {
                Instantiate(SlotUtil.instance.slotUtil.item.itemPrefab,
                            thePlayer.transform.position + thePlayer.transform.forward,
                            Quaternion.identity); //플레이어 앞에 버린 아이템 프리팹 생성
            }
            SlotUtil.instance.slotUtil.SetSlotCount(-1);
            yield return new WaitForSeconds(0.05f);
        }

        SlotUtil.instance.slotUtil = null;
        go_Base.SetActive(false);
        activated = false;
    }

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
