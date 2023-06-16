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

    //private static DialogueManager instance;

    public TextAsset inkJSONNPC;
    public CinemachineVirtualCamera vcam1;
    public Transform tFollowTarget;
    public GameObject arrowHelper;


    private void Awake()
    {
        if (StaticValues.instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        StaticValues.instance = this;
    }

    //public static DialogueManager GetInstance()
    //{
    //    return StaticValues.instance;
    //}

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitDialogueMode(inkJSONNPC);
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
            StaticValues.talkedTicketSeller = true;
            arrowHelper = GameObject.FindGameObjectWithTag("arrow");
            arrowHelper.transform.position = new Vector3(23.64f, -9.12f, 0f);
        }

        if (inkJSONNPC.name == "TicketSeller")
        {
            StaticValues.talkedLibrarian = true;
            arrowHelper.transform.position = new Vector3(22f, 0.85f, 0f);
        }

        if (inkJSONNPC.name == "Librarian")
        {
            StaticValues.talkedMrsTilton = true;
            arrowHelper.transform.position = new Vector3(5.87f, -7.19f, 0f);
        }

        if (inkJSONNPC.name == "MrsTilton")
        {
            StaticValues.talkedCar = true;
            arrowHelper.transform.position = new Vector3(24.92f, 0.48f, 0f);
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

            StaticValues.talkedSeller1 = true;

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

            arrowHelper.transform.position = new Vector3(319.55f, -26.81f, 0f);
        }

        if (inkJSONNPC.name == "Seller1")
        {
            //StaticValues.talkedZadok = true;
            StaticValues.isWhisky = true;
            arrowHelper.transform.position = new Vector3(430.91f, 38.51f, 0f);
        }

        if (inkJSONNPC.name == "Zadok")
        {
            StaticValues.talkedZadok = true;
            //PlayerMovement.talkedZadok = true;
            //PlayerMovement.isWhisky = true;
            arrowHelper.transform.position = new Vector3(41f, 5.3f, 0f);
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
            StaticValues.canEnemy1Attack = true;
            StaticValues.canEnemy2Attack = true;
            //Timer.canStartCountDown = true;
            //Timer.timerObj.GetComponent<MeshRenderer>().enabled = true;

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
