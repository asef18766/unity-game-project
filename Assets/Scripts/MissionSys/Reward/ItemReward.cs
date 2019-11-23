using System.Collections.Generic;
using UnityEditor;
public class ItemReward : I_Reward
{
    List<ItemCount> reward=null;
    public override bool send_reward()
    {
        if(reward==null)
            return false;
        
        foreach(var i in reward)
            if(! Bag.bag_ins.addItem(i))
                return false;
        
        return true;
    }
}