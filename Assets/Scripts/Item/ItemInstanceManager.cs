using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName= "Items/Create Item Manager")]
public class ItemInstanceManager:ScriptableObject
{
    static ItemInstanceManager m_instance;
    public static ItemInstanceManager Get_Id_Manager_Instance()
    {
        if(m_instance==null)
        {
            Debug.Log("Return Null instance of ItemInstanceManager");
            return null;
        }
        return m_instance;
    }
    void OnEnable()
    {
        m_instance=this;
    }
    [SerializeField] List<Item> item_set;
    public Item GetItemById(int id)
    {
        if(id>=item_set.Count||id<0)
        {
            Debug.Log("Error Id Request");
            return null;
        }
        return item_set[id];
    }
    public int GetIdByItem(Item it)
    {
        for(int i=0;i!=item_set.Count;++i)
            if(item_set[i]==it)
                return i;
        return -1;
    }
}