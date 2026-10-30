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
        public List<BodyPartEntry> BodyParts = new(); // Body parts of creature used in Unity Editor
        public List<StatEntry> EntryStats = new(); // Exact stats of creature used in Unity Editor

        [HideInInspector] public StatsSystem Stats = new(); // Stats system
        [HideInInspector] public CreatureState State = CreatureState.Idle; // Creature state

        [HideInInspector] public Inventory Inventory; // Creature inventory

        private void Awake()
        {
            // Adding stats or returning if creature has no stats
            if (EntryStats == null || EntryStats.Count == 0)
            {
                Debug.LogWarning("EntryStats is null");
                return;
                
            }
            for (int i = 0; i < EntryStats.Count; i++)
            {
                Stats.AddStat(EntryStats[i].Type, EntryStats[i].Value);
            }

            // Check if the bodyparts let creature to hold items
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

        /// <summary>
        /// This method return exact body part of a creature or null if the creature has not this body part
        /// </summary>
        /// <param name="bodyPartType">Enum variable of body part type to get</param>
        /// <returns>Body part from BodyParts or null if not found</returns>
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

        /// <summary>
        /// Implementation of an interface IGetStatValue
        /// This method return stats value of creature
        /// </summary>
        /// <param name="statType">Enum variable of stat type</param>
        /// <returns>Value of stat or null if Stats are null</returns>
        public float GetStatValue(StatType statType)
        {
            if (Stats == null)
            {
                return 0f;
            }
            return Stats.GetStatValue(statType);
        }

        /// <summary>
        /// Implementation of an interface IAddItem
        /// This method send to inventory an item to add and return if inventory did it
        /// </summary>
        /// <param name="itemId">id of an item to add</param>
        /// <param name="amount">amount of an item</param>
        /// <returns>True if Inventory added this item with this amount or false if not or null if Inventory is null</returns>
        public bool AddItem(string itemId, int amount)
        {
            if (Inventory == null)
            {
                return false;
            }
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
