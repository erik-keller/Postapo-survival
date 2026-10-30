using ProjectPostapo.InventorySystem;
using System.Collections.Generic;

namespace ProjectPostapo.CreatureSystem
{
    public class BodyPart
    {
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

        public List<BodyPartState> States = new(); // Holds a state of a body part, like broken, bandaged or missing(e.g. after amputation)
        public List<ItemInstance> Armor = new(); // Holds armors weared on this body part
        public int MaxArmorItems = 2; // Max number of armors on this body part
        public bool CanHoldSomething; // If this body part is handy


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

        /// <summary>
        /// This method tries to wear an armor on the body part if it's possible 
        /// and return if it was succesful
        /// In the futute I want to add here validation if the armor is not to heavy.
        /// </summary>
        /// <param name="armorItemInstance">Armor instance that has to be weared</param>
        /// <returns>If the operation was succesful</returns>
        public bool Wear(ItemInstance armorItemInstance)
        {
            if (armorItemInstance == null)
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

            Armor.Add(armorItemInstance);
            return true;
        }
    }
}
