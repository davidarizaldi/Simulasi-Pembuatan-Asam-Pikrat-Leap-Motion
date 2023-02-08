using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPosition : MonoBehaviour
{
    [SerializeField] GameObject erlenmeyer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "InErlenmeyerTrigger")
        {
            MountToErlenmeyer(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "InErlenmeyerTrigger")
        {
            MountToErlenmeyer(false);
        }
    }

    void MountToErlenmeyer(bool mount)
    {
        if (mount)
        {
            transform.parent = erlenmeyer.transform;
        }
        else
        {
            transform.parent = null;
        }
    }
}
