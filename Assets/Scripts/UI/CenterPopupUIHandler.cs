using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CenterPopupUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private GameObject TintedBackground;

    public readonly float popupDuration = 1.5f;
    public readonly int minPerSec = 10;
    private float timer;
    private float targetTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        popupText.SetText("Objectives completed!");
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration);

        GameManager.popupIsActive = false;
        gameObject.SetActive(false);
    }

    public IEnumerator ShowWaitFor(int minutes)
    {
        GameManager.popupIsActive = true;
        popupText.SetText("Objectives completed!");
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration);

        TintedBackground.SetActive(true);
        StartCountTo(minutes / minPerSec);
        popupText.SetText("Waiting time " + $"{(int)(timer * minPerSec),2}" + "/" + (targetTime * minPerSec) + " minutes.");
        yield return new WaitForSeconds(minutes / minPerSec);

        GameManager.popupIsActive = false;
        gameObject.SetActive(false);
        TintedBackground.SetActive(false);
    }

    public IEnumerator ShowSuccess()
    {
        popupText.SetText("You completed the practicum!");
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration * 2);

        gameObject.SetActive(false);
    }

    public IEnumerator ShowFailed(string liquidName)
    {
        popupText.SetText("You failed the practicum!\n " + liquidName + " is spilled.");
        popupText.rectTransform.sizeDelta = new Vector3(640.0f, 64.0f);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration * 5);

        popupText.rectTransform.sizeDelta = new Vector3(640.0f, 48.0f);
        gameObject.SetActive(false);
    }

    void UpdateWaitingMinutes()
    {
        popupText.SetText("Waiting time: " + $"{(int)(timer * minPerSec), 2}" + "/" + (targetTime * minPerSec) + " minutes.");
    }

    void StartCountTo(float targetTime)
    {
        timer = 0;
        this.targetTime = targetTime;
    }
}
