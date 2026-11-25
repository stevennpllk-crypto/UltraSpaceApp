using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Nova.Engine6.UI
{
    [Serializable]
    public sealed class NovaInventorySlot
    {
        public string itemId;
        public int quantity;
    }

    [DisallowMultipleComponent]
    public sealed class NovaInventory : MonoBehaviour
    {
        [SerializeField, Min(1)] private int capacity = 20;
        [SerializeField] private List<NovaInventorySlot> slots = new List<NovaInventorySlot>();
        [SerializeField] private UnityEvent onInventoryChanged = new UnityEvent();

        public IReadOnlyList<NovaInventorySlot> Slots => slots;
        public int Capacity => capacity;

        public bool AddItem(string itemId, int amount)
        {
            if (string.IsNullOrEmpty(itemId) || amount <= 0) return false;

            NovaInventorySlot existing = slots.Find(s => s != null && s.itemId == itemId);
            if (existing != null)
            {
                existing.quantity += amount;
            }
            else
            {
                if (slots.Count >= capacity) return false;
                slots.Add(new NovaInventorySlot { itemId = itemId, quantity = amount });
            }

            onInventoryChanged.Invoke();
            return true;
        }

        public bool RemoveItem(string itemId, int amount)
        {
            if (string.IsNullOrEmpty(itemId) || amount <= 0) return false;
            NovaInventorySlot existing = slots.Find(s => s != null && s.itemId == itemId);
            if (existing == null || existing.quantity < amount) return false;

            existing.quantity -= amount;
            if (existing.quantity <= 0) slots.Remove(existing);
            onInventoryChanged.Invoke();
            return true;
        }

        public int GetQuantity(string itemId)
        {
            NovaInventorySlot existing = slots.Find(s => s != null && s.itemId == itemId);
            return existing == null ? 0 : existing.quantity;
        }

        public void Clear()
        {
            slots.Clear();
            onInventoryChanged.Invoke();
        }
    }
}
