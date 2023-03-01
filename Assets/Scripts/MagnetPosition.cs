using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPosition : MonoBehaviour
{
    [SerializeField] private GameObject erlenmeyer;
    
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
