using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SteamAchievementCounter))]
public class AchievementCounterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var _target = (SteamAchievementCounter)target;

        if (GUILayout.Button("Increase Stat"))
        {
            EditorUtility.SetDirty(_target);

            _target.IncreaseStat();
        }

        if(GUILayout.Button("Reset Stat"))
        {
            EditorUtility.SetDirty(_target);

            _target.ResetStat();
        }
    }

}
#endif