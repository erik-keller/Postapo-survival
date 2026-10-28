using System.Collections.Generic;
using UnityEngine;

namespace ProjectPostapo.CreatureSystem
{
    public class BodyPart
    {
        public BodyPartType type;
        public List<BodyPartState> states;

        public BodyPart(BodyPartType type, List<BodyPartState> states = null)
        {
            this.type = type;

            if (states != null)
            {
                this.states = states;
            }
            else
            {
                this.states = new();
            }
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
    }
}
