using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HotplateBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject magnet;
    [SerializeField] private TMP_Text rpmDisplay;
    private GameManager gameManager;

    private float rpm = 0.0f;
    private float temp = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpin(float eulerAngle)
    {
        rpm = eulerAngle * 8;
        UpdateDisplay();
        magnet.GetComponent<MagnetSpinPull>().UpdateRPM(rpm);

        if (rpm > 200.0f)
        {
            gameManager.SetStirred(true);
        }
        else
        {
            gameManager.SetStirred(false);
        }
    }

    public void UpdateHeat(float eulerAngle)
    {
        temp = eulerAngle * 3 + 25.0f;

        if (temp > 50.0f)
        {
            gameManager.SetHeated(true);
        }
        else
        {
            gameManager.SetHeated(false);
        }
    }

    void UpdateDisplay()
    {
        rpmDisplay.text = rpm.ToString("0000");
    }
}
