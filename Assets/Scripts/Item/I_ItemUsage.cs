using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
public enum ItemEvent
{
    Use,
    Drop
}
public abstract class I_ItemUsage : MonoBehaviour
{
    public ItemEvent itemEvent;
    abstract public bool use();
}