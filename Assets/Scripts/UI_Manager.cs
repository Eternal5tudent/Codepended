using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    [SerializeField] private WorldCanvas worldCanvas;
    [SerializeField] private Transform leftSideOrigin;
    [SerializeField] private Transform rightSideOrigin;
    [SerializeField] private UI_Numberpad leftNumberPad;
    [SerializeField] private UI_Numberpad rightNumberPad;

    public Vector3 LeftSideOrigin { get { return leftSideOrigin.position; } }
    public Vector3 RightSideOrigin { get { return rightSideOrigin.position; } }
    public UI_Numberpad LeftNumberPad { get { return leftNumberPad; } }
    public UI_Numberpad RightNumberPad { get { return rightNumberPad; } }
}

public enum UI_Side { Left, Right }
