using UnityEngine;
using UnityEngine.EventSystems;

public class StickyNote : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Range(1,10)]
    public float hoverSpeed;

    public RectTransform idleState;
    public RectTransform hoverState;

    private RectTransform myRect;

    private bool isHovered;

    void Start()
    {
        myRect = GetComponent<RectTransform>();
    }

    public void Update()
    {
        MoveNote();
    }

    public void MoveNote()
    {
        switch (isHovered)
        {
            case true:
                transform.localPosition = Vector3.Lerp(myRect.localPosition, hoverState.localPosition, hoverSpeed * Time.deltaTime);
                break;
            case false:
                transform.localPosition = Vector3.Lerp(myRect.localPosition, idleState.localPosition, hoverSpeed * Time.deltaTime);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}