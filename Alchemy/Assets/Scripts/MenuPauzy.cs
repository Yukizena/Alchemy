using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPauzy : MonoBehaviour
{
    public static bool isPaused=false;

    public GameObject menupauzy;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
                Pause();
        }
    }
    public void Resume()
    {
        menupauzy.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void Awake()
    {
        Resume();
    }
    void Pause()
    {
        menupauzy.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void gotomainmenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }






}
