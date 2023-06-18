using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCredits : MonoBehaviour
{
    public GameObject showCredits;
    void Start() { }
    void Update() {
        LoadCredits();

    }
    public void LoadCredits()
    {
        showCredits = GameObject.FindGameObjectWithTag("Credits");
        showCredits.SetActive(true);
    }
}
