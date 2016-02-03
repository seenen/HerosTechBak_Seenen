using UnityEngine;
using System.Collections;
using System.Threading;

/// <summary>
/// 线程事件
/// </summary>
public delegate void ThreadHandle();

/// <summary>
/// 基础类
/// </summary>
public class MFThread : System.IDisposable
{
    private static int iThreadRef = 0;

    /// <summary>
    /// 运行标识.
    /// </summary>
    protected bool mIsRun;

    /// <summary>
    /// 线程休息时间.
    /// </summary>
    protected int mSleepTime = 0;

    /// <summary>
    /// 线程名.
    /// </summary>
    string mThreadName = string.Empty;

    /// <summary>
    /// 线程.
    /// </summary>
    Thread mThread = null;
    /// <summary>
    /// 线程超时辅助类
    /// </summary>
    private TimeOutHelper mTimeOutHelper = null;
    /// <summary>
    /// 创建线程超时辅助类
    /// </summary>
    /// <param name="timeOut">超时时间 </param>
    /// <param name="timeOutEventHandler"></param>
    public void SetTimeOutHelper(float timeOut,DownloadTimeOutEventHandler timeOutEventHandler)
    {
        if (timeOut <= 0.0001 && (timeOutEventHandler == null))
        {
            return;
        }
        mTimeOutHelper = new GameObject("TimeOutHelper").AddComponent<TimeOutHelper>();
        mTimeOutHelper.Init(timeOut, timeOutEventHandler);
    }


    public void DestroyTimeOutHelper()
    {
        if (mTimeOutHelper != null)
        {
            GameObject.DestroyObject(mTimeOutHelper.gameObject);
            mTimeOutHelper = null;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="handle">执行回调</param>
    /// <param name="sleeptime">0 表示执行一次,否则执行到线程主动停止</param>
    public MFThread(ThreadHandle handle, int sleeptime = 0)
    {
        this.mThreadName = handle.ToString() + iThreadRef.ToString();
        this.mSleepTime = sleeptime;
        this.m_ThreadEvent = handle;

        iThreadRef++;
    }

    #region 代理.
    protected event ThreadHandle m_ThreadEvent;

    protected virtual void BeginWork()
    {
        Debuger.Log("MFThread.BeginWork Enter" + mThreadName + " " + mSleepTime.ToString());

        try
        {
            if (mSleepTime == 0)
            {
                if (m_ThreadEvent != null)
                {
                    m_ThreadEvent();
                }
            }
            else
            {
                while (mIsRun)
                {
                    if (!mThread.IsAlive)
                    {
                        mIsRun = false;
                       
                        break;
                    }

                    if (m_ThreadEvent != null)
                        m_ThreadEvent();

                    Thread.Sleep(mSleepTime);
                }
            }
        }
        catch (System.ArgumentOutOfRangeException e)
        {
            Debug.LogException(e);
        }
        catch (ThreadAbortException e)
        {
            Debuger.Log("Thread - caught ThreadAbortException - resetting.");

            Debuger.Log(string.Format("Exception message: {0}", e.Message));

            Debug.LogException(e);

            Thread.ResetAbort();
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }

        Thread.Sleep(200);

        Debuger.Log("MFThread.BeginWork Quit" + mThreadName);

		mThread = null;

    }
    #endregion

    /// <summary>
    /// 线程启动
    /// </summary>
    public virtual void StartThread()
    {
        Debuger.Log("MFThread.StartThread " + mThreadName);

        mIsRun = true;

        try
        {
            mThread = new Thread(new ThreadStart(BeginWork));
            mThread.IsBackground = true;
            mThread.Name = mThreadName;
            mThread.Start();
        }
        catch (ThreadStartException e)
        {
            Debug.LogException(e);
        }
        catch (ThreadStateException e)
        {
            Debug.LogException(e);
        }
        catch (System.OutOfMemoryException e)
        {
            Debug.LogException(e);
        }
        catch (System.ArgumentNullException e)
        {
            Debug.LogException(e);
        }
    }
    
    /// <summary>
    /// 线程终止
    /// </summary>
    public virtual void StopThread()
    {
        if (mThread == null)
            return;

        Debuger.Log("MFThread.StopThread " + mThreadName);
        
        mIsRun = false;

		return;

        try
        {
            if (mThread.IsAlive)
            {
//				Debuger.Log("0");
//				Thread.Sleep(100);
//				Debuger.Log("1");
//                mThread.Abort();
//				Debuger.Log("2");
//				mThread.Join();
//				Debuger.Log("3");
			}
			Debuger.Log("4");
			mThread = null;
        }
        catch (System.Security.SecurityException e)
        {
            Debug.LogException(e);
            mThread = null;
        }
        catch (ThreadAbortException e)
        {
            Debug.LogException(e);
            mThread = null;
        }
        finally
        {
            Debuger.Log("Do Some Clean");
            mThread = null;
            
        }

        return;
    }

    public void Dispose()
    {
        Debuger.Log("MFThread.Dispose ");

        StopThread();
        DestroyTimeOutHelper();
        try
        {
            //System.GC.SuppressFinalize(this);
        }
        catch (System.ArgumentNullException e)
        {
            Debug.LogException(e);
        }
    }

    
}
