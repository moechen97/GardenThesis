using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearSaveButton : MonoBehaviour
{
    private Image button_bg;
    private TextMeshProUGUI button_text;
    [SerializeField] private GameObject tapThisText;
    [SerializeField] private TextMeshProUGUI skipTutorialButtonText;
    private void Awake()
    {
        button_bg = GetComponent<Image>();
        button_text = GetComponentInChildren<TextMeshProUGUI>();
        if(!SaveManager.Instance.HasSavedAnythingYet())
        {
            gameObject.SetActive(false);
            tapThisText.SetActive(false);
        }
    }
    public void ClearSave()
    {
        ClearSaveButtonAnimation();
        SaveManager.Instance.ClearSave();       
    }

    private void ClearSaveButtonAnimation() 
    {
        button_text.text = "Data cleared";
        button_bg.color = Color.white;
        tapThisText.SetActive(false);
        skipTutorialButtonText.text = "Skip Tutorial";
    }
}
