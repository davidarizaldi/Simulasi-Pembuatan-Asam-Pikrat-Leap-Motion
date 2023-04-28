using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GuideHudUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text guideText;

    public void UpdateGuideHud()
    {
        string text = LocalizationSettings.StringDatabase.GetLocalizedString("Guides", "GUIDE_" + GameManager.practicumStep);
        guideText.SetText(text);
    }
}
