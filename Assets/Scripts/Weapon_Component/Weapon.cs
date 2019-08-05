using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class Weapon:MonoBehaviour
{
    WeaponAnimationController animator;
    [SerializeField]Projectile_Shooter shooter;
    void Start()
    {
        animator=GetComponent<WeaponAnimationController>();
    }
    public void Attack()
    {
        if(shooter.Shoot())
            animator.PlayAnimation();
    }
    void Update()
    {
        
    }
}