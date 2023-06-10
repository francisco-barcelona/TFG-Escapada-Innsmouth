using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData 
{
    public float currentHealth;
    public float[] position;
    public bool talkedTaichiMan;
    public bool talkedTicketSeller;
    public bool talkedLibrarian;
    public bool talkedMrsTilton;
    public bool talkedCar;
    public bool talkedSeller1;
    public bool talkedZadok;
    public bool isWhisky;
    public bool isCarInnsmouth;
    public bool isTalkingNow;
    public bool canEnemy1Attack;
    public bool canEnemy2Attack;
    [NonSerialized]
    public GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
    [NonSerialized]
    public GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("Frogman");

    //public GameObject[] enemies;

    public PlayerData(Player player)
    {
        this.currentHealth = Health.currentHealth;

        this.position = new float[3];
        this.position[0] = player.transform.position.x;
        this.position[1] = player.transform.position.y;
        this.position[2] = player.transform.position.z;

        talkedTaichiMan = StaticValues.talkedTaichiMan;
        talkedTicketSeller = StaticValues.talkedTicketSeller;
        talkedLibrarian = StaticValues.talkedLibrarian;
        talkedMrsTilton = StaticValues.talkedMrsTilton;
        talkedCar = StaticValues.talkedCar;
        talkedSeller1 = StaticValues.talkedSeller1;
        talkedZadok = StaticValues.talkedZadok;
        isWhisky = StaticValues.isWhisky;
        isCarInnsmouth = StaticValues.isCarInnsmouth;
        isTalkingNow = StaticValues.isTalkingNow;
        canEnemy1Attack = StaticValues.canEnemy1Attack;
        canEnemy2Attack = StaticValues.canEnemy2Attack;

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        Debug.Log(enemies.Length);
        enemies2 = GameObject.FindGameObjectsWithTag("Frogman");
        Debug.Log(enemies2.Length);
    }
}
