using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class PrefabPreviewer : EditorWindow
{
    public DuckInfoSO duckInfo;
    private GameObject prefabToPreview;
    private GameObject hatToPreview;
    private GameObject previewInstance;

    private Vector2 previewAreaSize = new Vector2(256, 256);

    private SerializedObject serializedDuckInfo;
    private ReorderableList hatsList;

    private Editor previewEditor;

    [MenuItem("Tools/Prefab Previewer")]
    public static void ShowWindow()
    {
        GetWindow<PrefabPreviewer>("Prefab Previewer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab Previewer", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        prefabToPreview = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabToPreview, typeof(GameObject), false);
        duckInfo = (DuckInfoSO)EditorGUILayout.ObjectField("Duck Info", duckInfo, typeof(DuckInfoSO), false);

        if (duckInfo != null)
        {
            SetupHatsList();
        }

        if (EditorGUI.EndChangeCheck())
        {
            UpdatePreview();
        }

        if(hatsList != null)
        if (GUILayout.Button("Random Outfit"))
        {
            SelectRandomHat();
        }

        if (previewInstance != null)
        {
            DrawPreviewArea();
        }

        serializedDuckInfo?.Update();
        hatsList?.DoLayoutList();
        serializedDuckInfo?.ApplyModifiedProperties();
    }

    void UpdatePreview()
    {
        if (previewInstance != null)
        {
            DestroyImmediate(previewInstance);
        }

        if (prefabToPreview != null)
        {
            previewInstance = new GameObject("Preview");
            previewInstance.hideFlags = HideFlags.HideAndDontSave;

            var prefabInstance = Instantiate(prefabToPreview, previewInstance.transform);
            prefabInstance.hideFlags = HideFlags.HideAndDontSave;

            if (hatToPreview != null)
            {
                var hatInstance = Instantiate(hatToPreview, prefabInstance.transform);
                hatInstance.hideFlags = HideFlags.HideAndDontSave;

                hatInstance.transform.localPosition = new Vector3(0, 1.11f, 0.15f);
                hatInstance.transform.localRotation = Quaternion.Euler(-35, 0, 0);
                hatInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            if (previewEditor != null)
            {
                DestroyImmediate(previewEditor);
            }
            previewEditor = Editor.CreateEditor(previewInstance);
        }
    }

    void SelectRandomHat()
    {
        if (duckInfo != null && duckInfo.hats.Count > 0)
        {
            int randomIndex = Random.Range(0, duckInfo.hats.Count);
            hatToPreview = duckInfo.hats[randomIndex];
            UpdatePreview();
        }
    }

    void DrawPreviewArea()
    {
        GUILayout.Label("Live Preview:", EditorStyles.boldLabel);
        Rect r = GUILayoutUtility.GetRect(previewAreaSize.x, previewAreaSize.y, GUILayout.ExpandWidth(true));
        if (previewEditor != null)
        {
            previewEditor.OnInteractivePreviewGUI(r, GUIStyle.none);
        }
    }

    void SetupHatsList()
    {
        if (duckInfo == null) return;

        serializedDuckInfo = new SerializedObject(duckInfo);
        SerializedProperty hatsProperty = serializedDuckInfo.FindProperty("hats");

        hatsList = new ReorderableList(serializedDuckInfo, hatsProperty, true, true, true, true)
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Hats"),
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = hatsProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    element, GUIContent.none);
            }
        };
    }

    private void OnDestroy()
    {
        if (previewInstance != null)
        {
            DestroyImmediate(previewInstance);
        }
        if (previewEditor != null)
        {
            DestroyImmediate(previewEditor);
        }
    }
}
