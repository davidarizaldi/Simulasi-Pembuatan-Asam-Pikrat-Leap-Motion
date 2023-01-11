using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PracticumHudUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text[] objectiveText;

    private Objective[,] objectives =
        {
            {
                new Objective("Phenol", 5, "g"),
                new Objective("Sulfuric Acid", 7, "mL"),
                new Objective("Stirred"),
                new Objective("Phenol")
            },
            {
                new Objective("On Ice Bath"),
                new Objective("Stirred"),
                new Objective("Nitric Acid", 20, "mL"),
                new Objective()
            }
        };

    private class Objective
    {
        public string name;
        public float target;
        public string akhiran;
        public bool done;
        
        public Objective(string name, float target, string akhiran)
        {
            this.name = name;
            this.target = target;
            this.akhiran = akhiran;
        }

        public Objective(string name, bool done = false) : this(name, 0.0f, "")
        {
            this.done = done;
        }

        public Objective()
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateObjectiveHud(int state, float[] objProgress)
    {
        state--;
        for (int i=0; i<4; i++)
        {
            Objective objective = objectives[state, i];
            if (objective.target != 0.0f)
            {
                objectiveText[i].SetText(objective.name + " " + objProgress[i] + objective.akhiran + "/" + objective.target + objective.akhiran);
            }
            else
            {
                objectiveText[i].SetText(objective.name + " (" + (objProgress[i] == 1.0f ? "OK" : "X") + ")");
            }
        }
    }
}
