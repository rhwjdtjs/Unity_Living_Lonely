using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputNumber : MonoBehaviour
{
    private bool activated = false;

    [SerializeField]
    private Text text_Preview;
    [SerializeField]
    private Text text_Input;
    [SerializeField]
    private InputField if_text;

    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private CheckActions thePlayer;

    void Update()
    {
        if (activated)//인풋 필드가 활성화 되었을때
        {
            if (Input.GetKeyDown(KeyCode.Return))
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }
    }

    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = SlotUtil.instance.slotUtil.itemCount.ToString();
    }

    public void Cancel()
    {
        activated = false;
        SlotUtil.instance.SetColor(0);
        go_Base.SetActive(false);
        SlotUtil.instance.slotUtil = null;
    }

    public void OK() //만약 버린다면
    {
        SlotUtil.instance.SetColor(0); //컬러 알파 값을 0으로 지정 (안보임)

        int num;
        if (text_Input.text != "") //텍스트 입력란에 무언가 쓰여있으면
        {
            if (CheckNumber(text_Input.text)) //텍스트의 숫자를 확인
            {
                num = int.Parse(text_Input.text); //텍스트를 정수형으로 변환
                if (num > SlotUtil.instance.slotUtil.itemCount) //그 정수가 내가 갖고 있는 아이템 갯수보다 많으면
                    num = SlotUtil.instance.slotUtil.itemCount; //num값을 아이템 갯수로 설정
            }
            else
                num = 1; //그외에는 1로설정
        }
        else
            num = int.Parse(text_Preview.text); //미리 보는 텍스트 값을 정수형으로 변환 

        StartCoroutine(DropItemCorountine(num)); //아이템 드랍 시작
    }

    IEnumerator DropItemCorountine(int _num)
    {
        for (int i = 0; i < _num; i++) //num 갯수만큼 반복문
        {
            if (SlotUtil.instance.slotUtil.item.itemPrefab != null) //아이템이 있으면
            {
                Instantiate(SlotUtil.instance.slotUtil.item.itemPrefab,
                            thePlayer.transform.position + thePlayer.transform.forward,
                            Quaternion.identity); //플레이어 앞에 아이템 소환
            }
            SlotUtil.instance.slotUtil.SetSlotCount(-1); //인벤토리 아이템 갯수 차감
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
