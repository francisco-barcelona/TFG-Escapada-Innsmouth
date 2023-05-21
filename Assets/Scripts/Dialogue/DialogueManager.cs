using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    public TextAsset inkJSONNPC;
    public CinemachineVirtualCamera vcam1;
    public Transform tFollowTarget;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Debug.Log("Enter dialogue");
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        inkJSONNPC = inkJSON;

        if (inkJSONNPC.name == "Newburyport-taichiman")
        {
            PlayerMovement.talkedTicketSeller = true;
        }

        if (inkJSONNPC.name == "TicketSeller")
        {
            PlayerMovement.talkedLibrarian = true;
        }

        if (inkJSONNPC.name == "Librarian")
        {
            PlayerMovement.talkedMrsTilton = true;
        }

        if (inkJSONNPC.name == "MrsTilton")
        {
            PlayerMovement.talkedCar = true;
        }

        if (inkJSONNPC.name == "Car")
        {
            GameObject traveler = GameObject.FindGameObjectWithTag("Player");

            // Player disapears because enters the car
            //traveler.SetActive(false);
            traveler.GetComponent<Renderer>().enabled = false;

            // Camera follows the car
            vcam1 = GameObject.FindGameObjectWithTag("playerCam").GetComponent<CinemachineVirtualCamera>();
            vcam1.Priority = 0;            

            FollowThePath.canRun = true;

            PlayerMovement.talkedSeller1 = true;
        }

        if (inkJSONNPC.name == "Seller1")
        {
            PlayerMovement.talkedZadok = true;
            PlayerMovement.isWhisky = true;
        }

        if (inkJSONNPC.name == "Zadok")
        {
            //PlayerMovement.talkedZadok = true;
            //PlayerMovement.isWhisky = true;
        }

        ContinueStory();
    }

    private void ExitDialogueMode(TextAsset inkJSONNPC)
    {        
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        if(inkJSONNPC.name == "Zadok")
        {
            Enemy1.zadokTalk = true;
            Timer.canStartCountDown = true;
            Timer.timerObj.GetComponent<MeshRenderer>().enabled = true;
        }        

    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode(inkJSONNPC);
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);

        //if (canContinueToNextLine)
        //{
        //    currentStory.ChooseChoiceIndex(choiceIndex);
        //    // NOTE: The below two lines were added to fix a bug after the Youtube video was made
        //    InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
        //    ContinueStory();
        //}
    }


}
