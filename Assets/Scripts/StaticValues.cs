using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StaticValues 
{
    public static bool talkedTaichiMan = false;
    public static bool talkedTicketSeller = false;
    public static bool talkedLibrarian = false;
    public static bool talkedMrsTilton = false;
    public static bool talkedCar = false;
    public static bool talkedSeller1 = false;
    public static bool talkedZadok = false;
    public static bool isWhisky = false;
    public static bool isCarInnsmouth = false;
    public static bool isTalkingNow = false;
    public static bool zadokTalk = false;
    public static bool canEnemy1Attack = false;
    public static bool canEnemy2Attack = false;
    public static DialogueManager instance;    

    public static DialogueTrigger dialogueTrigger = new DialogueTrigger();

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public static void Die()
    {
        talkedTaichiMan = false;
        talkedTicketSeller = false;
        talkedLibrarian = false;
        talkedMrsTilton = false;
        talkedCar = false;
        talkedSeller1 = false;
        talkedZadok = false;
        isWhisky = false;
        isCarInnsmouth = false;
        isTalkingNow = false;
        zadokTalk = false;
        FollowThePath.canRun = false;
        canEnemy1Attack = false;
        canEnemy2Attack = false;
}
}
