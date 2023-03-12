using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearSaveButton : MonoBehaviour
{
    private Image bg;
    private TextMeshProUGUI text;
    private void Awake()
    {
        bg = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        if(!SaveManager.Instance.HasSavedAnythingYet())
        {
            gameObject.SetActive(false);
        }
    }
    public void ClearSave()
    {
        Debug.Log("CLEAR!");
        SaveManager.Instance.ClearSave();
        ClearSaveButtonAnimation();
    }

    private void ClearSaveButtonAnimation() 
    {
        text.text = "Data cleared";
        bg.color = Color.white;
    }
}
