using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class PauseMenu : MonoBehaviour
{

    private bool gamePaused = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        backgroundMusic.Pause();
        gamePaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        backgroundMusic.Play();
        gamePaused = false;
    }

    public void Restart()
    {
        Resume();
        CarController.EndGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Resume();
        CarController.EndGame();
        SceneManager.LoadScene("TrackSelection");
    }
}
