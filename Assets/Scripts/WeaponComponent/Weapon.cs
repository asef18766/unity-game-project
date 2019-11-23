using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    WeaponAnimationController animator;
    [SerializeField] ProjectileShooter shooter;

    void Start()
    {
        this.animator = GetComponent<WeaponAnimationController>();
    }

    public void Attack()
    {
        if(this.shooter.Shoot())
            this.animator.PlayAnimation();
    }
}