using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth = Health.currentHealth;

    public bool talkedTaichiMan = StaticValues.talkedTaichiMan;
    public bool talkedTicketSeller = StaticValues.talkedTicketSeller;
    public bool talkedLibrarian = StaticValues.talkedLibrarian;
    public bool talkedMrsTilton = StaticValues.talkedMrsTilton;
    public bool talkedCar = StaticValues.talkedCar;
    public bool talkedSeller1 = StaticValues.talkedSeller1;
    public bool talkedZadok = StaticValues.talkedZadok;
    public bool isWhisky = StaticValues.isWhisky;
    public bool isCarInnsmouth = StaticValues.isCarInnsmouth;
    public bool isTalkingNow = StaticValues.isTalkingNow;
    public static bool canEnemy1Attack = StaticValues.canEnemy1Attack;
    public static bool canEnemy2Attack = StaticValues.canEnemy2Attack;

    GameObject[] enemies;
    GameObject enemyLoaded;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {            
            SavePlayer();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadPlayer();
        }
    }

    public void Die()
    {
        StaticValues.talkedTaichiMan = false;
        StaticValues.talkedTicketSeller = false;
        StaticValues.talkedLibrarian = false;
        StaticValues.talkedMrsTilton = false;
        StaticValues.talkedCar = false;
        StaticValues.talkedSeller1 = false;
        StaticValues.talkedZadok = false;
        StaticValues.isWhisky = false;
        StaticValues.isCarInnsmouth = false;
        StaticValues.isTalkingNow = false;
        StaticValues.canEnemy1Attack = false;
        StaticValues.canEnemy2Attack = false;

    currentHealth = 10;

        FollowThePath.canRun = false;

        Vector3 position;
        position.x = -11;
        position.y = 6;
        position.z = 0;

        transform.position = position;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.currentHealth;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        StaticValues.talkedTaichiMan = data.talkedTaichiMan;
        StaticValues.talkedTicketSeller = data.talkedTicketSeller;
        StaticValues.talkedLibrarian = data.talkedLibrarian;
        StaticValues.talkedMrsTilton = data.talkedMrsTilton;
        StaticValues.talkedCar = data.talkedCar;
        StaticValues.talkedSeller1 = data.talkedSeller1;
        StaticValues.talkedZadok = data.talkedZadok;
        StaticValues.isWhisky = data.isWhisky;
        StaticValues.isCarInnsmouth = data.isCarInnsmouth;
        StaticValues.isTalkingNow = data.isTalkingNow;
        StaticValues.canEnemy1Attack = data.canEnemy1Attack;
        StaticValues.canEnemy2Attack = data.canEnemy2Attack;
        enemies = data.enemies;

        // Music load control
        if (StaticValues.talkedTaichiMan == false || StaticValues.talkedTicketSeller == false || StaticValues.talkedLibrarian == false || StaticValues.talkedMrsTilton == false)
        {
            GameObject musicNewburyport = GameObject.FindGameObjectWithTag("MusicNewburyport");
            AudioSource audioNewburyport;
            audioNewburyport = musicNewburyport.GetComponent<AudioSource>();
            audioNewburyport.Play();
        }

        if (StaticValues.talkedCar == true && StaticValues.talkedZadok == false)
        {
            // Car in Innsmouth
            FollowThePath.canRun = true;

            // Player is in Innsmouth, stop happy music
            GameObject musicNewburyport = GameObject.FindGameObjectWithTag("MusicNewburyport");
            AudioSource audioNewburyport;
            audioNewburyport = musicNewburyport.GetComponent<AudioSource>();
            audioNewburyport.Stop();
            //musicNewburyport.SetActive(false);

            // Begin misterious music
            GameObject musicInnsmouth = GameObject.FindGameObjectWithTag("MusicArriveInnsmouth");
            AudioSource audioArriveInnsmouth;
            audioArriveInnsmouth = musicInnsmouth.GetComponent<AudioSource>();
            audioArriveInnsmouth.Play();
        }

        if(StaticValues.talkedZadok == true)
        {
            // Car in Innsmouth
            FollowThePath.canRun = true;

            // Stop misterious music
            GameObject musicInnsmouth2 = GameObject.FindGameObjectWithTag("MusicArriveInnsmouth");
            AudioSource audioArriveInnsmouth2;
            audioArriveInnsmouth2 = musicInnsmouth2.GetComponent<AudioSource>();
            audioArriveInnsmouth2.Stop();

            // Begin Escape music
            GameObject musicEscape = GameObject.FindGameObjectWithTag("MusicEscape");
            AudioSource audioEscape;
            audioEscape = musicEscape.GetComponent<AudioSource>();
            audioEscape.Play();
        }

        foreach (GameObject enemy in enemies)
        {
            Instantiate(enemyLoaded, enemy.transform.position, enemy.transform.rotation);
        }

        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("enemy");
        Debug.Log("enemies after loading");
        Debug.Log(enemies2.Length);

    }

}
