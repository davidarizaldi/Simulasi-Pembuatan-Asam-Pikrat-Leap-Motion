using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HotplateBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject magnet;
    [SerializeField] private TMP_Text rpmDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpin(float rotateChange)
    {
        magnet.GetComponent<MagnetSpin>().rotateChange = new Vector3(0, rotateChange, 0);
    }

    public void UpdateDisplay(float rpm)
    {
        rpmDisplay.text = rpm.ToString("0000");
    }
}
