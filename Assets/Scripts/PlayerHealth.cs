using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public float damageHeight = -10f;

    public HealthUI healthUI;

    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (transform.position.y < damageHeight)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        healthUI.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
    }
}
