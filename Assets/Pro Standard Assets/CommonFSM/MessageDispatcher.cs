using UnityEngine;
using System.Collections;

/// <summary>
/// ��Ϣ���ͻ��ƣ���Ҫ���ڸ���ʵ������Ϣ����
/// </summary>
public class MessageDispatcher 
{
    private static MessageDispatcher self;

    public static MessageDispatcher Instance()
    {
        if (self == null)
            self = new MessageDispatcher();

        return self;
    }

    /// <summary>
    /// ��Ϣ����
    /// </summary>
    /// <param name="pReceiver"></param>
    /// <param name="telegram"></param>
    void Discharge(BaseGameEntity pReceiver, Telegram telegram)
    {
        if (!pReceiver.HandleMessage(telegram))
        {
            //telegram could not be handled
            //cout << "Message not handled";
        }
    }

    /// <summary>
    /// ȫ�ַ������ṩ��Ϣ���͵Ĺ���
    /// </summary>
    /// <param name="delay">�ӳ�ʱ��(Ŀǰ����)</param>
    /// <param name="sender"></param>
    /// <param name="receiver"></param>
    /// <param name="msg"></param>
    /// <param name="ExtraInfo"></param>
    public void DispatchMessage(double  delay,
                                 int    sender,
                                 int    receiver,
                                 int    msg,
                                 TelegramInfo ExtraInfo)
    {

        //  ������Ϣ��Entity
        BaseGameEntity pSender   = EntityManager.Instance().GetEntityFromID(sender);
        //  ������Ϣ��Entity
        BaseGameEntity pReceiver = EntityManager.Instance().GetEntityFromID(receiver);

        //  
        if (pReceiver == null)
            return;
  
        //  ��Ϣ��
        Telegram telegram = new Telegram(0, sender, receiver, msg, ExtraInfo);
  
        //  ����
        Discharge(pReceiver, telegram);
    }
}
