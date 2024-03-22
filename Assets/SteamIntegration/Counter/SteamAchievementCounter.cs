using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

[CreateAssetMenu(fileName = "SteamAchievement", menuName = "Achievements/SteamAchievementCounter")]
public class SteamAchievementCounter : ScriptableObject
{
    public string achievementName;
    
    public void IncreaseStat()
    {
        if (!SteamClient.IsValid) return;

        SteamUserStats.AddStat(achievementName, 1);
        SteamUserStats.StoreStats();
    }

    public void ResetStat()
    {
        if (!SteamClient.IsValid) return;

        SteamUserStats.SetStat(achievementName, 0);
    }
}
