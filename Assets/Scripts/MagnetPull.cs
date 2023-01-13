using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPull : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject magneticObject;

    private Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = magneticObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(pos, transform.position) < 0.04f)
        {
            rb.AddForce(pos - transform.position * 1.0f);
        }
    }
}
