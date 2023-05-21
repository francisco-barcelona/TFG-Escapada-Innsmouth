using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Timer : MonoBehaviour
{
    public static float countDown = 30;
    public TMP_Text text;
    public static bool canStartCountDown = false;
    public static GameObject timerObj;
    
    // Start is called before the first frame update
    void Start()
    {
        timerObj = GameObject.FindGameObjectWithTag("Timer");
        timerObj.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canStartCountDown == true && timerObj.GetComponent<MeshRenderer>().enabled == true)
        {   
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                Debug.Log(countDown);
                // Que salgan los monstruos
                Enemy2.isVisible = true;
            }

            text.text = System.Math.Round(countDown, 0).ToString();
        }
        
        
    }
}
