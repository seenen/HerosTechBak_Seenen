using UnityEngine;
using System.Collections;

public interface IJoystick
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void LAxis(float x, float y);

    void LButtonUp();
    void LButtonLeft();
    void LButtonDown();
    void LButtonRight();
    void LButtonBehind();
    void LButtonTop();

    void RAxis(float x, float y);

    void RButtonUp();
    void RButtonLeft();
    void RButtonDown();
    void RButtonRight();
    void RButtonBehind();
    void RButtonTop();

}
