using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(menuName= "Weapons/Create WeaponManager")]
public class Weapon_Manager:ScriptableObject
{
    GameObject cur_weapon;
    public List<GameObject> weapon_list=new List<GameObject>();
    public Weapon SetUpWeaponInstance(int id,GameObject player)
    {
        if( id<weapon_list.Count && id>=0)
        {
            if(cur_weapon!=null)
                Destroy(cur_weapon);
            InstantiateWeapon(weapon_list[id],player) ;
            return cur_weapon.GetComponentInChildren<Weapon>();
        }
        
        Debug.Log("request failed");
        return null;
    }
    
    void InstantiateWeapon(GameObject weapon,GameObject player)
    {
        cur_weapon=Instantiate(weapon,player.transform);
    }
    public int GetWeaponAmount()
    {
        return weapon_list.Count;
    }
}
