using UnityEngine;

namespace ProjectPostapo.CreatureSystem
{
    public abstract class CreatureMovement : MonoBehaviour
    {
        public virtual void Move(Vector2 input, bool isSprinting) { }
        public virtual void Jump() { }
    }
}
