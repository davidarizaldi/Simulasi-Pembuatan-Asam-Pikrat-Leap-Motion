using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class InstructionPageUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private VideoPlayer videoPlayer;
    
    private int page = 0;
    private static readonly string[] instructions =
    {
        "This simulation captures your hand gestures to interact with the objects in the practicum.\n\nRaise your hands up to be captured.",
        "You can lift objects with your hand by pinching the object.",
        "You could also rotate the object around while the object is being carried.\n\nRotating the flask will make the substance in it spills.",
        "Instructions are displayed in the top left corner of the screen to be followed.\n\nThe goals of each scene is displayed in the top right corner of the screen.",
        "Do not spill any substance on the table!\n\nSpilling any of it counts as failing the practicum as the substances used in this practicum is dangerous.",
        "Good luck!"
    };
    private static readonly string[] videoSources =
    {
        "Demo Praktikum 1 - Tangan Leap Motion",
        "Demo Praktikum 2 - Mengangkat Objek",
        "Demo Praktikum 3 - Memutar Objek",
        "Demo Praktikum 3 - Memutar Objek",
        "Demo Praktikum 3 - Memutar Objek",
        "Demo Praktikum 3 - Memutar Objek"
    };
    
    // Start is called before the first frame update
    void Start()
    {
        UpdatePage();
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
        if (page != (instructions.Length - 1))
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
        videoPlayer.clip = Resources.Load<VideoClip>(videoSources[page]);
    }
}
