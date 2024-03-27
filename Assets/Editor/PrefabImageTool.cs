using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Rendering;

public class PrefabImageTool : EditorWindow
{
    private Vector3 previewAreaSize = new Vector3(0, 100, 0);

    private GameObject gameObjectToRender;
    private Camera renderCamera;

    private Editor previewEditor;

    private float hatRotationY;
    private float hatRotationX;
    private float hatScale;

    private string savePath = "Assets/HatSprites/";

    private Texture2D latestImage;

    [MenuItem("Tools/Save GameObject Image")]
    public static void ShowWindow()
    {
        GetWindow<PrefabImageTool>(false, "GameObject Image Saver", true);
    }

    private void OnGUI()
    {
        GUILayout.Label("Image Saver", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();

        gameObjectToRender = EditorGUILayout.ObjectField("GameObject", gameObjectToRender, typeof(GameObject), true) as GameObject;

        GUILayout.Label("Preview Settings:", EditorStyles.boldLabel);

        if(gameObjectToRender == null)
        {
            return;
        }

        if (EditorGUI.EndChangeCheck())
        {
            UpdatePreview();
        }

        EditorGUI.BeginDisabledGroup(true);
        DrawPreviewArea();
        EditorGUI.EndDisabledGroup();

        GUILayout.BeginHorizontal();

        hatRotationY = EditorGUILayout.FloatField("Hat Rotation Y", hatRotationY);
        hatRotationX = EditorGUILayout.FloatField("Hat Rotation X", hatRotationX);
        hatScale = EditorGUILayout.FloatField("Hat Scale", hatScale);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Save Image"))
        {
            if (gameObjectToRender != null)
            {
                CaptureAndSaveGameObjectImage(gameObjectToRender);
            }
            else
            {
                Debug.LogError("No GameObject selected to render.");
            }
        }

        if (latestImage != null)
        {
            GUILayout.Label("Latest Image:", EditorStyles.boldLabel);
            GUILayout.Label(latestImage, GUILayout.ExpandWidth(true), GUILayout.Height(400));
        }

        gameObjectToRender.transform.localScale = new Vector3(hatScale, hatScale, hatScale);
        gameObjectToRender.transform.eulerAngles = new Vector3(hatRotationX * 5, -hatRotationY * 5, 0);
    }

    private void UpdatePreview()
    {
        if (previewEditor != null)
        {
            DestroyImmediate(previewEditor);
        }

        if (gameObjectToRender != null)
        {
            previewEditor = Editor.CreateEditor(gameObjectToRender);
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

    private void CaptureAndSaveGameObjectImage(GameObject gameObject)
    {
        // Create a temporary camera for rendering
        GameObject tempCameraObj = new GameObject("TempCamera");
        renderCamera = tempCameraObj.AddComponent<Camera>();
        renderCamera.backgroundColor = new Color(0, 0, 0, 0); // Transparent background
        renderCamera.clearFlags = CameraClearFlags.SolidColor;

        // Position the camera in front of the object
        Bounds bounds = CalculateBounds(gameObject);

        renderCamera.transform.position = bounds.center + Vector3.back * 1;
        renderCamera.transform.LookAt(bounds.center);

        // Create a RenderTexture
        RenderTexture renderTexture = new RenderTexture(1024, 1024, 24);
        renderCamera.targetTexture = renderTexture;

        // Render the GameObject
        RenderTexture.active = renderTexture;
        renderCamera.Render();

        // Save RenderTexture to a PNG
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        byte[] bytes = texture2D.EncodeToPNG();
        File.WriteAllBytes(savePath + gameObject.name + ".png", bytes);

        latestImage = texture2D;

        // Clean up
        RenderTexture.active = null;
        DestroyImmediate(tempCameraObj);
        DestroyImmediate(renderTexture);

        AssetDatabase.Refresh();
        Debug.Log("GameObject image saved to " + savePath);
    }

    private Bounds CalculateBounds(GameObject gameObject)
    {
        var bounds = new Bounds(gameObject.transform.position, Vector3.zero);
        foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }
}
