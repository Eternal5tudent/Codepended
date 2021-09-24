using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Vector2 axisInput_R { get; private set; }
    public Vector2 axisInput_L { get; private set; }

    private void Update()
    {
        float inputX_R = Input.GetAxisRaw("Horizontal_R");
        float inputY_R = Input.GetAxisRaw("Vertical_R");
        axisInput_R = new Vector2(inputX_R, inputY_R);

        float inputX_L = Input.GetAxisRaw("Horizontal_L");
        float inputY_L = Input.GetAxisRaw("Vertical_L");
        axisInput_L = new Vector2(inputX_L, inputY_L);
    }
}
