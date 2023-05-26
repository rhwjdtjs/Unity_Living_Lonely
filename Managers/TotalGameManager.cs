using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerobject; // �÷��̾� ������Ʈ�� �����ϱ� ���� ����
    [SerializeField] private GameObject playeruiobject; // �÷��̾� UI ������Ʈ�� �����ϱ� ���� ����
    [SerializeField] private StatusControllor thestatus; // �÷��̾� ���¸� �����ϱ� ���� StatusControllor ��ũ��Ʈ�� �����ϱ� ���� ����
    public int killcount = 0; // �÷��̾��� ų ���� ����ϴ� ����
    public static float survivaltimes; // �÷��̾��� ���� �ð��� ����ϴ� ����
    [SerializeField] public static float survivaltimesecond = 0; // �÷��̾��� ���� �ð�(��)�� ����ϴ� ���� (���� ������ �ٸ� ��ũ��Ʈ���� ���� ����)
    public static int survivaltimeminute = 0; // �÷��̾��� ���� �ð�(��)�� ����ϴ� ����
    public static int survivaltimehour = 0; // �÷��̾��� ���� �ð�(�ð�)�� ����ϴ� ����
    public static bool isPlayerDead = false; // �÷��̾ ����ߴ��� ���θ� ��Ÿ���� ����
    private PlayerSpawnManager theplayerspawn; // �÷��̾� ���� �Ŵ����� �����ϱ� ���� ����
    [SerializeField] private RealtimeRankingSystem rankingSystem; // �ǽð� ��ŷ �ý����� �����ϱ� ���� ����
    [SerializeField] private float survivalttime; // �÷��̾��� ���� �ð��� �����ϱ� ���� ����
    public float Survivalttime
    {
        get { return survivalttime; }
        set
        {
            survivalttime = value;
            survivaltimesecond = survivalttime;
        }
    }

    private void Awake()
    {
        rankingSystem = FindObjectOfType<RealtimeRankingSystem>(); // RealtimeRankingSystem ��ũ��Ʈ�� ã�� ����
    }

    void Start()
    {
        theplayerspawn = FindObjectOfType<PlayerSpawnManager>(); // PlayerSpawnManager ��ũ��Ʈ�� ã�� ����
        thestatus = FindObjectOfType<StatusControllor>(); // StatusControllor ��ũ��Ʈ�� ã�� ����
        theplayerspawn.RandomSelectSpawnPoint(); // ������ ���� ���� ����
    }

    private void timerecord()
    {
        survivaltimesecond += Time.deltaTime; // ���� �ð� ��� (DeltaTime�� �̿��Ͽ� �� ������ ����)
    }

    void Update()
    {
        if (!isPlayerDead)
            timerecord(); // �÷��̾ ������� �ʾ��� �� ���� �ð� ���

        playerresqawn(); // �÷��̾� ��Ȱ ó��

        if (Input.GetKeyDown(KeyCode.F12))
            Application.Quit(); // F12 Ű�� ������ ���� ����

        if (Input.GetKeyDown(KeyCode.F11))
        {
            rankingSystem.UpdateKillCount(killcount); // ��ŷ �ý��ۿ� ų �� ������Ʈ
            rankingSystem.UpdateSurvivalTime(survivaltimesecond); // ��ŷ �ý��ۿ� ���� �ð� ������Ʈ
            Debug.Log("��ϿϷ�");
        }
    }

    private void playerresqawn()
    {
        if (isPlayerDead) // �÷��̾ ����� ���
        {
            if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٸ� ������ ��Ȱ ó��
            {
                playerobject.gameObject.SetActive(true); // �÷��̾� ������Ʈ Ȱ��ȭ
                playeruiobject.gameObject.SetActive(false); // �÷��̾� UI ������Ʈ ��Ȱ��ȭ
                theplayerspawn.RandomSelectSpawnPoint(); // ������ ���� ���� ����
                thestatus.currentHp = 100; // �÷��̾� ü�� �ʱ�ȭ
                thestatus.currentHungry = 10000; // �÷��̾� ����� �ʱ�ȭ
                thestatus.currentThirsty = 10000; // �÷��̾� �񸶸� �ʱ�ȭ
                thestatus.sp = 4999; // �÷��̾� SP �ʱ�ȭ
                isPlayerDead = false; // ��� ���� ����
                LoadingSceneManager.LoadScene("gamescene"); // ���� �� �ٽ� �ε�

                if (!isPlayerDead)
                {
                    survivaltimesecond = 0; // ���� �ð� �ʱ�ȭ
                    survivaltimeminute = 0; // ���� �ð� �ʱ�ȭ
                    survivaltimehour = 0; // ���� �ð� �ʱ�ȭ
                    killcount = 0; // ų �� �ʱ�ȭ
                    return;
                }
            }
        }
    }
}
