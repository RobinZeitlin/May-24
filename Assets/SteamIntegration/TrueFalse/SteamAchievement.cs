using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

[CreateAssetMenu(fileName = "SteamAchievement", menuName = "Achievements/SteamAchievement")]
public class SteamAchievement : ScriptableObject
{
    public string achievementName;
    
    public void UnlockAchievement()
    {
        if (!SteamClient.IsValid) return;

        var ach = new Achievement(achievementName);
        ach.Trigger();

        Debug.Log(achievementName + "Lockstate = " + ach.State);
    }

    public void ResetAchievement()
    {
        if (!SteamClient.IsValid) return;

        var ach = new Achievement(achievementName);
        ach.Clear();

        Debug.Log(achievementName + "Lockstate = " + ach.State);
    }
}
