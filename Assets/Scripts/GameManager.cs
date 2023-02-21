using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainFlask;
    [SerializeField] private GameObject filter;
    [SerializeField] private Canvas objectiveHud;
    [SerializeField] private Canvas centerPopup;
    private LiquidVolume mainFlaskLV;
    public static GameManager Instance;
    private Objects ob;

    public static float[] mainFlaskLevels = new float[4];
    public static float[] filterLevels;
    public static float temp;
    public static int practicumStep;
    public static bool isRunning = true;

    private static readonly float targetTemp = 75.0f;

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
            new Objective(7, "Heated", targetTemp, "°")
        },
        {
            new Objective(7, "Heat Off", 25, "°"),
            new Objective("On Ice Bath"),
            new Objective("On Hotplate", true),
            new Objective("Stirred", true)
            
        },
        {
            new Objective(2, "Nitric Acid", 20, "mL"),
            new Objective("Stirred", true),
            new Objective(7, "Heat Off", 25, "°", true),
            new Objective("No Reaction Left", true)
        },
        {
            new Objective("Off Ice Bath"),
            new Objective("On Hotplate", true),
            new Objective("Stirred", true),
            new Objective(7, "Heated", targetTemp, "°")
        },
        {
            new Objective("Stir Off"),
            new Objective(7, "Heat Off", 25, "°"),
            new Objective(3, "Water", 200, "mL"),
            new Objective()
        },
        {
            new Objective(4, "Picric Acid", 5, "g"),
            new Objective(),
            new Objective(),
            new Objective()
        },
        {
            new Objective(" "),
            new Objective(),
            new Objective(),
            new Objective()
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        ob = GameObject.Find("Objects").GetComponent<Objects>();
        mainFlaskLV = mainFlask.GetComponentInChildren<LiquidVolume>();
        filterLevels = filter.GetComponentInChildren<SaringBehaviour>().mLAmounts;
        UpdateLevels();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning && AllIsDone())
        {
            practicumStep += 1;
            StartCoroutine(TransitionState(practicumStep));
        }
    }

    public void UpdateLevels()
    {
        for (int i = 0; i < 4; i++)
        {
            mainFlaskLevels[i] = mainFlaskLV.liquidLayers[i].amount / 0.002f;
        }
        filterLevels = filter.GetComponentInChildren<SaringBehaviour>().mLAmounts;

        CheckLiquidObjectives();
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    void CheckLiquidObjectives()
    {
        if (practicumStep == 5)
        {
            if (filterLevels[0] >= objectives[5,0].target)
            {
                objectives[5, 0].isDone = true;
            }
            return;
        }
        for (int i = 0; i < objectives.GetLength(1); i++)
        {
            Objective objective = objectives[practicumStep, i];
            if (objective.id >= 0 && objective.id <= 3)
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

    IEnumerator TransitionState(int practicumState)
    {
        CenterPopupUIHandler popUpHandler = centerPopup.GetComponent<CenterPopupUIHandler>();
        
        switch (practicumState)
        {
            case 1:
                StartCoroutine(popUpHandler.ShowWaitFor(30));
                yield return new WaitForSeconds(popUpHandler.popupDuration + 30 / popUpHandler.minPerSec);

                mainFlaskLV.liquidLayers[0].color.a = 0.039f;
                mainFlaskLV.liquidLayers[0].miscible = true;
                mainFlaskLV.liquidLayers[1].miscible = true;
                ob.objects[1].SetActive(false);
                ob.objects[2].SetActive(false);
                ob.objects[3].SetActive(false);
                ob.objects[4].transform.position = new Vector3(0.0f, ob.objects[4].transform.position.y, -0.25f);
                break;
            case 2:
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                yield return new WaitForSeconds(popUpHandler.popupDuration);

                mainFlaskLV.liquidLayers[2].miscible = true;
                ob.objects[5].transform.position = new Vector3(0.0f, ob.objects[5].transform.position.y, -0.2f);
                ob.objects[6].transform.position = new Vector3(-0.13f, ob.objects[6].transform.position.y, -0.2f);
                break;
            case 3:
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                ob.objects[5].SetActive(false);
                ob.objects[6].SetActive(false);
                break;
            case 4:
                StartCoroutine(popUpHandler.ShowWaitFor(120));
                yield return new WaitForSeconds(popUpHandler.popupDuration + 120 / popUpHandler.minPerSec);

                mainFlaskLV.liquidLayers[0].color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                mainFlaskLV.liquidLayers[1].color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                mainFlaskLV.liquidLayers[2].color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                mainFlaskLV.liquidLayers[3].color = new Color(0.0f, 1.0f, 0.25f, 0.5f);
                mainFlaskLV.liquidLayers[3].murkiness = 0.1f;
                mainFlaskLV.UpdateLayers();
                mainFlaskLV.liquidLayers[3].miscible = true;
                ob.objects[4].SetActive(false);
                ob.objects[7].transform.position = new Vector3(0.0f, ob.objects[7].transform.position.y, -0.2f);
                break;
            case 5:
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                ob.objects[7].SetActive(false);
                ob.objects[8].transform.position = new Vector3(0.0f, ob.objects[8].transform.position.y, -0.25f);
                break;
            case 6:
                StartCoroutine(popUpHandler.ShowSuccess());
                break;
            default:
                break;
        }
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
        yield return new WaitForSeconds(0);
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

    public void SetHeat(float value)
    {
        temp = value;

        if (temp > targetTemp)
        {
            SetHeated(true);
        }
        else
        {
            SetHeated(false);
        }
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

    public void DangerousLiquidDrop(string liquidName)
    {
        isRunning = false;
        liquidName = liquidName.Remove(liquidName.Length - 24);
        StartCoroutine(centerPopup.GetComponent<CenterPopupUIHandler>().ShowFailed(liquidName));
    }
}
