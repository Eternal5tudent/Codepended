using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    [SerializeField] private Transform leftSideOrigin;
    [SerializeField] private Transform rightSideOrigin;
    [SerializeField] private UI_Numberpad leftNumberPad;
    [SerializeField] private UI_Numberpad rightNumberPad;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    public Vector3 LeftSideOrigin { get { return leftSideOrigin.position; } }
    public Vector3 RightSideOrigin { get { return rightSideOrigin.position; } }
    public UI_Numberpad LeftNumberPad { get { return leftNumberPad; } }
    public UI_Numberpad RightNumberPad { get { return rightNumberPad; } }

    public void EnableLoseScreen()
    {
        loseScreen.SetActive(true);
        GameController.Instance.PauseGame();
    }

    public void EnableWinScreen()
    {
        winScreen.SetActive(true);
        GameController.Instance.PauseGame();
    }

    public void PauseGame()
    {
        GameController.Instance.PauseGame();
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void UnpauseGame()
    {
        GameController.Instance.UnPauseGame();
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void MainMenu()
    {
        GameController.Instance.ChangeScene("MainMenu");
    }

    public void ShowVictoryScreen()
    {
        winScreen.SetActive(true);
    }

    public void ShowDefeatScreen()
    {
        loseScreen.SetActive(true);
    }

    public void HidePauseButton()
    {
        pauseButton.SetActive(false);
    }
}

public enum UI_Side { Left, Right }
