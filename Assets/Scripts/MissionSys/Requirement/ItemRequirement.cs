using System.Collections.Generic;
using UnityEngine;
public class ItemRequirement:I_Requirement
{
    [SerializeField]List<ItemCount> req_item;
    public override bool check_require()
    {
        foreach (var i in req_item)
            if(Bag.bag_ins.checkItem(i)==-1)
                return false;
        return true;
    }
}