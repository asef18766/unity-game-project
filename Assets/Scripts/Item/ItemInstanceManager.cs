using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName= "Items/Create Item Manager")]
public class ItemInstanceManager:ScriptableObject
{
    [SerializeField]ItemInstanceManager _m_instance;
    static ItemInstanceManager m_instance=null;
    public static ItemInstanceManager Get_Id_Manager_Instance()
    {
        return m_instance;
    }
    void OnEnable()
    {
        m_instance=_m_instance;
    }
    [SerializeField] List<Item> item_set;
    public Item GetItemById(int id)
    {
        if(item_set[id]==null)
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