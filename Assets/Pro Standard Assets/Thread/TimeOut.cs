using UnityEngine;
using System.Collections;

public class TimeOut 
{
	
	private float mBeforTime;
	private float mTimeOut = 15.0f;
	
	public void SetTimeOut(float timeOut)
	{
		mTimeOut = timeOut;
	}
	
	public TimeOut()
	{
		mBeforTime = Time.time;
		
	}
	
	public bool CheckTimeout()
    {
		float now = Time.time;

		if ( (now - mBeforTime) > mTimeOut )
        {
			// timeout
			return true;
		}

		return false;
	}
}

public delegate void DownloadTimeOutEventHandler();
