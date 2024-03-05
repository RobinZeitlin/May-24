using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugReport : MonoBehaviour
{
    public void SendBugReport()
    {
        Debug.Log("Email Button Click");
        string email = "robin.zeitlin@hotmail.com";
        string subject = MyEscapeURL("BugReport");
        string body = MyEscapeURL("Thank_you_for_the_report.");

        string mailtoUrl = $"mailto:{email}?subject={subject}&body={body}";

        Application.OpenURL(mailtoUrl);
    }

    string MyEscapeURL(string url)
    {
        return UnityEngine.Networking.UnityWebRequest.EscapeURL(url);
    }
}
