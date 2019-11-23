using UnityEngine;
public class MoneyReward : I_Reward
{
    [SerializeField]int reward;
    public override bool send_reward()
    {
        PlayerData.money+=reward;
        return true;
    }
}