using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int currenthealth;
    [SerializeField] protected int maxHealth;

    protected void ChangeHealth(int change)
    {
        currenthealth += change;
        Mathf.Clamp(currenthealth, 0, maxHealth);

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this);
    }

    public virtual void TakeDamage(int damage, GameObject attacker)
    {
        ChangeHealth(-damage);
    }

    public virtual void Heal(int amount, GameObject attacker)
    {
        ChangeHealth(amount);
    }
}
