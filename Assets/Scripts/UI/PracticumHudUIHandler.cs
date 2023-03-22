using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PracticumHudUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text[] objectiveText;
    private GuideHudUIHandler guideHud;

    // Start is called before the first frame update
    void Start()
    {
        guideHud = GameObject.Find("Guide HUD").GetComponent<GuideHudUIHandler>();
    }

    public void UpdateObjectiveHud()
    {
        if (GameManager.popupIsActive || !GameManager.practicumRunning)
        {
            return;
        }
        guideHud.UpdateGuideHud();
        for (int i = 0; i < GameManager.objectives.GetLength(1); i++)
        {
            Objective objective = GameManager.objectives[GameManager.practicumStep, i];
            if (objective.nama == null)
            {
                objectiveText[i].SetText("");
            }
            else if (objective.id == 4)
            {
                objectiveText[i].SetText(objective.nama + " " + GameManager.filterLevels[0] + objective.akhiran + "/" + objective.target + objective.akhiran);
            }
            else if (objective.target != 0.0f)
            {
                objectiveText[i].SetText(objective.nama + " " + (int)(objective.id == 7 ? GameManager.temperature : GameManager.mainFlaskLevels[objective.id]) + objective.akhiran + "/" + objective.target + objective.akhiran);
            }
            else
            {
                objectiveText[i].SetText(objective.nama + " (" + (objective.isDone ? "OK" : "X") + ")");
            }

            objectiveText[i].color = (objective.isDone ? Color.green : Color.white);
            if (objective.nama == "Heat Off" && GameManager.temperature > 25)
            {
                objectiveText[i].color = Color.red;
            }
        }
    }
}
