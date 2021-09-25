using System.Collections;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TMP_Text textArea;
    public TMP_Text Text { get { return textArea; } }

    public void RenderText(string text)
    {
        textArea.text = text;
    }
}
