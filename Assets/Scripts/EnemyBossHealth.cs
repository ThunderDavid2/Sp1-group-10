using JetBrains.Annotations;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class EnemyBossHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private float slimeHeight;
    [SerializeField] private Color lowHealthColor, criticalHealthColor;
    [SerializeField] private float moveSpeed, normalMoveSpeed, lowHealthMoveSpeed, criticalHealthMoveSpeed;
    [SerializeField] private float lowHealthSize, criticalHealthSize;
    [SerializeField] private GameObject itemDrop;
    [SerializeField] private SpriteRenderer enemySprite;
    private Animator anim;
    private bool canTakeDamage = true;
    [SerializeField] private int currentEnemyHealth;
    [SerializeField] private int healingPickups;
    private bool lowHealth = false;
    private bool criticalHealth = false;

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
        UpdateBossSize();
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
    private void UpdateBossSize()
    {
        if(currentEnemyHealth == 3)
            if(lowHealth == false)
            {
                transform.localScale -= new Vector3(1, 1, 1);
                slimeHeight = slimeHeight - 0.46f;
                lowHealth = true;
            }
        if (currentEnemyHealth == 1)
            if (criticalHealth == false)
            {
                transform.localScale -= new Vector3(1, 1, 1);
                slimeHeight = slimeHeight - 0.46f;
                criticalHealth = true;
            }
    }
    public void Healing()
    {
        healingPickups += 1;
        if (healingPickups >= 5)
        {
            currentEnemyHealth += 1;
            healingPickups = 0;
            if(currentEnemyHealth == 4)
            {
                transform.localScale += new Vector3(1, 1, 1);
                lowHealth = false;
                moveSpeed = normalMoveSpeed;
                
            }
            if(currentEnemyHealth == 2)
            {
                enemySprite.color = lowHealthColor;
                transform.localScale += new Vector3(1, 1, 1);
                criticalHealth = false;
                moveSpeed = lowHealthMoveSpeed;
            }
        }
       
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
    public int GetBossCurrentHealth() { return currentEnemyHealth; }

}