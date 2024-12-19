using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthbarFill;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        healthbarFill.fillAmount = currentHealth / maxHealth;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Damage Taken: {amount}, Current Health: {currentHealth}");
        UpdateHealthBar();
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Health Restored: {amount}, Current Health: {currentHealth}");
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
    }
}