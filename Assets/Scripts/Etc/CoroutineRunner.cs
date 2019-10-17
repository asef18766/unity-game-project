using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    static CoroutineRunner I_runner = null;
    
    public static void RunCoroutine(IEnumerator coroutine)
    {
        if(I_runner == null)
        {
            GameObject g = new GameObject("CoroutineRunner");
            DontDestroyOnLoad(g);

            I_runner = g.AddComponent<CoroutineRunner>();
        }
        I_runner.StartCoroutine(coroutine);
    }

    IEnumerator MonitorRunning(IEnumerator coroutine)
    {
        while(coroutine.MoveNext())
        {
            yield return coroutine.Current;
        }
    }
}