using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnemySlimeBlue : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float bounciness = 100f;
    [SerializeField] private float knockbackForce = 500f;
    [SerializeField] private float upwardsForce = 500f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private float slimeHeight;
    [SerializeField] private GameObject slimeDrop;
    [SerializeField] private AudioClip slimeSquish;
    private SpriteRenderer rend;
    private Animator anim;
    private Rigidbody2D rgbd;
    private Transform target;
    private Vector2 moveDirection;
    private GameObject player;

    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.Find("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.transform.position, player.transform.position);
        if(distance < 20)
        {
            if(target)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
                rgbd.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rgbd.linearVelocity.y); 
            }
            if (moveDirection.x < 0f)
            {
                rend.flipX = true;
            }
            if (moveDirection.x > 0f)
            {
                rend.flipX = false;
            }
        }


        if(distance > 20)
        {
            if (moveSpeed < 0)
            {
                rend.flipX = true;
            }
            else
            {
                rend.flipX = false;
            }
            transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
        }

      
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.transform.position.y > transform.position.y + slimeHeight)
            {
                return;
            }
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardsForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardsForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rgbd = other.attachedRigidbody;
            if(rgbd != null)
            {
                rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, 0);
                rgbd.AddForce(new Vector2(0, bounciness));
                anim.SetTrigger("Death");
                GetComponent<EnemyAttacks>().enabled = false;
                GetComponent<EnemySlimeBlue>().enabled = false;
                damageGiven = 0;
                audioSource.PlayOneShot(slimeSquish);
                Invoke(nameof(OnDeath), 1f);
            }
        }
    }

    private void OnDeath()
    {
        Instantiate(slimeDrop, (Vector2)transform.position + new Vector2(0f, 2f), Quaternion.identity);
        Destroy(gameObject);
        
    }




}
