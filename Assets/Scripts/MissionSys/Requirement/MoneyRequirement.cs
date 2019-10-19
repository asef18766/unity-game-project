using System.Collections.Generic;
using UnityEngine;
public class MoneyRequirement:I_Requirement
{
    [SerializeField]int require;
    public override bool check_require()
    {
        return PlayerData.money>=require;
    } 
}