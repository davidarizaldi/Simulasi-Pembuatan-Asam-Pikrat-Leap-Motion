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
    [SerializeField] private VideoPlayer videoPlayerPov;

    private int page = 0;
    private static readonly string[] instructions =
    {
        "This simulation captures your hand gestures to interact with the objects in the practicum.\nRaise your hands up to be captured.",
        "You can lift objects with your hand by pinching the object.",
        "You could also rotate the object around while the object is being carried.\nRotating the flask will make the substance in it spills.",
        "Instructions are displayed in the top left corner of the screen to be followed.\nThe goals of each scene is displayed in the top right corner of the screen.",
        "Do not spill any substance on the table!\nSpilling any of it counts as failing the practicum as the substances used in this practicum is dangerous.",
        "A voice recognition is available to move the camera to \"close\", \"left\", and \"down\"\nKeyboard inputs can also be used by pressing the \"up\", \"left\", and \"down\" arrows respectively",
        "Good luck!"
    };
    private static readonly string[] videoSources =
    {
        "Demo Praktikum 1 - Tangan Leap Motion",
        "Demo Praktikum 2 - Mengangkat Objek",
        "Demo Praktikum 3 - Memutar Objek",
        "Demo Praktikum 4 - Instruksi Praktikum",
        "Demo Praktikum 5 - Tumpah",
        "Demo Praktikum 6 - Voice Recognition",
        "Demo Praktikum 7 - Good Luck"
    };
    private static readonly string[] videoSourcesPov =
    {
        "PoV Demo Praktikum 1",
        "PoV Demo Praktikum 2",
        "PoV Demo Praktikum 3",
        "PoV Demo Praktikum 4",
        "PoV Demo Praktikum 5",
        "PoV Demo Praktikum 6",
        "PoV Demo Praktikum 7"
    };

    // Start is called before the first frame update
    void Start()
    {
        UpdatePage();
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
        videoPlayerPov.clip = Resources.Load<VideoClip>(videoSourcesPov[page]);
    }
}
