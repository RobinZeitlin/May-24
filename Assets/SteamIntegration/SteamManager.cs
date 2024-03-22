using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SteamManager : MonoBehaviour
{
    [Header("Steam")]

    public uint appId;
    [Header("Events")]
    public UnityEvent SteamConnected;

    public static SteamManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void OnEnable()
    {
        if (SteamClient.IsValid)
        {
            SteamConnected?.Invoke();
            Debug.Log("Welcome to Honker " + SteamClient.Name);
            return;
        }

        try
        {
            SteamClient.Init(appId);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
