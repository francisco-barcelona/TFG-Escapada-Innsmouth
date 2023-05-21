using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public PlayerState currentState;

    public static bool talkedTaichiMan = false;
    public static bool talkedTicketSeller = false;
    public static bool talkedLibrarian = false;
    public static bool talkedMrsTilton = false;
    public static bool talkedCar = false;
    public static bool talkedSeller1 = false;
    public static bool talkedZadok = false;
    public static bool isWhisky = false;
    public static bool isCarInnsmouth = false;
    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    public void Update()
    {

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }



    }
    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        change = Vector3.zero;
        change.y = Input.GetAxisRaw("Vertical");
        change.x = Input.GetAxisRaw("Horizontal");
        if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    // Update is called once per frame. Used FixedUpdate because Update() makes the character walk slower.
    //void FixedUpdate()
    //{
    //    change = Vector3.zero;
    //    change.x = Input.GetAxisRaw("Horizontal");
    //    change.y = Input.GetAxisRaw("Vertical");

    //    if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
    //    {
    //        StartCoroutine(AttackCo());
    //    } else if(currentState == PlayerState.walk)
    //    {
    //        UpdateAnimationAndMove();
    //    }        
    //}

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidBody.MovePosition(
            transform.position + change.normalized * speed * Time.deltaTime
            );
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}