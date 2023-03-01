using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningMovement : MonoBehaviour
{
    [SerializeField] private GameObject rotatorSpin;
    [SerializeField] private GameObject rotatorHeat;
    private const float rotateChange = 0.01f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad4))
        {
            //rotatorSpin.transform.Rotate(new Vector3(0, 0, -rotateChange) * Time.deltaTime, Space.Self);
            rotatorSpin.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, rotateChange));
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            //rotatorSpin.transform.Rotate(new Vector3(0, 0, rotateChange) * Time.deltaTime, Space.Self);
            rotatorSpin.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -rotateChange));
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            //rotatorHeat.transform.Rotate(new Vector3(0, 0, -rotateChange) * Time.deltaTime, Space.Self);
            rotatorHeat.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, rotateChange));
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            //rotatorHeat.transform.Rotate(new Vector3(0, 0, rotateChange) * Time.deltaTime, Space.Self);
            rotatorHeat.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -rotateChange));
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            rotatorSpin.GetComponent<Rigidbody>().angularDrag = 1.0f;
            rotatorHeat.GetComponent<Rigidbody>().angularDrag = 1.0f;
        }
    }
}
