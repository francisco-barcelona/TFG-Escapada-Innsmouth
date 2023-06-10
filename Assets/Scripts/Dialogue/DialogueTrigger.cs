using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !StaticValues.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Enter dialogue manager");
                if (inkJSON.name == "Newburyport-taichiman")
                {
                    StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "TicketSeller" && StaticValues.talkedTicketSeller == true)
                {
                    StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Librarian" && StaticValues.talkedLibrarian == true)
                {
                    if(StaticValues.GetInstance() != null)
                    {
                        StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                    }
                    else
                    {
                        Debug.Log("Librarian is null");
                    }
                    
                }
                if (inkJSON.name == "MrsTilton" && StaticValues.talkedMrsTilton == true)
                {
                    StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Car" && StaticValues.talkedCar == true && StaticValues.isCarInnsmouth == false)
                {
                    StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Seller1" && StaticValues.talkedSeller1 == true)
                {
                    StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Zadok" && StaticValues.isWhisky == true)
                {
                    StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "CarBroken" && StaticValues.talkedCar == true && StaticValues.zadokTalk == true)
                {
                    StaticValues.GetInstance().EnterDialogueMode(inkJSON);
                }

            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            StaticValues.isTalkingNow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            StaticValues.isTalkingNow = false;
        }
    }
}
