using LiquidVolumeFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{
    private LiquidVolume lv;
    [SerializeField] private ParticleSystem externalSmoke;
    private GameManager gameManager;

    private static float reactionVolume = 0.0f;
    private const float maxReactionVolume = 20.0f;
    private float reactionPercent = 0.0f;
    private const float reactionSmokeDuration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        lv = transform.GetComponent<LiquidVolume>();
    }

    public void NitricAcidAdded()
    {
        reactionVolume += 1.0f;
        gameManager.CheckReaction(reactionVolume);
        lv.smokeEnabled = true;
        StartCoroutine(ReactionSmokeDecay());
        UpdateSmoke();
    }

    IEnumerator ReactionSmokeDecay()
    {
        yield return new WaitForSeconds(reactionSmokeDuration);
        reactionVolume -= 1.0f;
        gameManager.CheckReaction(reactionVolume);
        if (reactionVolume == 0.0f)
        {
            lv.smokeEnabled = false;
        }
        UpdateSmoke();
    }

    void UpdateSmoke()
    {
        reactionPercent = reactionVolume / maxReactionVolume;

        lv.smokeScale = reactionPercent * 0.25f;
        lv.smokeBaseObscurance = 10.0f - reactionPercent * 10.0f;
        if (lv.smokeBaseObscurance < 0.0f)
        {
            lv.smokeBaseObscurance = 0.0f;
        }

        UpdateExternalSmoke();
    }

    void UpdateExternalSmoke()
    {
        var em = externalSmoke.GetComponent<ParticleSystem>().emission;
        em.rateOverTime = reactionPercent * 40.0f;
    }
}
