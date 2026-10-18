using System.Collections.Generic;
using UnityEngine;

namespace ProjectPostapo.Stats
{
    public class StatsSystem : IGetStatValue
    {
        private Dictionary<StatType, Stat> stats = new();

        public void AddStat(StatType statType, float value)
        {
            stats.Add(statType, new Stat(value));
        }

        public float GetStatValue(StatType statType)
        {
            if (!stats.TryGetValue(statType, out Stat stat))
            {
                Debug.LogError($"Stat of type {statType} not found.");
                return 0f;

            }

            return stat.Value;
        }

        public void ModifyStat(StatType statType, float amount)
        {
            if (!stats.TryGetValue(statType, out Stat stat))
            {
                Debug.LogError($"Stat of type {statType} not found.");
                return;
            }

            stat.Modify(amount);
        }
    }
}
