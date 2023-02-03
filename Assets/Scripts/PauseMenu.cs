using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject MenuPause;

    public void ResumeGame()
    {
        Time.timeScale = 1;
        MenuPause.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        MenuPause.SetActive(true);

    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
