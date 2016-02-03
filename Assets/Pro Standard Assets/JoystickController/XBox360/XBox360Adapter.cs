using UnityEngine;
using System.Collections;

public class XBox360Adapter : IAdapter
{
    IJoystick mIJoystick;

    public XBox360Adapter(string name, IJoystick js)
    {
        controller = name;

        mIJoystick = js;
    }

    void IAdapter.CurrentAxis(string xname, float x, string yname, float y)
    {
        if (xname == JoystickController.X_Axis && yname == JoystickController.Y_Axis)
            mIJoystick.LAxis(x, y);

        else if (xname == JoystickController.axis_4th && yname == JoystickController.axis_5th)
            mIJoystick.RAxis(x, y);

        else if (yname == JoystickController.axis_6th) 
        {
            if (y > 0.9f) mIJoystick.LButtonLeft();
            else if (y < -0.9f) mIJoystick.LButtonRight();
        }
        else if (xname == JoystickController.axis_7th)
        {
            if (x > 0.9f) mIJoystick.LButtonDown();
            else if (x < -0.9f) mIJoystick.LButtonTop();
        }
    }

    void IAdapter.CurrentButton(string name)
    {
        if (name == JoystickController.joystick_button_3)
            mIJoystick.RButtonUp();
        else if (name == JoystickController.joystick_button_2)
            mIJoystick.RButtonLeft();
        else if (name == JoystickController.joystick_button_1)
            mIJoystick.RButtonRight();
        else if (name == JoystickController.joystick_button_0)
            mIJoystick.RButtonDown();
        else if (name == JoystickController.joystick_button_5)
            mIJoystick.RButtonBehind();
    }

    string _controllername;

    public string controller
    {
        get
        {
            return _controllername;
        }
        set
        {
            _controllername = value;
        }
    }
}
