using UnityEngine;
using System.Collections;

public class TimeOutHelper : MFMonoBehaviour {

    private TimeOut mTimeOut = null;
    private DownloadTimeOutEventHandler mTimeOutEventHandler = null;
    protected override void MFAwake()
    {
    }

    protected override void MFStart()
    {
    }

    
    protected override void MFOnEnable()
    {
    }

    protected override void MFOnDisable()
    {
    }

    protected override void MFOnDestroy()
    {
        mTimeOut = null;
        mTimeOutEventHandler = null;
    }

    void Update()
    {
        if (mTimeOut.CheckTimeout())
        {
            if (mTimeOutEventHandler != null)
            {
                mTimeOutEventHandler();
                mTimeOutEventHandler = null;
            }
        }
    }

    
    public void Init(float timeOut,DownloadTimeOutEventHandler timeOutEventHandler)
    {
        mTimeOut = new TimeOut();
        mTimeOut.SetTimeOut(timeOut);
        mTimeOutEventHandler = timeOutEventHandler;
    }
}
