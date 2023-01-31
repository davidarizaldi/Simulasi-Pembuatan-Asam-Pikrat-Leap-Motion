using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskPosition : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.SetOnHotplate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "OnHotplateTrigger")
        {
            gameManager.SetOnHotplate(true);
        }
        if (other.transform.name == "OnIceBathTrigger")
        {
            gameManager.SetOnIceBath(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "OnHotplateTrigger")
        {
            gameManager.SetOnHotplate(false);
        }
        if (other.transform.name == "OnIceBathTrigger")
        {
            gameManager.SetOnIceBath(false);
        }
    }
}
