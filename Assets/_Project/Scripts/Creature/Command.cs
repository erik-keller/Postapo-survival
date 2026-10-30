using UnityEngine;

namespace ProjectPostapo.CreatureSystem
{
    /// <summary>
    /// Abstract class that holds an Execute method that can be implemented in child classes
    /// </summary>
    public abstract class Command
    {
        public virtual void Execute(ref CreatureMovement actor) { }
    }

    /// <summary>
    /// Class used to execute moving in CreatureMovement
    /// </summary>
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

    /// <summary>
    /// Class used to execute jumping in CreatureMovement
    /// </summary>
    public class JumpCommand : Command
    {
        public override void Execute(ref CreatureMovement actor)
        {
            actor.Jump();
        }
    }
}
