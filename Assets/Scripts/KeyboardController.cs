using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(GameManager.Instance.gameObject);
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!GameManager.practicumRunning)
            {
                Destroy(GameManager.Instance.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
