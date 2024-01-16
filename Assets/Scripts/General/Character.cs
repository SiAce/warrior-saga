using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float invincibleDuration;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDeath;
    public UnityEvent<float> OnHealthChange;

    private bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pit"))
        {
            currentHealth = 0;
            OnHealthChange?.Invoke(0);
            OnDeath?.Invoke();
        }
    }

    public void TakeDemage(Attack attacker)
    {
        if (invincible) { return; }

        currentHealth = Mathf.Max(currentHealth - attacker.damage, 0);

        OnHealthChange?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
            invincible = true;
        }
        else
        {
            OnTakeDamage?.Invoke(attacker.transform);
            StartCoroutine(MakeInvincible());
        }
    }

    IEnumerator MakeInvincible()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        invincible = false;
    }
}
