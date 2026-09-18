using System.Runtime.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySlimeGray : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float bounciness = 800f;
    [SerializeField] private float knockbackForce = 500f;
    [SerializeField] private float upwardsForce = 500f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private float slimeHeight;
    [SerializeField] private GameObject slimeDrop;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float raycastDistance = 1f;
    private SpriteRenderer rend;
    private Animator anim;
    private Rigidbody2D rgbd;
    private Transform target;
    private Vector2 moveDirection;
    private GameObject player;
    private float timer;
    private bool isGrounded;
    private bool shouldJump;

    private void Start()
    {
        timer += Time.deltaTime;
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        // Is Grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, whatIsGround);
        // Player above detection
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << target.gameObject.layer);

        float playerDirection = Mathf.Sign(target.position.x - transform.position.x);

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 30)
        {
            timer += Time.deltaTime;
            if(target)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
                rgbd.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rgbd.linearVelocity.y);
                if(isGrounded)
                {
                    RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(playerDirection, 0), 2f, whatIsGround);
                    RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(playerDirection, 0, 0), Vector2.down, 2f, whatIsGround);
                    RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, whatIsGround);

                    if (!groundInFront.collider && !gapAhead.collider)
                    {
                        shouldJump = true;
                    }
                    else if (isPlayerAbove && platformAbove.collider)
                    {
                        shouldJump = true;
                    }
                }
            }
            if(moveDirection.x < 0f)
            {
                rend.flipX = true;
            }
            if(moveDirection.x > 0f)
            {
                rend.flipX = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 jumpDirection = (target.position - transform.position).normalized;
            rgbd.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("Jump");

        } 
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.transform.position.y > transform.position.y + slimeHeight)
            {
                return;
            }
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);
        }
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Rigidbody2D rgbd = other.attachedRigidbody;
            if(rgbd != null)
            {
                rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, 0);
                rgbd.AddForce(new Vector2(0, bounciness));
                anim.SetTrigger("Death");
                GetComponent<EnemySlimeGray>().enabled = false;
                damageGiven = 1;
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
