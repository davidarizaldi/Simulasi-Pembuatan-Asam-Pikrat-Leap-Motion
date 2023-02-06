using LiquidVolumeFX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaringBehaviour : MonoBehaviour
{
    private LiquidVolume lv;
    private Collider[] childrenColliders;

    [SerializeField] private float[] mLAmounts;
    [SerializeField] private float mLSum;
    private readonly float maxML = 100;
    private readonly float maxVolume = 0.261799387799f;
    private float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        lv = transform.parent.GetComponent<LiquidVolume>();

        childrenColliders = transform.parent.transform.parent.GetComponentsInChildren<Collider>();
        foreach (Collider col in childrenColliders)
        {
            if (col != GetComponent<Collider>())
            {
                Physics.IgnoreCollision(col, GetComponent<Collider>());
            }
        }
        Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.Find("Magnet").transform.GetComponent<Collider>());

        multiplier = maxVolume / maxML;
        mLAmounts = new float[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water Particle"))
        {
            AddAmount(other);
            Destroy(other);
        }
    }

    void AddAmount(GameObject other)
    {
        switch (other.name)
        {
            case "Picric Acid Variant(Clone)":
                mLAmounts[0]++;
                mLSum++;
                break;
            case "Picric Acid Particle Variant(Clone)":
                mLAmounts[1]++;
                mLSum++;
                break;
            default:
                break;
        }
        UpdateLevel();
    }

    void UpdateLevel()
    {
        double volumeSum = mLSum * multiplier;
        float heightPerML = CalcHeight(volumeSum) / mLSum;
        lv.liquidLayers[0].amount = heightPerML * mLAmounts[0];
        lv.liquidLayers[1].amount = heightPerML * mLAmounts[1];
        lv.UpdateLayers();
    }

    float CalcHeight(double volume)
    {
        double height = Math.Cbrt((12 * volume) / Math.PI);
        return (float)height;
    }
}
