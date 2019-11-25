using System.Collections.Generic;
using UnityEngine;
public enum GameChoice
{
    LogCutter=0,
    AttackOnMobs
}
public class MiniGameRequirement:I_Requirement
{
    [SerializeField]GameChoice gameChoice;
    [SerializeField]GameInfo require;
    public override bool check_require()
    {
        return PlayerData.record[(int)gameChoice].highestRecord >= require.highestRecord && PlayerData.record[(int)gameChoice].times >= require.times;
    }
}