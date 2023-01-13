using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpin : MonoBehaviour
{
    private Vector3 rotateChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateChange * Time.deltaTime, Space.World);
    }

    public void UpdateRPM(float rpm)
    {
        rotateChange = new Vector3(0, (rpm * 6.0f), 0);
    }
}
