using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void LoadGame()
    {
        GameController.Instance.ChangeScene("Game");
    }
}
