using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class QuitGame : MonoBehaviour
{
    public UnityEvent quitEvent;
    public int duration;

    public void Quit()
    {
        QuitFunction();
    }

    async void QuitFunction()
    {
        quitEvent?.Invoke();

        await Task.Delay(duration * 1000);

#if UNITY_EDITOR
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }
}
