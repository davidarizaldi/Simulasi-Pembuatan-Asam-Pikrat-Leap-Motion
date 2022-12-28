using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPull : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Vector3 pos = new Vector3(0, 0.82f, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
