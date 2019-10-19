using System.Collections.Generic;
using UnityEngine;
public enum GameChoice
{
    LogCutter=0,
    AttackOnMobs
}
[CreateAssetMenu(menuName="Mission/Create Mini Game Requirement")]
public class MiniGameRequirement:I_Requirement
{
    [SerializeField]GameChoice gameChoice;
    [SerializeField]GameInfo require;
    public override bool check_require()
    {
        return PlayerData.record[(int)gameChoice].highest_record >= require.highest_record && PlayerData.record[(int)gameChoice].times >= require.times;
    }
}