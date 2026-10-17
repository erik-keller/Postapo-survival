using System.Collections.Generic;
using UnityEngine;
using ProjectPostapo.Stats;

namespace ProjectPostapo.Player
{
    public class PlayerStats : MonoBehaviour, IGetStatValue
    {
        private Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();

        private void Awake()
        {
            // Initialize stats with default values
            stats.Add(StatType.MaxHealth, new Stat(100));
            stats.Add(StatType.CurrentHealth, new Stat(100));
            stats.Add(StatType.InteractionRange, new Stat(2f));
            stats.Add(StatType.MovementSpeed, new Stat(5f));
            stats.Add(StatType.SprintMultiplier, new Stat(1.5f));
            stats.Add(StatType.JumpHeight, new Stat(2f));
        }

        public float GetStatValue(StatType statType)
        {
            if (stats.TryGetValue(statType, out Stat stat))
            {
                return stat.Value;
            }
            else
            {
                Debug.LogError($"Stat of type {statType} not found.");
                return 0f;
            }
        }

        public void ModifyStat(StatType statType, float amount)
        {
            if (stats.TryGetValue(statType, out Stat stat))
            {
                stat.Modify(amount);
            }
            else
            {
                Debug.LogError($"Stat of type {statType} not found.");
            }
        }
    }
}