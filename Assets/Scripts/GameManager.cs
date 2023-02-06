using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainFlask;
    [SerializeField] private GameObject secondFlask;
    [SerializeField] private Canvas objectiveHud;
    [SerializeField] private Canvas centerPopup;
    private LiquidVolume mainFlaskLV;
    private LiquidVolume secondFlaskLV;
    public static GameManager Instance;

    public static float[] mainFlaskLevels = new float[4];
    public static float secondFlaskLevel;
    public static float temp;
    public static int practicumStep;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static Objective[,] objectives =
    {
        {
            new Objective(0, "Phenol", 5, "g"),
            new Objective(1, "Sulfuric Acid", 7, "mL"),
            new Objective("Stirred"),
            new Objective("Heated")
        },
        {
            new Objective("Heat Off"),
            new Objective("On Ice Bath"),
            new Objective("On Hotplate", true),
            new Objective("Stirred", true)
            
        },
        {
            new Objective(2, "Nitric Acid", 20, "mL"),
            new Objective("Stirred", true),
            new Objective("No Reaction Left", true),
            new Objective()
        },
        {
            new Objective("Off Ice Bath"),
            new Objective("Stirred", true),
            new Objective("Heated"),
            new Objective()
        },
        {
            new Objective("Stir Off"),
            new Objective("Heat Off"),
            new Objective(3, "Water", 200, "mL"),
            new Objective()
        },
        {
            new Objective(4, "Picric Acid", 5, "g"),
            new Objective(),
            new Objective(),
            new Objective()
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        mainFlaskLV = mainFlask.GetComponentInChildren<LiquidVolume>();
        secondFlaskLV = secondFlask.GetComponentInChildren<LiquidVolume>();
        practicumStep = 0;
        UpdateLevels();
    }

    // Update is called once per frame
    void Update()
    {
        if (AllIsDone())
        {
            practicumStep += 1;
            switch (practicumStep)
            {
                case 1:
                    StartCoroutine(centerPopup.GetComponent<CenterPopupUIHandler>().ShowWaitFor(30));
                    mainFlaskLV.liquidLayers[0].color.a = 0.039f;
                    mainFlaskLV.liquidLayers[0].miscible = true;
                    mainFlaskLV.liquidLayers[1].miscible = true;
                    break;
                case 2:
                    StartCoroutine(centerPopup.GetComponent<CenterPopupUIHandler>().ShowObjectivesCompleted());
                    mainFlaskLV.liquidLayers[2].miscible = true;
                    break;
                case 3:
                    StartCoroutine(centerPopup.GetComponent<CenterPopupUIHandler>().ShowObjectivesCompleted());
                    break;
                case 4:
                    StartCoroutine(centerPopup.GetComponent<CenterPopupUIHandler>().ShowWaitFor(120));
                    mainFlaskLV.liquidLayers[0].color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                    mainFlaskLV.liquidLayers[1].color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                    mainFlaskLV.liquidLayers[2].color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                    mainFlaskLV.liquidLayers[3].color = new Color(0.0f, 1.0f, 0.25f, 0.5f);
                    mainFlaskLV.liquidLayers[3].murkiness = 0.1f;
                    mainFlaskLV.UpdateLayers();
                    mainFlaskLV.liquidLayers[3].miscible = true;
                    break;
                case 5:
                    StartCoroutine(centerPopup.GetComponent<CenterPopupUIHandler>().ShowObjectivesCompleted());
                    UpdateSecLevel();
                    break;
                default:
                    break;
            }
            objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
        }
    }

    public void UpdateLevels()
    {
        for (int i = 0; i < 4; i++)
        {
            mainFlaskLevels[i] = mainFlaskLV.liquidLayers[i].amount / 0.002f;
        }

        CheckLiquidObjectives();
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    public void UpdateSecLevel()
    {
        secondFlaskLevel = secondFlaskLV.liquidLayers[0].amount / 0.002f;
        if (practicumStep == 5)
        {
            Objective objective = objectives[practicumStep, 0];
            if (secondFlaskLevel >= objective.target)
            {
                objective.isDone = true;
            }
        }

        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    void CheckLiquidObjectives()
    {
        for (int i = 0; i < objectives.GetLength(1); i++)
        {
            Objective objective = objectives[practicumStep, i];
            if (objective.target != 0.0f)
            {
                if (mainFlaskLevels[objective.id] >= objective.target)
                {
                    objective.isDone = true;
                }
            }
        }
    }

    bool AllIsDone()
    {
        bool allIsDone = true;
        for (int i = objectives.GetLength(1) - 1; i >= 0; i--)
        {
            if (!objectives[practicumStep, i].isDone)
            {
                allIsDone = false;
                break;
            }
        }
        return allIsDone;
    }

    public void SetStirred(bool value)
    {
        for (int i = 0; i < objectives.GetLength(1); i++)
        {
            if (objectives[practicumStep, i].nama == "Stirred")
            {
                objectives[practicumStep, i].isDone = value;
            }
            else if (objectives[practicumStep, i].nama == "Stir Off")
            {
                objectives[practicumStep, i].isDone = !value;
            }
        }
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    public void SetHeated(bool value)
    {
        for (int i = 0; i < objectives.GetLength(1); i++)
        {
            if (objectives[practicumStep, i].nama == "Heated")
            {
                objectives[practicumStep, i].isDone = value;
            }
            else if (objectives[practicumStep, i].nama == "Heat Off")
            {
                objectives[practicumStep, i].isDone = !value;
            }
        }
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    public void SetOnHotplate(bool value)
    {
        for (int i = 0; i < objectives.GetLength(1); i++)
        {
            if (objectives[practicumStep, i].nama == "On Hotplate")
            {
                objectives[practicumStep, i].isDone = value;
            }
        }
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    public void SetOnIceBath(bool value)
    {
        for (int i = 0; i < objectives.GetLength(1); i++)
        {
            if (objectives[practicumStep, i].nama == "On Ice Bath")
            {
                objectives[practicumStep, i].isDone = value;
            }
            if (objectives[practicumStep, i].nama == "Off Ice Bath")
            {
                objectives[practicumStep, i].isDone = !value;
            }
        }
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    public void CheckReaction(float volume)
    {
        for (int i = 0; i < objectives.GetLength(1); i++)
        {
            if (objectives[practicumStep, i].nama == "No Reaction Left")
            {
                objectives[practicumStep, i].isDone = (volume == 0.0f);
            }
        }
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }
}
