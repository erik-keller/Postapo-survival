using ProjectPostapo.InventorySystem;
using ProjectPostapo.Stats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace ProjectPostapo.CreatureSystem
{
    public class Creature : MonoBehaviour, IGetStatValue, IAddItem
    {
        public List<BodyPartEntry> BodyParts = new();
        public List<StatEntry> EntryStats = new();

        [HideInInspector] public StatsSystem Stats = new();
        [HideInInspector] public CreatureState State = CreatureState.Idle;

        [HideInInspector] public Inventory Inventory;

        private void Awake()
        {
            if (EntryStats.Count != 0)
            {
                for(int i = 0; i < EntryStats.Count; i++)
                {
                    Stats.AddStat(EntryStats[i].Type, EntryStats[i].Value);
                }
            }
            else
            {
                Debug.LogWarning("Loaded default stat values");

                Stats.AddStat(StatType.MaxHealth, 100.0f);
                Stats.AddStat(StatType.CurrentHealth, 100.0f);
                Stats.AddStat(StatType.InteractionRange, 2.0f);
                Stats.AddStat(StatType.MovementSpeed, 5.0f);
                Stats.AddStat(StatType.SprintMultiplier, 1.5f);
                Stats.AddStat(StatType.JumpHeight, 1.5f);
            }

            uint handySlots = 0;

            var hand = GetBodyPart(BodyPartType.LeftHand);

            for(int i = 0; i < BodyParts.Count; i++)
            {
                if (BodyParts[i].CanHoldSomething)
                {
                    handySlots++;
                }
            }

            Inventory = new(handySlots);
        }

        public BodyPartEntry? GetBodyPart(BodyPartType bodyPartType)
        {
            for (int i = 0; i < BodyParts.Count; i++)
            {
                if (BodyParts[i].BodyPartType == bodyPartType)
                {
                    return BodyParts[i];
                }
            }
            return null;
        }

        public float GetStatValue(StatType statType)
        {
            return Stats.GetStatValue(statType);
        }

        public bool AddItem(string itemId, int amount)
        {
            return Inventory.AddItem(itemId, amount);
        }

        [Serializable]
        public struct StatEntry
        {
            public StatType Type;
            public float Value;

            public StatEntry(StatType type, float value)
            {
                this.Type = type;
                this.Value = value;
            }
        }

        [Serializable]
        public struct BodyPartEntry
        {
            public BodyPartType BodyPartType;
            public BodyPart BodyPart;
            public bool CanHoldSomething;

            BodyPartEntry(BodyPartType bodyPartType, BodyPart bodyPart, bool canHoldSomething)
            {
                this.BodyPartType = bodyPartType;
                this.BodyPart = bodyPart;
                this.CanHoldSomething = canHoldSomething;
            }
        }
    }
}
