using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskPosition : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject iceBath;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "OnHotplateTrigger")
        {
            gameManager.SetOnHotplate(true);
        }
        if (other.transform.name == "OnIceBathTrigger")
        {
            gameManager.SetOnIceBath(true);
            MountToIcebath(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "OnHotplateTrigger")
        {
            gameManager.SetOnHotplate(false);
        }
        if (other.transform.name == "OnIceBathTrigger")
        {
            gameManager.SetOnIceBath(false);
            MountToIcebath(false);
        }
    }

    void MountToIcebath(bool mount)
    {
        if (mount)
        {
            transform.parent = iceBath.transform;
        }
        else
        {
            transform.parent = null;
        }
    }
}
