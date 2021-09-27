using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Numberpad : MonoBehaviour
{
    [SerializeField] private TMP_Text input;
    [SerializeField] AudioClip dialpadSFX;

    public Numberpad connectedNumberPad;
    private int typedInput;
    public void Open(Numberpad numberpad)
    {
        connectedNumberPad = numberpad;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        input.text = "";
        connectedNumberPad = null;
        gameObject.SetActive(false);
    }

    public void AcceptInput()
    {
        print("evaluating...");
        typedInput = int.Parse(input.text);
        connectedNumberPad.EvaluateInput(typedInput);
        Close();
    }

    public void ConnectWithNumberPad(Numberpad numberpad)
    {
        connectedNumberPad = numberpad;
    }

    public void TypeNumber(string num)
    {
        if (input.text.Length < 4)
        {
            input.text += num;
            AudioManager.Instance.PlaySFX(dialpadSFX);
        }
    }
}
