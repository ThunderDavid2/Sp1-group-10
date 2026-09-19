using JetBrains.Annotations;
using UnityEngine;

public class EnemyBossHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private float slimeHeight;
    [SerializeField] private Color lowHealthColor, criticalHealthColor;
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
        if(currentEnemyHealth <= 5)
        {
            enemySprite.color = lowHealthColor;
        }
        if(currentEnemyHealth <= 2)
        {
            enemySprite.color = criticalHealthColor;
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

}
