using LiquidVolumeFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillBehaviour : MonoBehaviour
{
    private LiquidVolume liquidVolume;
    [SerializeField] private GameObject spillHere;
    [SerializeField] private ParticleSystem spilledWater;

    private bool isSpilling = false;
    private Vector3 spillPosition;
    private float spillAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        liquidVolume = GetComponent<LiquidVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        isSpilling = liquidVolume.GetSpillPoint(out spillPosition, out spillAmount);
        
        if (spilledWater.isStopped)
        {
            if (isSpilling)
            {
                spilledWater.Play();
                spillHere.SetActive(true);
                UpdateSpilling();
            }
        }
        else
        {
            if (!isSpilling)
            {
                spilledWater.Stop();
                spillHere.SetActive(false);
            }
            UpdateSpilling();
            spillHere.transform.position = spillPosition;
        }
    }

    private void UpdateSpilling()
    {
        spilledWater.transform.localScale = new Vector3(spillAmount, spillAmount, spillAmount) * 0.2f;
        spilledWater.transform.position = spillPosition;
        liquidVolume.level -= spillAmount * 0.1f;
    }
}
