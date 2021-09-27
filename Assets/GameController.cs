using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField] AudioClip gameWonSound;
    public void ChangeScene(string sceneName)
    {
        UnPauseGame();
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        IEnumerator GameOver_Cor()
        {
            UI_Manager.Instance.UnpauseGame();
            UI_Manager.Instance.HidePauseButton();
            yield return new WaitForSeconds(1.5f);
            PauseGame();
            UI_Manager.Instance.ShowDefeatScreen();
        }
        StartCoroutine(GameOver_Cor());
    }

    public void GameWon()
    {
        AudioManager.Instance.PlaySFX(gameWonSound);
        UI_Manager.Instance.UnpauseGame();
        PauseGame();
        UI_Manager.Instance.HidePauseButton();
        UI_Manager.Instance.ShowVictoryScreen();
    }


}
