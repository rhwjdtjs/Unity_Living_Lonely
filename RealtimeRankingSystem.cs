using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using PlayFab.DataModels;

public class RealtimeRankingSystem : MonoBehaviourPunCallbacks
{
    private static RealtimeRankingSystem instance = null;
    private const string LeaderboardStatisticName = "TotalKills";
    private const string LeaderboardSurvivalTimeName = "TotalSurvivalTime";

    // PlayFab���� ���� API Ű�� �Է��ϼ���.
    private const string PlayFabTitleId = "1C256";
    private const string PlayFabSecretKey = "C1IDMS1C1FF93ZSAN43KEQC7CJF8DFP9A8GO6CQ1KFE5DU14W4";

    private int currentKillCount;
    private float currentSurvivalTime;
    private void Awake()
    {
        if (null == instance)
        {
            
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
          
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

        // PlayFab �ʱ�ȭ
        PlayFabSettings.TitleId = PlayFabTitleId;
      //  PlayFabSettings.staticSettings.DeveloperSecretKey = PlayFabSecretKey;
    }

    public void UpdateKillCount(int killCount)
    {
        currentKillCount = killCount;

        // PlayFab�� ų �� ������Ʈ
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = LeaderboardStatisticName,
                    Value = currentKillCount
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticsSuccess, OnUpdateStatisticsFailure);
    }
    public void UpdateStatistics(int killCount, float survivalTimeSeconds)
    {
        var statistics = new List<StatisticUpdate>
        {
            new StatisticUpdate { StatisticName = "KillCount", Value = killCount },
            new StatisticUpdate { StatisticName = "SurvivalTimeSeconds", Value = (int)survivalTimeSeconds }
        };

        var request = new UpdatePlayerStatisticsRequest { Statistics = statistics };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticsSuccess, OnUpdateStatisticsFailure);

        // ���濡�� ų ���� ���� �ð� ���ε�
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "KillCount", killCount },
            { "SurvivalTimeSeconds", survivalTimeSeconds }
        });
    }

    public void UpdateSurvivalTime(float survivalTime)
    {
        currentSurvivalTime = survivalTime;

        // PlayFab�� ���� �ð� ������Ʈ
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = LeaderboardSurvivalTimeName,
                    Value = (int)currentSurvivalTime
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticsSuccess, OnUpdateStatisticsFailure);
    }

  

    private void OnUpdateStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        // �÷��̾� ��� ������Ʈ ���� ó��
        Debug.Log("Player statistics updated successfully.");
    }

    private void OnUpdateStatisticsFailure(PlayFabError error)
    {
        // �÷��̾� ��� ������Ʈ ���� ó��
        Debug.LogError("Failed to update player statistics: " + error.GenerateErrorReport());
    }

    public void GetLeaderboard()
    {
        // KillCount�� ���� ��ŷ ��ȸ
        var killCountRequest = new GetLeaderboardRequest
        {
            StatisticName = "KillCount",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(killCountRequest, OnGetKillCountLeaderboardSuccess, OnGetLeaderboardFailure);

        // SurvivalTime�� ���� ��ŷ ��ȸ
        var survivalTimeRequest = new GetLeaderboardRequest
        {
            StatisticName = "SurvivalTimeSeconds",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(survivalTimeRequest, OnGetSurvivalTimeLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    private void OnGetKillCountLeaderboardSuccess(GetLeaderboardResult result)
    {
        // KillCount ��ŷ ��ȸ ���� ó��
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("KillCount - " + item.Position + ": " + item.DisplayName + " - Kill Count: " + item.StatValue);
        }
    }

    private void OnGetSurvivalTimeLeaderboardSuccess(GetLeaderboardResult result)
    {
        // SurvivalTime ��ŷ ��ȸ ���� ó��
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("SurvivalTime - " + item.Position + ": " + item.DisplayName + " - Survival Time: " + item.StatValue);
        }
    }

    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        // ��ŷ ��ȸ ���� ó��
        Debug.LogError("Failed to get leaderboard: " + error.GenerateErrorReport());
    }
}