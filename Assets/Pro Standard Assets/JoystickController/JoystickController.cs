using UnityEngine;
using System.Collections;

public class JoystickController : MonoBehaviour
{
#region Device
    IDevice mIDevice;

    public void StartDevice(IDevice device)
    {
        mIDevice = device;

        StartCoroutine(ControllerDetect());
    }

    IEnumerator ControllerDetect()
    {
        while (true)
        {
            string[] controllers = Input.GetJoystickNames();

            if (controllers != null && controllers.Length > 0)
            {
                if (!bConnect)
                {
                    bConnect = true;

                    mIDevice.OnDevicesFound(controllers);
                }
            }
            else if (controllers == null || controllers.Length == 0)
            {
                if (bConnect)
                {
                    bConnect = false;

                    mIDevice.OnDevicesMissing();
                }
            }

            yield return 10;

        }
    }

    bool bConnect = false;

#endregion

    /// <summary>
	/// This is a joystick detect demo/project. Made by project Team Unity~ from the Entertainment Technology Center at Carnegie Mellon.
	/// The purpose for this demo/project is to understand what is the mapping for you joystick. 
    /// </summary>
#region Joystick
    private string currentButton;
    private string currentAxis;
	private float axisInput;
	
    IAdapter mIAdapter;

    public void SetAdapter(IAdapter adapter)
    {
        mIAdapter = adapter;

        mIDevice.OnConnect(mIAdapter.controller);

    }

	// Update is called once per frame
	void Update () 
	{
		getAxis();
		getButton();

        if (mIAdapter == null)
            return;

        mIAdapter.CurrentAxis(X_Axis, Input.GetAxisRaw(X_Axis), Y_Axis, Input.GetAxisRaw(Y_Axis));

        mIAdapter.CurrentAxis(axis_3rd, Input.GetAxisRaw(axis_3rd), axis_4th, Input.GetAxisRaw(axis_4th));

        mIAdapter.CurrentAxis(axis_5th, Input.GetAxisRaw(axis_5th), axis_6th, Input.GetAxisRaw(axis_6th));

        mIAdapter.CurrentAxis(axis_7th, Input.GetAxisRaw(axis_7th), axis_8th, Input.GetAxisRaw(axis_8th));

        if (!string.IsNullOrEmpty(currentButton))
        {
            mIAdapter.CurrentButton(currentButton);

            currentButton = string.Empty;
        }
	}

    public static string X_Axis = "X axis";
    public static string Y_Axis = "Y axis";
    public static string axis_3rd = "3rd axis";
    public static string axis_4th = "4th axis";
    public static string axis_5th = "5th axis";
    public static string axis_6th = "6th axis";
    public static string axis_7th = "7th axis";
    public static string axis_8th = "8th axis";

    /// <summary>
	/// Get Axis data of the joysick
	/// </summary>
	void getAxis()
	{
        if (Input.GetAxisRaw(X_Axis) > 0.3 || Input.GetAxisRaw(X_Axis) < -0.3)
		{
            currentAxis = X_Axis;
            axisInput = Input.GetAxisRaw(X_Axis);
		}

        if (Input.GetAxisRaw(Y_Axis) > 0.3 || Input.GetAxisRaw(Y_Axis) < -0.3)
		{
            currentAxis = Y_Axis;
            axisInput = Input.GetAxisRaw(Y_Axis);
		}

        if (Input.GetAxisRaw(axis_3rd) > 0.3 || Input.GetAxisRaw(axis_3rd) < -0.3)
		{
            currentAxis = axis_3rd;
            axisInput = Input.GetAxisRaw(axis_3rd);
		}

        if (Input.GetAxisRaw(axis_4th) > 0.3 || Input.GetAxisRaw(axis_4th) < -0.3)
		{
            currentAxis = axis_4th;
            axisInput = Input.GetAxisRaw(axis_4th);
		}

        if (Input.GetAxisRaw(axis_5th) > 0.3 || Input.GetAxisRaw(axis_5th) < -0.3)
		{
            currentAxis = axis_5th;
            axisInput = Input.GetAxisRaw(axis_5th);
		}

        if (Input.GetAxisRaw(axis_6th) > 0.3 || Input.GetAxisRaw(axis_6th) < -0.3)
		{
            currentAxis = axis_6th;
            axisInput = Input.GetAxisRaw(axis_6th);
		}

        if (Input.GetAxisRaw(axis_7th) > 0.3 || Input.GetAxisRaw(axis_7th) < -0.3)
		{
            currentAxis = axis_7th;
            axisInput = Input.GetAxisRaw(axis_7th);
		}

        if (Input.GetAxisRaw(axis_8th) > 0.3 || Input.GetAxisRaw(axis_8th) < -0.3)
		{
            currentAxis = axis_8th;
            axisInput = Input.GetAxisRaw(axis_8th);
		}
	}

