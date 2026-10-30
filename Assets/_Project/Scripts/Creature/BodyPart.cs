using ProjectPostapo.InventorySystem;
using System.Collections.Generic;

namespace ProjectPostapo.CreatureSystem
{
    public class BodyPart
    {
        public List<BodyPartState> States = new();
        public List<ItemInstance> Armor = new();
        public int MaxArmorItems = 2;
        public bool CanHoldSomething;

        public BodyPart(List<BodyPartState> states = null, List<ItemInstance> armor = null, bool canHoldSomething = false)
        {
            if (states != null)
            {
                this.States = states;
            }
            if (armor != null)
            {
                this.Armor = armor;
            }

            this.CanHoldSomething = canHoldSomething;
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
        LeftHand,
        RightHand,
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
