using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 5;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color normalHealthColor, criticalHealthColor, lowHealthColor;
    private int currentHealth;

    private bool cantakeDamage = true;

    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }

 
    public void TakeDamage(int damage)   
    {
        if (cantakeDamage == true)
        {
            currentHealth -= damage;

            UpdateHealthbar();

            playerMovement.PlayerDamage();
            cantakeDamage = false;
            Invoke(nameof(CanTakeDamage), 0.4f);
        }
        
        if (currentHealth <= 0)
        {
            Respawn();
        }
    
    }

    public void CanTakeDamage()
    {
        cantakeDamage = true;
    }
    
    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthbar();
        //transform.position = spawnPosition.position;
        //GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        SceneManager.LoadScene(3);
    }

    private void UpdateHealthbar()
    {
        healthSlider.value = currentHealth;

        if(currentHealth <= 3)
        {
            fillImage.color = lowHealthColor;
        }
        else
        {
            fillImage.color = normalHealthColor;
        }
        if(currentHealth == 1)
        {
            fillImage.color = criticalHealthColor;
        }
    }


    public bool RestoreHealth(int healthToRestore)
    {
        if (currentHealth >= startingHealth)
        {
            return false;
        }
        currentHealth += healthToRestore;
        UpdateHealthbar();

        if(currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
        return true;
    }

}
