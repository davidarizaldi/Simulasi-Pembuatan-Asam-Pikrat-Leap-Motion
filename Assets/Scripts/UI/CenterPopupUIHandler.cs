using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class CenterPopupUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private GameObject TintedBackground;

    public readonly float popupDuration = 1.5f;
    public readonly int minPerSec = 10;
    private float timer;
    private float targetTime;
    
    // Update is called once per frame
    void Update()
    {
        if (timer < targetTime)
        {
            timer += Time.deltaTime;
            UpdateWaitingMinutes();
        }
    }

    public IEnumerator ShowObjectivesCompleted()
    {
        GameManager.popupIsActive = true;
        popupText.SetText(LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "OBJECTIVES_COMPLETED"));
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration);

        GameManager.popupIsActive = false;
        gameObject.SetActive(false);
    }

    public IEnumerator ShowWaitFor(int minutes)
    {
        GameManager.popupIsActive = true;
        popupText.SetText(LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "OBJECTIVES_COMPLETED"));
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration);

        TintedBackground.SetActive(true);
        StartCountTo(minutes / minPerSec);
        popupText.SetText(LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "WAITING_TIME") + $"{(int)(timer * minPerSec),2}" + "/" + (targetTime * minPerSec) + LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "MINUTES"));
        yield return new WaitForSeconds(minutes / minPerSec);

        GameManager.popupIsActive = false;
        gameObject.SetActive(false);
        TintedBackground.SetActive(false);
    }

    public IEnumerator ShowSuccess()
    {
        popupText.SetText(LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "PRACTICUM_COMPLETED"));
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration * 2);

        gameObject.SetActive(false);
    }

    public void ShowFailed(string liquidName)
    {
        liquidName = liquidName.Remove(liquidName.Length - 24);
        if (MainMenuUIHandler.localeID == 1)
        {
            switch (liquidName)
            {
                case "Phenol": liquidName = "Fenol"; break;
                case "Sulfuric Acid": liquidName = "Asam Sulfat"; break;
                case "Nitric Acid": liquidName = "Asam Nitrat"; break;
                case "Picric Acid": liquidName = "Asam Pikrat"; break;
                default: break;
            }
        }
        popupText.SetText(liquidName + LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "PRACTICUM_FAILED"));
        popupText.rectTransform.sizeDelta = new Vector3(640.0f, 64.0f);
        gameObject.SetActive(true);
    }

    void UpdateWaitingMinutes()
    {
        popupText.SetText(LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "WAITING_TIME") + $"{(int)(timer * minPerSec), 2}" + "/" + (targetTime * minPerSec) + LocalizationSettings.StringDatabase.GetLocalizedString("HUD Text", "MINUTES"));
    }

    void StartCountTo(float targetTime)
    {
        timer = 0;
        this.targetTime = targetTime;
    }
}
