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

    public static float[] mainFlaskLevels;
    public static float[] filterLevels;
    public static float temperature;
    public static bool isStirring;
    public static int practicumStep;
    public static bool practicumRunning;
    public static bool popupIsActive;

    private const float targetTemp = 75.0f;

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

    public static Objective[,] objectives;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        UpdateLevels();
        LevelSelect(MainMenuUIHandler.selectedLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (practicumRunning && AllIsDone())
        {
            practicumStep += 1;
            StartCoroutine(TransitionState(practicumStep));
        }
    }

    void Initialize()
    {
        mainFlaskLevels = new float[4];
        filterLevels = filter.GetComponentInChildren<SaringBehaviour>().mLAmounts;
        temperature = 25.0f;
        isStirring = false;
        practicumStep = 0;
        practicumRunning = true;
        popupIsActive = false;

        ob = GameObject.Find("Objects").GetComponent<Objects>();
        mainFlaskLV = mainFlask.GetComponentInChildren<LiquidVolume>();

        objectives = new Objective[,]
        {
            {
                new Objective(0, "Phenol", 5, "g"),
                new Objective(),
                new Objective(),
                new Objective()
            },
            {
                new Objective(1, "Sulfuric Acid", 7, "mL"),
                new Objective(),
                new Objective(),
                new Objective()
            },
            {
                new Objective("Stirred"),
                new Objective(7, "Heated", targetTemp, "°"),
                new Objective(),
                new Objective()
            },
            {
                new Objective(7, "Heat Off", 25, "°"),
                new Objective("On Ice Bath"),
                new Objective("On Hotplate", true),
                new Objective()
            },
            {
                new Objective("Stirred", true),
                new Objective(7, "Heat Off", 25, "°", true),
                new Objective(2, "Nitric Acid", 20, "mL"),
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
    }

    void LevelSelect(int selectedLevel)
    {
        switch(selectedLevel)
        {
            case 7:
                mainFlaskLV.alpha = 0.15f;
                mainFlaskLV.liquidLayers[3].miscible = true;
                goto case 6;
            case 6:
                if (selectedLevel == 6)
                {
                    ob.objects[9].transform.localEulerAngles = new Vector3(ob.objects[9].transform.localEulerAngles.x, ob.objects[9].transform.localEulerAngles.y, 30);
                    ob.objects[10].transform.localEulerAngles = new Vector3(ob.objects[10].transform.localEulerAngles.x, ob.objects[10].transform.localEulerAngles.y, 30);
                }
                mainFlaskLV.liquidLayers[0].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                mainFlaskLV.liquidLayers[1].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                mainFlaskLV.liquidLayers[2].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                mainFlaskLV.liquidLayers[3].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                ob.objects[4].SetActive(false);
                goto case 5;
            case 5:
                mainFlaskLV.liquidLayers[2].amount = 0.04f;
                if (selectedLevel == 5)
                {
                    mainFlaskLV.liquidLayers[0].color = new Color(0.47f, 0.235f, 0.078f, 0.5f);
                    mainFlaskLV.liquidLayers[1].color = new Color(0.47f, 0.235f, 0.078f, 0.5f);
                    mainFlaskLV.liquidLayers[2].color = new Color(0.47f, 0.235f, 0.078f, 0.5f);
                    ob.objects[9].transform.localEulerAngles = new Vector3(ob.objects[9].transform.localEulerAngles.x, ob.objects[9].transform.localEulerAngles.y, 30);
                }
                
                ob.objects[5].SetActive(false);
                ob.objects[6].SetActive(false);
                ob.objects[14].SetActive(false);
                goto case 4;
            case 4:
                ob.objects[0].transform.position = new(0.0f, 1.07f, 0.05f);
                ob.objects[4].transform.position = new(0.0f, 1.07f, 0.05f);
                if (selectedLevel == 4)
                {
                    ob.objects[9].transform.localEulerAngles = new Vector3(ob.objects[9].transform.localEulerAngles.x, ob.objects[9].transform.localEulerAngles.y, 30);
                }

                mainFlaskLV.liquidLayers[2].miscible = true;
                ob.objects[13].SetActive(false);
                goto case 3;
            case 3:
                if (selectedLevel == 3)
                {
                    ob.objects[9].transform.localEulerAngles = new Vector3(ob.objects[9].transform.localEulerAngles.x, ob.objects[9].transform.localEulerAngles.y, 30);
                    ob.objects[10].transform.localEulerAngles = new Vector3(ob.objects[10].transform.localEulerAngles.x, ob.objects[10].transform.localEulerAngles.y, 30);
                }
                else
                {
                    mainFlaskLV.liquidLayers[0].murkiness = 0.0f;
                    mainFlaskLV.liquidLayers[0].color.a = 0.039f;
                    mainFlaskLV.liquidLayers[0].miscible = true;
                    mainFlaskLV.liquidLayers[1].miscible = true;
                }
                goto case 2;
            case 2:
                mainFlaskLV.liquidLayers[1].amount = 0.014f;

                ob.objects[2].SetActive(false);
                ob.objects[3].SetActive(false);
                ob.objects[12].SetActive(false);
                goto case 1;
            case 1:
                mainFlaskLV.liquidLayers[0].amount = 0.01f;

                ob.objects[1].SetActive(false);
                ob.objects[11].SetActive(false);
                goto case 0;
            case 0:
                UpdateLevels();
                practicumStep = selectedLevel;
                mainFlaskLV.UpdateLayers();
                StartCoroutine(TransitionState(practicumStep));
                break;
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
        if (practicumStep == 7)
        {
            if (filterLevels[0] >= objectives[7,0].target)
            {
                objectives[7, 0].isDone = true;
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
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                yield return new WaitForSeconds(popUpHandler.popupDuration);

                ob.objects[1].SetActive(false);
                ob.objects[11].SetActive(false);
                ob.objects[2].transform.position = new Vector3(0.0f, ob.objects[2].transform.position.y, -0.2f);
                ob.objects[3].transform.position = new Vector3(-0.13f, ob.objects[3].transform.position.y, -0.2f);
                ob.objects[12].transform.position = new Vector3(0.0f, ob.objects[12].transform.position.y, -0.275f);
                break;
            case 2:
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                yield return new WaitForSeconds(popUpHandler.popupDuration);

                ob.objects[2].SetActive(false);
                ob.objects[3].SetActive(false);
                ob.objects[12].SetActive(false);
                break;
            case 3:
                StartCoroutine(popUpHandler.ShowWaitFor(30));
                yield return new WaitForSeconds(popUpHandler.popupDuration + 30 / popUpHandler.minPerSec);

                mainFlaskLV.liquidLayers[0].murkiness = 0.0f;
                mainFlaskLV.liquidLayers[0].color.a = 0.039f;
                mainFlaskLV.liquidLayers[0].miscible = true;
                mainFlaskLV.liquidLayers[1].miscible = true;
                ob.objects[4].transform.position = new Vector3(0.0f, ob.objects[4].transform.position.y, -0.25f);
                ob.objects[13].transform.position = new Vector3(0.0f, ob.objects[13].transform.position.y, -0.4f);
                break;
            case 4:
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                SetStirred(isStirring);
                SetHeat(temperature);
                yield return new WaitForSeconds(popUpHandler.popupDuration);

                mainFlaskLV.liquidLayers[2].miscible = true;
                ob.objects[5].transform.position = new Vector3(0.0f, ob.objects[5].transform.position.y, -0.2f);
                ob.objects[6].transform.position = new Vector3(-0.13f, ob.objects[6].transform.position.y, -0.2f);
                ob.objects[14].transform.position = new Vector3(0.0f, ob.objects[14].transform.position.y, -0.275f);
                ob.objects[13].SetActive(false);
                break;
            case 5:
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                yield return new WaitForSeconds(popUpHandler.popupDuration);

                ob.objects[5].SetActive(false);
                ob.objects[6].SetActive(false);
                ob.objects[14].SetActive(false);
                break;
            case 6:
                StartCoroutine(popUpHandler.ShowWaitFor(120));
                yield return new WaitForSeconds(popUpHandler.popupDuration + 120 / popUpHandler.minPerSec);

                mainFlaskLV.liquidLayers[0].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                mainFlaskLV.liquidLayers[1].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                mainFlaskLV.liquidLayers[2].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                mainFlaskLV.liquidLayers[3].color = new Color(1.0f, 0.862f, 0.0f, 1.0f);
                mainFlaskLV.alpha = 0.15f;
                mainFlaskLV.UpdateLayers();
                mainFlaskLV.liquidLayers[3].miscible = true;
                ob.objects[4].SetActive(false);
                ob.objects[7].transform.position = new Vector3(0.0f, ob.objects[7].transform.position.y, -0.2f);
                ob.objects[15].transform.position = new Vector3(0.0f, ob.objects[15].transform.position.y, -0.275f);
                break;
            case 7:
                StartCoroutine(popUpHandler.ShowObjectivesCompleted());
                yield return new WaitForSeconds(popUpHandler.popupDuration);

                ob.objects[7].SetActive(false);
                ob.objects[8].transform.position = new Vector3(0.0f, ob.objects[8].transform.position.y, -0.25f);
                ob.objects[15].SetActive(false);
                break;
            case 8:
                StartCoroutine(popUpHandler.ShowSuccess());
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.1f);
        objectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud();
    }

    public void SetStirred(bool value)
    {
        isStirring = value;
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
        temperature = value;

        if (temperature > targetTemp)
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
        practicumRunning = false;
        liquidName = liquidName.Remove(liquidName.Length - 24);
        StartCoroutine(centerPopup.GetComponent<CenterPopupUIHandler>().ShowFailed(liquidName));
    }
}
