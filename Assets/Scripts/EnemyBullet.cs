using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private AudioClip bulletHit;
    private GameObject player;
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private float timer;
    private ParticleSystem particles;
    private AudioSource audioSource;

    [SerializeField] private float bulletForce;
    private void Start()
    {
        timer = Time.deltaTime;

        rend = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
        rgbd.linearVelocity = new Vector2(direction * bulletForce, 0f);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 4)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(bulletHit, transform.position);
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);

        }
    }

}

