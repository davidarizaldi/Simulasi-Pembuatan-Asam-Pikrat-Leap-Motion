using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCounter : MonoBehaviour
{
    public int counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "Water Particle" || other.name == "Water Particle(Clone)")
        {
            counter += 1;
        }
        else if (other.name == "Water Particle Medium Variant" || other.name == "Water Particle Medium Variant(Clone)")
        {
            counter += 10;
        }
        Destroy(other);
    }
}
