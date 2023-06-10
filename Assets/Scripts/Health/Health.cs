using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public static float currentHealth { get; set; }
    private Animator anim;
    //private bool dead;
    [SerializeField] private AudioSource audioDie;
    public bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        } else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                audioDie.Play();
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                StaticValues.Die();

                // Game Over Screen                
                SceneManager.LoadScene("Menu");
            }            
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
 
}
