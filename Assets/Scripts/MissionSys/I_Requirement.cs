using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class I_Requirement : ScriptableObject {
    abstract public bool check_require();
}
