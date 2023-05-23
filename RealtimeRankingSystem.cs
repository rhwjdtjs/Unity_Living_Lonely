using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using PlayFab.DataModels;
using System;

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

    
    public void UpdateStatistics(int killCount, float survivalTimeSeconds)
    {
        // 기존의 킬 수와 생존 시간을 가져옵니다.
        int previousKillCount = 0;
        float previousSurvivalTime = 0;

        GetKillCount(
            value =>
            {
                previousKillCount = value;

                GetSurvivalTime(
                    value2 =>
                    {
                        previousSurvivalTime = value2;

                        Debug.Log(previousKillCount);
                        Debug.Log(previousSurvivalTime);
                        Debug.Log(killCount);
                        Debug.Log(survivalTimeSeconds);

                    // 현재 기록이 이전 기록보다 낮으면 기록을 하지 않습니다.
                    if (killCount <= previousKillCount && survivalTimeSeconds <= previousSurvivalTime)
                        {
                        // 기록을 하지 않고 종료합니다.
                        return;
                        }

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
                    },
                    error2 =>
                    {
                        Debug.LogError("Failed to get previous survival time: " + error2);
                    }
                );
            },
            error =>
            {
                Debug.LogError("Failed to get previous kill count: " + error);
            }
        );
    }


    private void GetKillCount(Action<int> onSuccess, Action<string> onFailure)
    {
        var request = new GetPlayerStatisticsRequest();
        PlayFabClientAPI.GetPlayerStatistics(request,
            result =>
            {
                var killCountStat = result.Statistics.Find(stat => stat.StatisticName == "KillCount");
                if (killCountStat != null)
                {
                    onSuccess?.Invoke(killCountStat.Value);
                }
                else
                {
                    onFailure?.Invoke("Kill count statistic not found");
                }
            },
            error =>
            {
                onFailure?.Invoke("Failed to get kill count: " + error.ErrorMessage);
            }
        );
    }

    private void GetSurvivalTime(Action<float> onSuccess, Action<string> onFailure)
    {
        var request = new GetPlayerStatisticsRequest();
        PlayFabClientAPI.GetPlayerStatistics(request,
            result =>
            {
                var survivalTimeStat = result.Statistics.Find(stat => stat.StatisticName == "SurvivalTimeSeconds");
                if (survivalTimeStat != null)
                {
                    onSuccess?.Invoke(survivalTimeStat.Value);
                }
                else
                {
                    onFailure?.Invoke("Survival time statistic not found");
                }
            },
            error =>
            {
                onFailure?.Invoke("Failed to get survival time: " + error.ErrorMessage);
            }
        );
    }
    public void UpdateKillCount(int killCount)
    {
        // 기존의 킬 수를 가져옵니다.
        GetKillCount(
            previousKillCount =>
            {
            // 현재 기록이 이전 기록보다 낮으면 기록을 하지 않습니다.
            if (killCount <= previousKillCount)
                {
                // 기록을 하지 않고 종료합니다.
                return;
                }

                var statistics = new List<StatisticUpdate>
                {
                new StatisticUpdate { StatisticName = "KillCount", Value = killCount }
                };

                var request = new UpdatePlayerStatisticsRequest { Statistics = statistics };
                PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticsSuccess, OnUpdateStatisticsFailure);

            // 포톤에도 킬 수 업로드
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
                {
                { "KillCount", killCount }
                });
            },
            error =>
            {
                Debug.LogError("Failed to get previous kill count: " + error);
            }
        );
    }

    public void UpdateSurvivalTime(float survivalTimeSeconds)
    {
        // 기존의 생존 시간을 가져옵니다.
        GetSurvivalTime(
            previousSurvivalTime =>
            {
            // 현재 기록이 이전 기록보다 낮으면 기록을 하지 않습니다.
            if (survivalTimeSeconds <= previousSurvivalTime)
                {
                // 기록을 하지 않고 종료합니다.
                return;
                }

                var statistics = new List<StatisticUpdate>
                {
                new StatisticUpdate { StatisticName = "SurvivalTimeSeconds", Value = (int)survivalTimeSeconds }
                };

                var request = new UpdatePlayerStatisticsRequest { Statistics = statistics };
                PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticsSuccess, OnUpdateStatisticsFailure);

            // 포톤에도 생존 시간 업로드
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
                {
                { "SurvivalTimeSeconds", survivalTimeSeconds }
                });
            },
            error =>
            {
                Debug.LogError("Failed to get previous survival time: " + error);
            }
        );
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