using
    UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 5;

    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private Color lowHealthColor, criticalHealthColor;
    [SerializeField] private GameObject itemDrop;
    private bool canTakeDamage = true;
    private int currentEnemyHealth;

    void Start()
    {
        currentEnemyHealth = startingHealth;
    }
    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        UpdateBossHealthColor();
        if (currentEnemyHealth <= 0)
        {
            Instantiate(itemDrop, (Vector2)transform.position + new Vector2(0f, 2f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void ResetDamage()
    {
        canTakeDamage = true;
    }
    private void UpdateBossHealthColor()
    {
        if (currentEnemyHealth <= 3)
        {
            enemySprite.color = lowHealthColor;
        }
        
        if (currentEnemyHealth == 1)
        {
            enemySprite.color = criticalHealthColor;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!canTakeDamage) return;
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.transform.position.y > transform.position.y + 2.88f)
            {
                TakeDamage(1);
                canTakeDamage = false;
                Invoke(nameof(ResetDamage), 0.1f);
            }
        }

    }
    

    
}
