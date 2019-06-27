using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour {
	Animator ani;
	public void PlayAnimation()
	{
		ani.SetTrigger("attack");
	}
	// Use this for initialization
	void Start () {
		ani=GetComponent<Animator>();
	}
}
