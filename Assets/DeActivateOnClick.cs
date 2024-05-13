using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActivateOnClick : MonoBehaviour
{
    public void DeActivate(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
