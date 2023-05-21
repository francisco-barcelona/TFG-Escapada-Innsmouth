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
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(inkJSON.name);
                if(inkJSON.name == "Newburyport-taichiman")
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "TicketSeller" && PlayerMovement.talkedTicketSeller == true)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Librarian" && PlayerMovement.talkedLibrarian == true)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "MrsTilton" && PlayerMovement.talkedMrsTilton == true)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Car" && PlayerMovement.talkedCar == true && PlayerMovement.isCarInnsmouth == false)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Seller1" && PlayerMovement.talkedSeller1 == true)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "Zadok" && PlayerMovement.talkedZadok == true && PlayerMovement.isWhisky == true)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
                if (inkJSON.name == "CarBroken" && PlayerMovement.talkedCar == true && Enemy1.zadokTalk == true)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
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
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
