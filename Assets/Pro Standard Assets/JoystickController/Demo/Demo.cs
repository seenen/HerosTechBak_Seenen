using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour, IDevice, IJoystick
{


	// Use this for initialization
	void Start()
    {
        Joystick();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

	void OnGUI()
	{
		GUILayout.Space(100);

		GUILayout.Label("----------Devices----------");

        GUILayout.Button(controllername);
	}


    #region Joystick
    JoystickMgr mJoystickMgr;

    void Joystick()
    {
        if (mJoystickMgr == null)
            mJoystickMgr = new JoystickMgr();

        mJoystickMgr.Init(this);
    }

    string controllername = string.Empty;

    public void OnDevicesFound(string[] controllers)
    {
        Debuger.Log("Demo.OnDevicesFound " + controllers[0]);

        mJoystickMgr.Connect(controllers[0], this);
    }

    public void OnDevicesMissing()
    {
        Debuger.Log("Demo.OnDevicesMissing ");

        controllername = string.Empty;
    }

    public void OnConnect(string controllernames)
    {
        Debuger.Log("Demo.OnConnect " + controllernames);

        controllername = controllernames;
    }

    public void OnDisconnect()
    {
        Debuger.Log("Demo.OnDisconnect ");

        controllername = string.Empty;
    }

    #endregion

    #region IJoystick
    public void LAxis(float x, float y)
    {

    }

    public void LButtonUp()
    {
    }

    public void LButtonLeft()
    {
    }

    public void LButtonDown()
    {
    }

    public void LButtonRight()
    {
    }

    public void LButtonBehind()
    {
    }

    public void LButtonTop()
    {
    }

    public void RAxis(float x, float y)
    {
    }

    public void RButtonUp()
    {
    }

    public void RButtonLeft()
    {
    }

    public void RButtonDown()
    {
    }

    public void RButtonRight()
    {
    }

    public void RButtonBehind()
    {
    }

    public void RButtonTop()
    {
    }
    #endregion IJoystick
}
