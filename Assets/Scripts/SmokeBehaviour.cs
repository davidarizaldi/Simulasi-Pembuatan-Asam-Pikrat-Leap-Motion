using LiquidVolumeFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    private LiquidVolume lv;

    private float reactionVolume = 0.0f;
    private readonly float maxReactionVolume = 20.0f;
    private readonly float reactionDecay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        lv = transform.GetComponent<LiquidVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        DecayReaction();
    }

    public void NitricAcidAdded()
    {
        reactionVolume += 1.0f;
        lv.smokeEnabled = true;
        UpdateSmoke();
    }

    void DecayReaction()
    {
        if (reactionVolume > 0.0f)
        {
            reactionVolume -= reactionDecay * Time.deltaTime;
            if (reactionVolume < 0.0f)
            {
                reactionVolume = 0.0f;
                lv.smokeEnabled = false;
            }
            UpdateSmoke();
        }
    }

    void UpdateSmoke()
    {
        lv.smokeScale = reactionVolume / maxReactionVolume;
    }
}
