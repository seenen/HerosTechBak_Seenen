using UnityEngine;
using System.Collections;

public static class BaseGameEntityID
{
    static int m_ID = 0;

    public static int GUID
    {
        get
        {
            int tmp = m_ID;

            m_ID++;

            return tmp;
        }
    }
}

abstract public class BaseGameEntity
{
    //  ʵ��Ψһ��ʶ��(Unity�������GameObject��InstacneID����ʾ)
    int m_ID;

    //  
    void SetID(int val)
    {
        m_ID = val;
    }

    //////////////////////////////////////
    public BaseGameEntity(int id)
    {
        SetID(BaseGameEntityID.GUID);
    }

    //  ���е�����������ʵ��
    public abstract void Update();

    public int ID
    {
        get { return m_ID; }
    }

    //  ���е�ʵ��entities��ͨ���˷���ʵ����Ϣ����
    //  ʹ��MessageDispatcher��
    public abstract bool HandleMessage(Telegram msg);// { Debug.LogException(new System.Exception("No Overwrite Method")); return false; }

}