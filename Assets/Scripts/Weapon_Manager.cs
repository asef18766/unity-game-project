using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class Weapon_Manager
{
    private static I_Weapon[] weapon_list;
    private static Weapon_Manager I_Self=null;
    private Weapon_Manager(){}
    public static Weapon_Manager GetInstance()
    {
        if(I_Self==null)
        {
            I_Self=new Weapon_Manager();
            I_Self.init();
        }
        return I_Self;
    }
    void init()
    {
        weapon_list=new I_Weapon[]{new Pistol()};
        for(int u=0;u!=weapon_list.Length;++u)
            weapon_list[u].init();
    }
    public I_Weapon request(int id)
    {
        if( id<weapon_list.Length && id>=0)
            return weapon_list[id];
        return null;
    }
}
