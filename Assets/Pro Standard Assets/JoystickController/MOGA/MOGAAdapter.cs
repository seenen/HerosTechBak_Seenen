using UnityEngine;
using System.Collections;

public class MOGAAdapter : IAdapter
{
    IJoystick mIJoystick;
 
    public MOGAAdapter(string name, IJoystick js)
    {
        controller = name;

        mIJoystick = js;
    }

    public void CurrentAxis(string xname, float x, string yname, float y)
    {
//		Debuger.Log("IDapater.CurrentAxis: " + xname + " " + x + " " + yname + " " + y);

		if (xname == JoystickController.X_Axis && yname == JoystickController.Y_Axis)
            mIJoystick.LAxis(x, y);

        else if (xname == JoystickController.axis_3rd && yname == JoystickController.axis_4th)
            mIJoystick.RAxis(x, y);
    }

	public void CurrentButton(string name)
    {
		Debuger.Log("IDapater.CurrentButton: " + name);

        if (name == JoystickController.joystick_button_4)
            mIJoystick.LButtonTop();
        else if (name == JoystickController.joystick_button_7)
            mIJoystick.LButtonLeft();
        else if (name == JoystickController.joystick_button_5)
            mIJoystick.LButtonRight();
        else if (name == JoystickController.joystick_button_6)
            mIJoystick.LButtonDown();


        else if (name == JoystickController.joystick_button_12)
            mIJoystick.RButtonUp();
        else if (name == JoystickController.joystick_button_15)
            mIJoystick.RButtonLeft();
        else if (name == JoystickController.joystick_button_13)
            mIJoystick.RButtonRight();
        else if (name == JoystickController.joystick_button_14)
            mIJoystick.RButtonDown();
		else if (name == JoystickController.joystick_button_9)
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
