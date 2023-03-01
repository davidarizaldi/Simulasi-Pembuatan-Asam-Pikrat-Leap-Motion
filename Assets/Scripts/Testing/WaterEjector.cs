using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEjector : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterParticle;
    [SerializeField] private ParticleSystem phenolParticle;
    [SerializeField] private ParticleSystem sulfuricParticle;
    [SerializeField] private ParticleSystem nitricParticle;
    [SerializeField] private ParticleSystem picricParticle;

    private static int pourWater;
    private static int pourPicric;

    // Update is called once per frame
    void Update()
    {
        if (pourWater > 0)
        {
            pourWater--;
            Instantiate(waterParticle, transform.position, waterParticle.transform.rotation);
        }

        if (pourPicric > 0)
        {
            pourPicric--;
            Instantiate(picricParticle, transform.position, waterParticle.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(waterParticle, transform.position, waterParticle.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(phenolParticle, transform.position, waterParticle.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(sulfuricParticle, transform.position, waterParticle.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(nitricParticle, transform.position, waterParticle.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(picricParticle, transform.position, waterParticle.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            pourWater = 200;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = new Vector3(0.0f, 1.08f, -0.25f);
            pourPicric = 50;
        }
    }
}