    public static string joystick_button_0 = "joystick button 0";
    public static string joystick_button_1 = "joystick button 1";
    public static string joystick_button_2 = "joystick button 2";
    public static string joystick_button_3 = "joystick button 3";
    public static string joystick_button_4 = "joystick button 4";
    public static string joystick_button_5 = "joystick button 5";
    public static string joystick_button_6 = "joystick button 6";
    public static string joystick_button_7 = "joystick button 7";
    public static string joystick_button_8 = "joystick button 8";
    public static string joystick_button_9 = "joystick button 9";
    public static string joystick_button_10 = "joystick button 10";
    public static string joystick_button_11 = "joystick button 11";
    public static string joystick_button_12 = "joystick button 12";
    public static string joystick_button_13 = "joystick button 13";
    public static string joystick_button_14 = "joystick button 14";
    public static string joystick_button_15 = "joystick button 15";
    public static string joystick_button_16 = "joystick button 16";
    public static string joystick_button_17 = "joystick button 17";
    public static string joystick_button_18 = "joystick button 18";
    public static string joystick_button_19 = "joystick button 19";

	/// <summary>
	/// get the button data of the joystick
	/// </summary>
	void getButton()
	{
        if (Input.GetButton(joystick_button_0))
            currentButton = joystick_button_0;

        if (Input.GetButton(joystick_button_1))
            currentButton = joystick_button_1;
		   
		if(Input.GetButton(joystick_button_2))
			currentButton = joystick_button_2;
		   
		if(Input.GetButton(joystick_button_3))
			currentButton = joystick_button_3;
		   
		if(Input.GetButton(joystick_button_4))
			currentButton = joystick_button_4;
		   
		if(Input.GetButton(joystick_button_5))
			currentButton = joystick_button_5;
		   
		if(Input.GetButton(joystick_button_6))
			currentButton = joystick_button_6;
		   
		if(Input.GetButton(joystick_button_7))
			currentButton = joystick_button_7;
		   
		if(Input.GetButton(joystick_button_8))
			currentButton = joystick_button_8;
		   
		if(Input.GetButton(joystick_button_9))
			currentButton = joystick_button_9;
		   
		if(Input.GetButton(joystick_button_10))
			currentButton = joystick_button_10;
		   
		if(Input.GetButton(joystick_button_11))
			currentButton = joystick_button_11;
		   
		if(Input.GetButton(joystick_button_12))
			currentButton = joystick_button_12;
		   
		if(Input.GetButton(joystick_button_13))
			currentButton = joystick_button_13;
		   
		if(Input.GetButton(joystick_button_14))
			currentButton = joystick_button_14;
		
		if(Input.GetButton(joystick_button_15))
			currentButton = joystick_button_15;
		
		if(Input.GetButton(joystick_button_16))
            currentButton = joystick_button_16;

        if (Input.GetButton(joystick_button_17))
            currentButton = joystick_button_17;

        if (Input.GetButton(joystick_button_18))
            currentButton = joystick_button_18;

        if (Input.GetButton(joystick_button_19))
            currentButton = joystick_button_19;	   
	}
#endregion

    /// <summary>
	/// show the data onGUI
	/// </summary>
	void OnGUI()
	{
        //GUI.TextArea(new Rect(400, 100, 250, 50), "Current Button : " + currentButton);
        //GUI.TextArea(new Rect(400, 200, 250, 50), "Current Axis : " + currentAxis);
        //GUI.TextArea(new Rect(400, 300, 250, 50), "Axis Value : " + axisInput);
	}
}
