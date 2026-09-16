using System.Runtime.CompilerServices;
using UnityEngine;

public class EnvironmentalDamage : MonoBehaviour
{
    [SerializeField] private int damageGiven = 1;
    private GameObject playerObject;

    private bool standingOnObject = false;
    private float timer = 0.0f;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            standingOnObject = true;
            playerObject = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            standingOnObject = false;
            playerObject = null;
            timer = 0f;
        }
    }

    private void Update()
    {
        if(standingOnObject == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 1f)
        {
            playerObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);
            timer = 0f;
        }
       
    }

     private void OnCollisionEnter2D(Collision2D other)
        //Upptäcka kollisionen
     {
           if (other.gameObject.CompareTag("Player"))
           {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);
           }

    
     }
}