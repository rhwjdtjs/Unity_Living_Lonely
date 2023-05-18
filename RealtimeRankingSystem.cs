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

    // PlayFab에서 받은 API 키를 입력하세요.
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

        // PlayFab 초기화
        PlayFabSettings.TitleId = PlayFabTitleId;
      //  PlayFabSettings.staticSettings.DeveloperSecretKey = PlayFabSecretKey;
    }

    public void UpdateKillCount(int killCount)
    {
        currentKillCount = killCount;

        // PlayFab로 킬 수 업데이트
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

        // 포톤에도 킬 수와 생존 시간 업로드
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "KillCount", killCount },
            { "SurvivalTimeSeconds", survivalTimeSeconds }
        });
    }

    public void UpdateSurvivalTime(float survivalTime)
    {
        currentSurvivalTime = survivalTime;

        // PlayFab로 생존 시간 업데이트
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
        // 플레이어 통계 업데이트 성공 처리
        Debug.Log("Player statistics updated successfully.");
    }

    private void OnUpdateStatisticsFailure(PlayFabError error)
    {
        // 플레이어 통계 업데이트 실패 처리
        Debug.LogError("Failed to update player statistics: " + error.GenerateErrorReport());
    }

    public void GetLeaderboard()
    {
        // KillCount에 대한 랭킹 조회
        var killCountRequest = new GetLeaderboardRequest
        {
            StatisticName = "KillCount",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(killCountRequest, OnGetKillCountLeaderboardSuccess, OnGetLeaderboardFailure);

        // SurvivalTime에 대한 랭킹 조회
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
        // KillCount 랭킹 조회 성공 처리
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("KillCount - " + item.Position + ": " + item.DisplayName + " - Kill Count: " + item.StatValue);
        }
    }

    private void OnGetSurvivalTimeLeaderboardSuccess(GetLeaderboardResult result)
    {
        // SurvivalTime 랭킹 조회 성공 처리
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("SurvivalTime - " + item.Position + ": " + item.DisplayName + " - Survival Time: " + item.StatValue);
        }
    }

    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        // 랭킹 조회 실패 처리
        Debug.LogError("Failed to get leaderboard: " + error.GenerateErrorReport());
    }
}