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
    public int health;
    private Rigidbody2D myRigidBody;
    public static Vector3 change;
    private Animator animator;
    public PlayerState currentState;

    [SerializeField] private AudioSource audioSourceHit;

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
        
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger && StaticValues.isTalkingNow == false)
        {
            audioSourceHit.Play();
            StartCoroutine(AttackCo());
        }



    }
    void FixedUpdate()
    {
        if (StaticValues.GetInstance().dialogueIsPlaying)
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