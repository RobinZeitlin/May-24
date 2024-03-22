using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SteamAchievement))]
public class AchievementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var _target = (SteamAchievement)target;

        if (GUILayout.Button("Unlock Achievement"))
        {
            EditorUtility.SetDirty(_target);

            _target.UnlockAchievement();
        }

        if(GUILayout.Button("Reset Achievement"))
        {
            EditorUtility.SetDirty(_target);

            _target.ResetAchievement();
        }
    }

}
#endif