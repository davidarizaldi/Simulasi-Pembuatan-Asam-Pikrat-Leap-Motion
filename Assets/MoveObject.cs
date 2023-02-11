using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private GameObject mainFlask;
    [SerializeField] private GameObject iceBath;
    [SerializeField] private GameObject magnet;

    private readonly Vector3 onHotplate = new(0.0f, 1.1f, 0.0f);
    private Vector3 targetFlask;
    private Vector3 targetIceBath;
    private bool flaskMoving = false;
    private bool iceBathMoving = false;
    public float speed = 1.0f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        targetFlask = mainFlask.transform.position;
        targetIceBath = iceBath.transform.position;
        time = 0.5f / speed;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (flaskMoving)
        {
            mainFlask.transform.position = Vector3.MoveTowards(mainFlask.transform.position, targetFlask, step);
        }
        if (iceBathMoving)
        {
            iceBath.transform.position = Vector3.MoveTowards(iceBath.transform.position, targetIceBath, step);
        }

        if (Input.GetKey(KeyCode.I))
        {
            StartCoroutine(ToIceBath());
        }
        if (Input.GetKey(KeyCode.H))
        {
            StartCoroutine(ToHotplate());
        }
        if (Input.GetKey(KeyCode.B))
        {
            StartCoroutine(DoubleTo());
        }
        if (Input.GetKey(KeyCode.R))
        {
            iceBath.SetActive(false);
        }
        if (Input.GetKey(KeyCode.O))
        {
            StartCoroutine(ToOut());
        }
    }

    IEnumerator ToIceBath()
    {
        mainFlask.GetComponent<Rigidbody>().useGravity = false;
        magnet.GetComponent<Rigidbody>().useGravity = false;
        magnet.GetComponent<MagnetSpinPull>().enabled = false;
        targetFlask = mainFlask.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
        flaskMoving = true;
        yield return new WaitForSeconds(time);
        targetFlask = iceBath.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
        yield return new WaitForSeconds(time);
        flaskMoving = false;
        mainFlask.GetComponent<Rigidbody>().useGravity = true;
        magnet.GetComponent<Rigidbody>().useGravity = true;
        magnet.GetComponent<MagnetSpinPull>().enabled = true;
    }

    IEnumerator ToOut()
    {
        mainFlask.GetComponent<Rigidbody>().useGravity = false;
        magnet.GetComponent<Rigidbody>().useGravity = false;
        magnet.GetComponent<MagnetSpinPull>().enabled = false;
        targetFlask = mainFlask.transform.position + new Vector3(0.0f, 0.2f, 0.0f);
        flaskMoving = true;
        yield return new WaitForSeconds(time);
        targetFlask = targetFlask + new Vector3(0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(time);
        flaskMoving = false;
        mainFlask.GetComponent<Rigidbody>().useGravity = true;
        magnet.GetComponent<Rigidbody>().useGravity = true;
        magnet.GetComponent<MagnetSpinPull>().enabled = true;
    }

    IEnumerator ToHotplate()
    {
        mainFlask.GetComponent<Rigidbody>().useGravity = false;
        magnet.GetComponent<Rigidbody>().useGravity = false;
        targetFlask = mainFlask.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
        flaskMoving = true;
        yield return new WaitForSeconds(time);
        targetFlask = onHotplate;
        yield return new WaitForSeconds(time);
        flaskMoving = false;
        mainFlask.GetComponent<Rigidbody>().useGravity = true;
        magnet.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator DoubleTo()
    {
        iceBath.GetComponent<Rigidbody>().isKinematic = true;
        //mainFlask.GetComponent<Rigidbody>().isKinematic = true;
        targetIceBath = iceBath.transform.position + new Vector3(0.0f, 0.15f, 0.0f);
        //targetFlask = mainFlask.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
        iceBathMoving = true;
        //flaskMoving = true;
        yield return new WaitForSeconds(time);
        targetIceBath = onHotplate;
        //targetFlask = onHotplate + new Vector3(0.0f, 0.1f, 0.0f);
        yield return new WaitForSeconds(time);
        iceBathMoving = false;
        //flaskMoving = false;
        iceBath.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.5f);
        iceBath.GetComponent<Rigidbody>().isKinematic = true;
        //mainFlask.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.5f);
        iceBath.GetComponent<Rigidbody>().isKinematic = false;
    }
}
