using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CenterPopupUIHandler : MonoBehaviour
{
    [SerializeField] TMP_Text popupText;
    private readonly int popupDuration = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowObjectivesCompleted()
    {
        popupText.SetText("Objectives completed!");
        gameObject.SetActive(true);

        yield return new WaitForSeconds(popupDuration);
        gameObject.SetActive(false);
    }

    public IEnumerator ShowWaitFor(int minutes)
    {
        popupText.SetText("Objectives completed!");
        gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration);

        if (minutes <= 60)
        {
            popupText.SetText("Waiting for " + minutes + "\nminutes.");
        }
        else if (minutes > 60)
        {
            popupText.SetText("Waiting for " + (minutes / 60) + "\nhours");
        }

        yield return new WaitForSeconds(popupDuration);
        gameObject.SetActive(false);
    }
}
