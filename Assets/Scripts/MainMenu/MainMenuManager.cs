using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIMainMenuPages = new List<GameObject>();

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetMainMenuPage(int _index)
    {
        for(int i = 0; i < UIMainMenuPages.Count; i++)
        {
            if (i == _index) UIMainMenuPages[i].SetActive(true);
            else UIMainMenuPages[i].SetActive(false);
        }
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
