using LiquidVolumeFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillBehaviour : MonoBehaviour
{
    private LiquidVolume liquidVolume;
    [SerializeField] private ParticleSystem waterParticle;
    [SerializeField] private ParticleSystem waterParticleMedium;
    [SerializeField] private float levelPerML; // FlorenceFlask250 = 0.002544f; GraduatedCylinder100 = 0.010111f;
    [SerializeField] private float underSomeLevel; // FlorenceFlask250 = 0; GraduatedCylinder100 = 0.05f;
    [SerializeField] private float levelPerML2; // FlorenceFlask250 = 0; GraduatedCylinder100 = 0.005f;

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
            if (spillAmount < levelPerML2 * 10)
            {
                Instantiate(waterParticle, spillPosition, waterParticle.transform.rotation);
                liquidVolume.level -= levelPerML2;
            }
            else
            {
                Instantiate(waterParticleMedium, spillPosition, waterParticle.transform.rotation);
                liquidVolume.level -= levelPerML2 * 10;
            }
        }
        else
        {
            if (spillAmount < levelPerML * 10)
            {
                Instantiate(waterParticle, spillPosition, waterParticle.transform.rotation);
                liquidVolume.level -= levelPerML;
            }
            else
            {
                Instantiate(waterParticleMedium, spillPosition, waterParticle.transform.rotation);
                liquidVolume.level -= levelPerML * 10;
            }
        }
    }
}
