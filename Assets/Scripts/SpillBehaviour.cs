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
    [SerializeField] private bool isPrimaryFlask;

    private bool isSpilling = false;
    private Vector3 spillPosition;
    private Vector3 lastSpillPos;
    
    // Start is called before the first frame update
    void Start()
    {
        liquidVolume = GetComponent<LiquidVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        isSpilling = liquidVolume.GetSpillPoint(out spillPosition);

        if (isSpilling)
        {
            SpawnWater(spillPosition);
            lastSpillPos = spillPosition;
        }
        else if (Vector3.Dot(transform.up, Vector3.down) > 0.4f)
        {
            if (liquidVolume.level > 0)
            {
                SpawnWater(lastSpillPos);
            }
        }
    }

    void SpawnWater(Vector3 spillPosition)
    {
        if (isPrimaryFlask)
        {
            Instantiate(waterParticle, spillPosition, waterParticle.transform.rotation);
            int layerCount = liquidVolume.liquidLayers.Length;
            for (int i = 0; i < layerCount; i++)
            {
                liquidVolume.liquidLayers[i].amount -= levelPerML * (liquidVolume.liquidLayers[i].amount / liquidVolume.level);
            }
            liquidVolume.UpdateLayers();
            return;
        }
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
