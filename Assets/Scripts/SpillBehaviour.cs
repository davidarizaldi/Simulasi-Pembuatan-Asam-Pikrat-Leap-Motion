using LiquidVolumeFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillBehaviour : MonoBehaviour
{
    private LiquidVolume liquidVolume;
    [SerializeField] private ParticleSystem waterParticle;
    [SerializeField] private float levelPerML; // FlorenceFlask250 = 0.002f; GraduatedCylinder100 = 0.010111f; Erlenmeyer250 = 0.002f
    [SerializeField] private float underSomeLevel; // GraduatedCylinder100 = 0.05f;
    [SerializeField] private float levelPerML2; // GraduatedCylinder100 = 0.005f;

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

        if (isSpilling)
        {
            spawnWater();
        }
    }

    private void spawnWater()
    {
        if (liquidVolume.level <= underSomeLevel)
        {
            Instantiate(waterParticle, spillPosition, waterParticle.transform.rotation);
            liquidVolume.level -= levelPerML2;
        }
        else
        {
            Instantiate(waterParticle, spillPosition, waterParticle.transform.rotation);
            liquidVolume.level -= levelPerML;
        }
    }
}
