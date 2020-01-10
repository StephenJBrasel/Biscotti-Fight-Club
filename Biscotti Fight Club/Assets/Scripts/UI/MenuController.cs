using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController FPcontrol;

    private bool isPaused;

    private void Start()
    {
        if (FPcontrol == null) FPcontrol = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                FPcontrol.SetCursorLock(false);
                isPaused = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeGame()
    {
        FPcontrol.SetCursorLock(true);
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        FPcontrol.SetCursorLock(false);
        GameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //public void MainMenu()
    //{
    //    SceneManager.LoadScene();
    //}

    public void QuitButton()
    {
        Application.Quit();
    }
}
