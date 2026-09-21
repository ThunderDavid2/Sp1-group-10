using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class BossRoom : MonoBehaviour
{

    [SerializeField] private GameObject bossRoomCanvas;
    private AudioSource audioSource;
    private Rigidbody2D playerrgbd;
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossRoomCanvas.SetActive(true);
            playerrgbd = other.gameObject.GetComponent<Rigidbody2D>();
            playerrgbd.linearDamping = 10000000000;
            other.gameObject.GetComponent<AudioSource>().Stop();
            //other.gameObject.GetComponent<Animator>().SetFloat("MoveSpeed", 0.01f);
            audioSource.Play();
            GetComponent<Collider2D>().enabled = false;
            Invoke(nameof(DisableCollider), 10f);
        }
    }
    private void DisableCollider()
    {
        playerrgbd.linearDamping = 0;
        bossRoomCanvas.SetActive(false);
    }

}
