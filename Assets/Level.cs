using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(levelMusic);
    }
}
