using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Create WeaponManager")]
public class WeaponManager : ScriptableObject
{
    GameObject curWeapon;
    public List<GameObject> weaponList = new List<GameObject>();
    public Weapon SetUpWeaponInstance(int id, GameObject player)
    {
        if(id < weaponList.Count && id >= 0)
        {
            if(curWeapon != null)
                Destroy(curWeapon);
            InstantiateWeapon(weaponList[id], player);
            return curWeapon.GetComponentInChildren<Weapon>();
        }

        Debug.Log("request failed");
        return null;
    }

    void InstantiateWeapon(GameObject weapon, GameObject player)
    {
        curWeapon = Instantiate(weapon, player.transform);
    }
    public int GetWeaponAmount()
    {
        return weaponList.Count;
    }
}