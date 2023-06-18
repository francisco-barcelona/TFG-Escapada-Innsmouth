using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHelp : MonoBehaviour
{
    public GameObject showHelp;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            showHelp = GameObject.FindGameObjectWithTag("ShowHelp");
            showHelp.SetActive(false);
        }
    }
}
