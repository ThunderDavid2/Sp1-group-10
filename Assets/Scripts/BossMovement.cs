using UnityEngine;

public class BossMovement : MonoBehaviour
{
        [SerializeField] float moveSpeed = 2.0f;
        [SerializeField] private float bounciness = 100f;
        [SerializeField] private int damageGiven = 1;

        //Knockback
        [SerializeField] private float knockbackForce = 100f;
        [SerializeField] private float upwardsForce = 5f;

        private SpriteRenderer rend;

        private void Start()
        {
            rend = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (moveSpeed < 0)
            {
                rend.flipX = true;
            }
            if (moveSpeed > 0)
            {
                rend.flipX = false;
            }
        }

        void FixedUpdate()
        {
            transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        }

        private void OnCollisionEnter2D(Collision2D other)
        // Upptäcka kollisionen
        {
            if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
            {
                moveSpeed = -moveSpeed;
                // byter håll på x axeln
            }

            if (other.gameObject.CompareTag("Player"))
            {
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
                // Hämtar Rigidbody som är tillsatt till Collidern

                if (rgbd != null)
                {
                    rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, 0);
                    rgbd.AddForce(new Vector2(0, bounciness));
                }
                GetComponent<EnemyHealth>().TakeDamage(1);
                //Destroy(gameObject);


            }
        }
    }


