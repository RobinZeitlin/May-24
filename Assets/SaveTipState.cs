using UnityEngine;
using UnityEngine.Events;

public class SaveTipState : MonoBehaviour
{
    bool saveTipState = false;

    public ClickedSprite clickedSprite;
    public UnityEvent DeActivateObjects;

    void Start()
    {
        if (PlayerPrefs.HasKey("SaveTipState"))
        {
            saveTipState = PlayerPrefs.GetInt("SaveTipState", 0) == 1;

            if (saveTipState)
            {
                DeactivateObjects();
            }
        }
    }

    void DeactivateObjects()
    {
        SaveTipStateToggle();

        if (clickedSprite != null)
        {
            clickedSprite.ChangeSprite();
        }

        if (DeActivateObjects != null)
        {
            DeActivateObjects.Invoke();
        }
    }

    public void SaveTipStateToggle()
    {
        saveTipState = true;
        PlayerPrefs.SetInt("SaveTipState", 1);
    }
}
