using JetBrains.Annotations;
using UnityEngine;

public class EnemyBossHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private float slimeHeight;
    [SerializeField] private Color lowHealthColor, criticalHealthColor;
    [SerializeField] private float moveSpeed, lowHealthMoveSpeed, criticalHealthMoveSpeed;
    [SerializeField] private GameObject itemDrop;
    [SerializeField] private SpriteRenderer enemySprite;
    private Animator anim;
    private bool canTakeDamage = true;
    private int currentEnemyHealth;

    private void Start()
    {
        currentEnemyHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        anim.SetTrigger("Damage");
        UpdateBossHealthColor();
        GetComponent<BossDamagedProjectile>().SpawnProjectiles(5);
        //GetComponent<BossDamagedProjectile>().Damaged();
        if(currentEnemyHealth <= 0)
        {
            anim.SetTrigger("Death");
            GetComponent<EnemyBossMovement>().enabled = false;
            Invoke(nameof(Death), 1f);
        }
    }

    private void CanTakeDamageAgain()
    {
        canTakeDamage = true;
    }
    private void UpdateBossHealthColor()
    {
        if(currentEnemyHealth <= 3)
        {
            enemySprite.color = lowHealthColor;
            moveSpeed = lowHealthMoveSpeed;
        }
        if(currentEnemyHealth <= 1)
        {
            enemySprite.color = criticalHealthColor;
            moveSpeed = criticalHealthMoveSpeed;
        }
    }
    private void Death()
    {
        Instantiate(itemDrop, (Vector2)transform.position + new Vector2(0f, 2f), Quaternion.identity);
        Destroy(gameObject);
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage) return;
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.transform.position.y > transform.position.y + slimeHeight)
            {
                TakeDamage(1);
                canTakeDamage = false;
                Invoke(nameof(CanTakeDamageAgain), 0.1f);
            }
        }
    }

    public float GetBossMovementSpeed() { return moveSpeed; }

}
