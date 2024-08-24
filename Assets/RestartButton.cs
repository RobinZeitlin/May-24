using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

    public void RestartPlayerPrefs()
    {
        PlayerPrefs.SetInt("Level", 1);
        ShowMessage("SaveFile deleted. Restart the game.");
        Application.Quit();
    }

    public static void ShowMessage(string message)
    {
        // Show the message box with the error message
        MessageBox(IntPtr.Zero, message, "Error", 0x00000000);
    }
}
