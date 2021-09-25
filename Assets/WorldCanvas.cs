using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldCanvas : Singleton<WorldCanvas>
{
    [SerializeField] private TextBox textBoxPrefab;
    public GameObject CreateTextElement(string text, Vector3 worldPosition)
    {
        TextBox newTextBox = Instantiate(textBoxPrefab.gameObject, worldPosition, Quaternion.identity, transform).GetComponent<TextBox>();
        newTextBox.RenderText(text);
        return newTextBox.gameObject;
    }

}
