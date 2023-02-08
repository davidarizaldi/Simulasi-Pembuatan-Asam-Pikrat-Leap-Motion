using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpinPull : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject magneticObject;

    private Vector3 pos;
    private Vector3 rotateChange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = magneticObject.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(pos, transform.position) < 0.04f)
        {
            transform.Rotate(rotateChange * Time.deltaTime, Space.World);
            rb.AddForce(pos - transform.position * 1.0f);
        }
    }

    public void UpdateRPM(float rpm)
    {
        rotateChange = new Vector3(0, (rpm * 6.0f), 0);
    }
}
