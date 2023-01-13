using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PracticumHudUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text[] objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateObjectiveHud()
    {
        for (int i = 0; i < GameManager.objectives.GetLength(1); i++)
        {
            Objective objective = GameManager.objectives[GameManager.practicumStep, i];
            if (objective.nama == null)
            {
                objectiveText[i].SetText("");
            }
            else if (objective.target != 0.0f)
            {
                objectiveText[i].SetText(objective.nama + " " + GameManager.mainFlaskLevels[objective.id] + objective.akhiran + "/" + objective.target + objective.akhiran);
            }
            else
            {
                objectiveText[i].SetText(objective.nama + " (" + (objective.isDone ? "OK" : "X") + ")");
            }
        }
    }
}
