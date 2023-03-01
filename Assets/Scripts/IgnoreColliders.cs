using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliders : MonoBehaviour
{
    [SerializeField] private GameObject[] ignoredObjects;
    private Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        colliders = transform.GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            foreach (GameObject ignored in ignoredObjects)
            {
                Physics.IgnoreCollision(col, ignored.transform.GetComponent<Collider>());
            }
        }
    }
}
