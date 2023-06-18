using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCredits : MonoBehaviour
{
    public GameObject showCredits;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            showCredits = GameObject.FindGameObjectWithTag("Credits");
            showCredits.SetActive(false);
        }
    }
}
