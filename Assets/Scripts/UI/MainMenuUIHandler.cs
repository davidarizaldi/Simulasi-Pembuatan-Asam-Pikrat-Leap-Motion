using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelect;

    public static int selectedLevel = 0;

    public void StartSimulation(int level)
    {
        SceneManager.LoadScene(2);
        selectedLevel = level;
    }

    public void NavLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void ReturnToMenu()
    {
        levelSelect.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void NavInstructionPage()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
