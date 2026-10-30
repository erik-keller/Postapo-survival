using ProjectPostapo.InventorySystem;
using System.Collections.Generic;

namespace ProjectPostapo.CreatureSystem
{
    public class BodyPart
    {
        public BodyPartType Type;
        public List<BodyPartState> States = new();
        public List<ItemInstance> Armor = new();
        public int MaxArmorItems = 2;

        public BodyPart(BodyPartType type, List<BodyPartState> states = null, List<ItemInstance> armor = null)
        {
            this.Type = type;

            if (states != null)
            {
                this.States = states;
            }
            if (armor != null)
            {
                this.Armor = armor;
            }
        }

        public bool Wear(ItemInstance itemInstance)
        {
            if (itemInstance == null)
            {
                return false;
            }
            if (Armor.Count > MaxArmorItems)
            {
                return false;
            }
            if (States.Contains(BodyPartState.Missing))
            {
                return false;
            }

            Armor.Add(itemInstance);
            return true;
        }
    }

    public enum BodyPartType
    {
        Torso,
        Head,
    }

    public enum BodyPartState
    {
        Broken,
        Bleeding,
        Scratched,
        Bandaged,
        Missing,
    }
}
