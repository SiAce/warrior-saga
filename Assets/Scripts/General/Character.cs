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

    private bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        invincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDemage(Attack attacker)
    {
        if (invincible) { return; }

        currentHealth = Mathf.Max(currentHealth - attacker.damage, 0);

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
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
