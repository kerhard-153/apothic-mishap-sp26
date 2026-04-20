using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Animator[] leafAnimators;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetMaxHealth(int maxHealth)
    {
        // Powerups?
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < leafAnimators.Length; i++)
        {
            if (i >= currentHealth)
            {
                leafAnimators[i].SetTrigger("Crumple");
            }
        }
    }
}
