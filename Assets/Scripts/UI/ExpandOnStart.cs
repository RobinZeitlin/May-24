using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ExpandOnStart : MonoBehaviour
{
    public AnimationCurve scaleCurve;
    public float duration = 1f;

    private float scaleTime;
    private Vector3 startScale;

    public UnityEvent eventSystem;

    void OnEnable()
    {
        startScale = transform.localScale;
        scaleTime = 0;
        eventSystem?.Invoke();
        StartCoroutine(ScaleUp());
    }

    IEnumerator ScaleUp()
    {
        while (scaleTime < 1)
        {
            transform.localScale = startScale * scaleCurve.Evaluate(scaleTime);
            scaleTime += Time.deltaTime * duration;

            yield return null;
        }

        transform.localScale = startScale * scaleCurve.Evaluate(1f);
    }
    private void OnDisable()
    {
        StopCoroutine(ScaleUp());
        transform.localScale = startScale * scaleCurve.Evaluate(1f);
    }
}
