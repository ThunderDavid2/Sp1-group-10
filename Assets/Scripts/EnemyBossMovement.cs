using System.
    Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBossMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float bounciness;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float upwardsForce;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float slimeHeight;
    [SerializeField] private GameObject shockwaveObjectRight, shockwaveObjectLeft;
    [SerializeField] private Transform shockwavePos;
    [SerializeField] private ParticleSystem landParticleSystem;
    private SpriteRenderer rend;
    private Animator anim;
    private Rigidbody2D rgbd;
    private Transform target;
    private Vector2 moveDirection;
    private float timer;
    private int jumpTime;
    private bool isGrounded;
    private float distance;
    private float moveSpeed;
    private int health;
    private float airTime;
    private bool hasJumped = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        jumpTime = Random.Range(1, 10);      
    }
    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, whatIsGround);
        distance = Vector2.Distance(transform.transform.position, target.transform.position);
        if(distance < 40)
        {
            timer += Time.deltaTime; 
            if (target)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
                rgbd.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rgbd.linearVelocity.y);
            }
            if (moveDirection.x < 0)
            {
                rend.flipX = true;
            }
            if (moveDirection.x > 0)
            {
                rend.flipX = false;
            }
            if (isGrounded == false)
            {
                airTime += Time.deltaTime;
                if (airTime > 1.2)
                {
                    hasJumped = true;
                    airTime = 0;
                }
            }
            else
            {
                airTime = 0;
            }
        }
        moveSpeed = GetComponent<EnemyBossHealth>().GetBossMovementSpeed();
        health = GetComponent<EnemyBossHealth>().GetBossCurrentHealth();
        Shockwave();
    }

    private void FixedUpdate()
    {
        if (distance < 40 && isGrounded && timer > jumpTime)
        {
            anim.SetTrigger("Jump");
            rgbd.AddForce(new Vector2(rgbd.linearVelocity.x, jumpForce), ForceMode2D.Impulse);
            timer = 0;
            if (health >= 4)
            {
                jumpTime = Random.Range(1, 10);
            }
            else if(health <=3)
            {
                jumpTime = Random.Range(1, 6);
            }
            else if(health == 1)
            {
                jumpTime = Random.Range(1, 3);
            }
        }
            
    }
    private void Shockwave()
    {
        if(hasJumped && isGrounded)
        {
            hasJumped = false;
            Instantiate(shockwaveObjectRight, shockwavePos.position, Quaternion.Euler(0, 0, 45));
            Instantiate(shockwaveObjectLeft, shockwavePos.position, Quaternion.Euler(0, 0, -45));
            landParticleSystem.Play();
        }
    }
  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.transform.transform.position.y > transform.position.y + slimeHeight)
            {
                return;
            }
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);
            {
                if (other.gameObject.GetComponent<PlayerMovement>() !=null)
                {
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
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Player"))
        {
            Rigidbody2D rgbd = other.attachedRigidbody;
            if(rgbd != null)
            {
                rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, 0);
                rgbd.AddForce(new Vector2(0, bounciness));            }
        }
    }

}
