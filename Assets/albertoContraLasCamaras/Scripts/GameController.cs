using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private PauseMenuUI pauseMenu;
    private bool paused = false;

    private void OnEnable() {
        Alberso.onPlayerDie += GameOver;
    }

    private void OnDisable() {
        Alberso.onPlayerDie -= GameOver;
    }

    private void TogglePause() {
        if (paused) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }

        paused = !paused;
    }

    public void TogglePauseMenu() {
        TogglePause();
        pauseMenu.TogglePauseMenu(paused);
    }

    public void GameExit() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver() {
        TogglePause();
        menuButton.SetActive(false);
    }
}
