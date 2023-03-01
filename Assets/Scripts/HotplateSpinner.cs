using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotplateSpinner : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        LimitRotation();

        if (transform.hasChanged)
        {
            GetComponentInParent<HotplateBehaviour>().UpdateSpin(transform.localEulerAngles.z);
            transform.hasChanged = false;
        }
    }

    void LimitRotation()
    {
        if (transform.localEulerAngles.z < 360 && transform.localEulerAngles.z > 180)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        if (transform.localEulerAngles.z > 90)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 90);
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
