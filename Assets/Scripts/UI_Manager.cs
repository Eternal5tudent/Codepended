using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    [SerializeField] private WorldCanvas worldCanvas;

    public WorldCanvas WorldCanvas { get { return worldCanvas; } }
}
