using LiquidVolumeFX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaringBehaviour : MonoBehaviour
{
    private LiquidVolume lv;
    private Collider[] childrenColliders;
    [SerializeField] private GameObject excessFlask;

    [HideInInspector] public float[] mLAmounts;
    private float mLSum;
    private readonly float maxML = 50;
    private readonly float maxVolume = 0.261799387799f;
    private float multiplier;
    private float waterCounter;

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

        InvokeRepeating(nameof(FilterOut), 0.15f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if (mLSum < maxML)
        {
            if (other.CompareTag("Water Particle"))
            {
                AddAmount(other);
                Destroy(other);
            }
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
        double volumeAmount0 = mLAmounts[0] * multiplier;
        float heightPerML = CalcHeight(volumeAmount0) / mLAmounts[0];
        if (float.IsNaN(heightPerML))
            heightPerML = 0.0f;
        lv.liquidLayers[0].amount = heightPerML * mLAmounts[0];

        double volumeSum = mLSum * multiplier;
        float height = CalcHeight(volumeSum);
        if (float.IsNaN(height))
            height = 0.0f;
        lv.liquidLayers[1].amount = height - heightPerML * mLAmounts[0];
        lv.UpdateLayers();
        GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLevels();
    }

    float CalcHeight(double volume)
    {
        double height = Math.Cbrt((12 * volume) / Math.PI);
        return (float)height;
    }

    void FilterOut()
    {
        if (mLAmounts[1] > 0)
        {
            mLAmounts[1]--;
            if (waterCounter < 46.4f)
            {
                mLSum--;
                excessFlask.GetComponentInChildren<LiquidVolume>().liquidLayers[0].amount += 0.002f;
                excessFlask.GetComponentInChildren<LiquidVolume>().UpdateLayers();
                waterCounter++;
            }
            else
            {
                mLAmounts[0]++;
                waterCounter -= 46.4f;
            }
            UpdateLevel();
        }
    }
}
