using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicalButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClicksound); });
    }
}
