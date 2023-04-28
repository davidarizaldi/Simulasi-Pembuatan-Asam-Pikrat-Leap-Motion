using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelect;

    public static int selectedLevel = 0;
    public static int localeID = 0;
    private bool localeIsChanging;

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

    public void ChangeLocale()
    {
        if (localeIsChanging) return;
        if (localeID == 0)
        {
            StartCoroutine(SetLocale(1));
            localeID = 1;
        } else
        {
            StartCoroutine(SetLocale(0));
            localeID = 0;
        }
    }

    IEnumerator SetLocale(int id)
    {
        localeIsChanging = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        localeIsChanging = false;
    }
}
