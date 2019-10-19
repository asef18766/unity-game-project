using System.Collections.Generic;
using UnityEngine;
public class ReputationRequirement: I_Requirement
{
    [SerializeField]int require;
    public override bool check_require()
    {
        return PlayerData.reputation>=require;
    }
}