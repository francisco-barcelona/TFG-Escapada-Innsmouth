using UnityEngine;
using Cinemachine;
using System.Collections;

public class FollowThePath : MonoBehaviour
{

    public CinemachineVirtualCamera vcam1;

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 5f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    public static bool canRun = false;

    // Use this for initialization
    private void Start()
    {
        if(canRun == true)
        {
            // Set position of Enemy as position of the first waypoint
            transform.position = waypoints[waypointIndex].transform.position;
        }        
    }

    // Update is called once per frame
    private void Update()
    {
        if (canRun == true)
        {
            // Move Enemy
            StartCoroutine(Move());
            //Move();
        }        
    }

    // Method that actually make Enemy walk
    private IEnumerator Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;

                Debug.Log(waypointIndex);
                Debug.Log(waypoints.Length - 1);

                if (waypointIndex == waypoints.Length - 1)
                {
                    GameObject traveler = GameObject.FindGameObjectWithTag("Player");

                    Debug.Log(traveler);

                    // Player exits the car
                    traveler.transform.position = new Vector3(318, -34, 0);

                    // Camera follows the car
                    vcam1 = GameObject.FindGameObjectWithTag("playerCam").GetComponent<CinemachineVirtualCamera>();
                    vcam1.Priority = 13;

                    yield return new WaitForSeconds(3);
                    traveler.GetComponent<Renderer>().enabled = true;
                    PlayerMovement.isCarInnsmouth = true;
                }
            }            
        }
    }
}
