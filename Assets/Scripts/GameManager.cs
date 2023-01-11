using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainFlask;
    [SerializeField] Canvas ObjectiveHud;

    private LiquidVolume mainFlaskLV;
    private float phenolML;
    private float sulfurML;
    private float nitricML;
    private float waterML;
    private float temp;

    private int practicumState;

    // Start is called before the first frame update
    void Start()
    {
        mainFlaskLV = mainFlask.GetComponentInChildren<LiquidVolume>();
        practicumState = 1;

        UpdateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            phenolML = 0.01f / 0.002f;
            ObjectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud(practicumState, new float[] { phenolML, sulfurML, 1.0f, 1.0f });
        }
    }

    public void UpdateLevel()
    {
        phenolML = mainFlaskLV.liquidLayers[0].amount / 0.002f;
        sulfurML = mainFlaskLV.liquidLayers[1].amount / 0.002f;
        nitricML = mainFlaskLV.liquidLayers[2].amount / 0.002f;
        waterML = mainFlaskLV.liquidLayers[3].amount / 0.002f;
        ObjectiveHud.GetComponent<PracticumHudUIHandler>().UpdateObjectiveHud(practicumState, new float[] { phenolML, sulfurML, 0.0f, 0.0f });
    }
}
