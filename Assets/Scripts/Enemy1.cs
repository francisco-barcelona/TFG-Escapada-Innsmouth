using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public Transform homePosition;
    public float chaseRadius;
    public float attackRadius;
    public Animator anim;
    [SerializeField]
    public static bool zadokTalk = false;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(zadokTalk == true)
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk && Vector3.Distance(target.position, transform.position) > attackRadius)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    ChangeState(EnemyState.walk);
                    anim.SetBool("wakeUp", true);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                anim.SetBool("wakeUp", false);
            }
            else
            {
                anim.SetBool("wakeUp", false);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
