using UnityEngine;
public class ReputationReward : I_Reward
{
    [SerializeField]int reward;
    public override bool send_reward()
    {
        PlayerData.reputation+=reward;
        return true;
    }
}