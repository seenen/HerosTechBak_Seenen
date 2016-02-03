using UnityEngine;
using System.Collections;

public class JoystickMgr
{
    GameObject go;

    JoystickController jc;

    public JoystickMgr()
    {
        go = new GameObject("__JoystickController");

        jc = go.AddComponent<JoystickController>();
    }


    public bool Init(IDevice device)
    {
        if (device == null)
            return false;

        jc.StartDevice(device);

        return true;

    }

    public bool Connect(string name, IJoystick joystick)
    {
        if (joystick == null)
            return false;

#if UNITY_IPHONE
		{
			MOGAAdapter adapter = new MOGAAdapter(name, joystick);
			
			jc.SetAdapter(adapter);
		}
#else
        {
            XBox360Adapter adapter = new XBox360Adapter(name, joystick);

            jc.SetAdapter(adapter);
        }
#endif

        return true;
    }

    public void Disconnect()
    {
        jc.SetAdapter(null);
    }
    
}
