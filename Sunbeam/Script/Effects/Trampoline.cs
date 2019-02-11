using Godot;

namespace Sunbeam.Effects
{
    public class Trampoline : Effect
    {
        private int m_force = 1000;

        protected override void ApplyEffect(float delta)
        {
            var player = (Player)m_target;
            player.AddExternalForce(new Vector2(0, -m_force));
        }

        protected override bool OnlyOnGround => true;
    }
}
