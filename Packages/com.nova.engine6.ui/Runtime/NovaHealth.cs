using System;
using UnityEngine;
using UnityEngine.Events;

namespace Nova.Engine6.UI
{
    [Serializable] public sealed class FloatEvent : UnityEvent<float> { }

    [DisallowMultipleComponent]
    public sealed class NovaHealth : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float maxHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        [SerializeField] private FloatEvent onHealthChanged = new FloatEvent();
        [SerializeField] private UnityEvent onDeath = new UnityEvent();

        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public float Normalized => maxHealth <= 0f ? 0f : Mathf.Clamp01(currentHealth / maxHealth);
        public bool IsDead => currentHealth <= 0f;

        private void Awake()
        {
            maxHealth = Mathf.Max(1f, maxHealth);
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            onHealthChanged.Invoke(Normalized);
        }

        public void TakeDamage(float amount)
        {
            if (amount <= 0f || IsDead) return;
            SetHealth(currentHealth - amount);
            if (IsDead) onDeath.Invoke();
        }

        public void Heal(float amount)
        {
            if (amount <= 0f || IsDead) return;
            SetHealth(currentHealth + amount);
        }

        public void SetHealth(float value)
        {
            currentHealth = Mathf.Clamp(value, 0f, maxHealth);
            onHealthChanged.Invoke(Normalized);
        }

        public void SetMaxHealth(float value, bool refill)
        {
            maxHealth = Mathf.Max(1f, value);
            currentHealth = refill ? maxHealth : Mathf.Min(currentHealth, maxHealth);
            onHealthChanged.Invoke(Normalized);
        }
    }
}
