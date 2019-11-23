using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTeleporter : MonoBehaviour
{
	public float detectRadius = 1;
	public float delayTime = 2;
	public bool avaible = true;

	protected virtual bool detect() { return false; }
	protected virtual IEnumerator teleport() { yield return new WaitForSeconds(this.delayTime); }
}