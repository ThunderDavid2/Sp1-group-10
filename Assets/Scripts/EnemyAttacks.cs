using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private AudioClip fire, chargingUp;

    private bool hasFired = false;
    private bool soundHasPlayed = false;
    private Animator anim;
    private float timer;
    private GameObject player;
    private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1f;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.transform.position, player.transform.position);
        if(distance < 25)
        {
            timer += Time.deltaTime;
            if(timer >= 1 && !hasFired && !soundHasPlayed)
            {
                if(gameObject.CompareTag("RangedEnemy"))
                {
                    audioSource.PlayOneShot(chargingUp);
                    soundHasPlayed = true;
                }
            }
            if(timer >= 1.5 && !hasFired)
            {
                anim.SetTrigger("Fire");
                hasFired = true;
            }

            if(timer > 2)
            {
                timer = 0;
                hasFired = false;
                soundHasPlayed = false;
                shoot();
            }

        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        audioSource.PlayOneShot(fire);
    }
    
    
}

