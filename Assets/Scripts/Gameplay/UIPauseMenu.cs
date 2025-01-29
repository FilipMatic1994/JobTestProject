using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    public void ResumeGame()
    {
        player.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
