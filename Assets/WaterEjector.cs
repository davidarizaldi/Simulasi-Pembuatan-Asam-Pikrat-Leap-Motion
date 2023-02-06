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

    private static int water = 200;
    private bool pour = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pour && water > 0)
        {
            water--;
            Instantiate(waterParticle, transform.position, waterParticle.transform.rotation);
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
            if (pour == false)
            {
                pour = true;
            }
            else
            {
                pour = false;
            }
        }
    }
}
