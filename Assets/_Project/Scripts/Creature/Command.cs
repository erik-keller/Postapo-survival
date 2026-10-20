using UnityEngine;

namespace ProjectPostapo.CreatureSystem
{
    public abstract class Command
    {
        public virtual void Execute(ref CreatureMovement actor) { }
    }

    public class MoveCommand : Command
    {
        private Vector2 direction;
        private bool isSprinting;
        public MoveCommand(Vector2 input, bool sprinting)
        {
            direction = input;
            isSprinting = sprinting;
        }
        public override void Execute(ref CreatureMovement actor)
        {
            actor.Move(direction, isSprinting);
        }
    }
    public class JumpCommand : Command
    {
        public override void Execute(ref CreatureMovement actor)
        {
            actor.Jump();
        }
    }
}
