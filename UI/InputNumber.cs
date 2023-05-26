using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    private bool activated = false;

    [SerializeField] private Text text_Preview; // 미리보기 텍스트
    [SerializeField] private Text text_Input; // 입력 텍스트
    [SerializeField] private InputField if_text; // 입력 필드

    [SerializeField] private GameObject go_Base; // 기본 게임 오브젝트

    [SerializeField] private CheckActions thePlayer; // 플레이어의 CheckActions 스크립트

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

    // 아이템을 버리면 실행되는 함수
    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = SlotUtil.instance.slotUtil.itemCount.ToString();
    }

    // 취소 버튼 누르면 호출되는 함수
    public void Cancel()
    {
        activated = false;
        SlotUtil.instance.SetColor(0);
        go_Base.SetActive(false);
        SlotUtil.instance.slotUtil = null;
    }

    // 확인 버튼 누르면 호출되는 함수
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

    // 아이템 드롭 코루틴
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

    // 숫자 체크 함수
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
