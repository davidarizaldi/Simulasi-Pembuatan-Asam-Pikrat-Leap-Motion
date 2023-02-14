using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDetection : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "Water Particle" || other.name == "Water Particle(Clone)") { }
        else
        {
            gameManager.DangerousLiquidDrop(other.name);
        }
        Destroy(other);
    }
}
