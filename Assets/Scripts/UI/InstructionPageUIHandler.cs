using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionPageUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text instructionText;
    
    private int page = 0;
    private static readonly string[] instructions =
    {
        "lmao",
        "xd"
    };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavMainMenuPage()
    {
        SceneManager.LoadScene(0);
    }

    public void NextPage()
    {
        if (page != 1)
        {
            page++;
            UpdatePage();
        }
    }

    public void BackPage()
    {
        if (page != 0)
        {
            page--;
            UpdatePage();
        }
    }

    void UpdatePage()
    {
        instructionText.SetText(instructions[page]);
    }
}
