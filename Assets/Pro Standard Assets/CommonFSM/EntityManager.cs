using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager
{
    private static EntityManager self;

    public static EntityManager Instance()
    {
        if (self == null)
            self = new EntityManager();

        return self;
    }

    //  ������
    Dictionary<int, BaseGameEntity> m_EntityMap = new Dictionary<int, BaseGameEntity>();

    //  ע��ʵ�����
    public void RegisterEntity(BaseGameEntity NewEntity)
    {
        m_EntityMap.Add(NewEntity.ID, NewEntity);
    }

    //  ����ʵ������ID���ض���
    public BaseGameEntity GetEntityFromID(int id)
    {
        return m_EntityMap[id];
    }

    //  �Ƴ�ʵ�����
    public void RemoveEntity(BaseGameEntity pEntity)
    {
        m_EntityMap.Remove(pEntity.ID);
    }
}
