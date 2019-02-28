using Godot;

namespace Sunbeam.Effects
{
    public class Trampoline : Effect
    {
        [Export]
        public int Force = 1000;

        protected override void ApplyEffect(float delta)
        {
            var player = (Player)m_target;
            player.AddExternalForce(new Vector2(0, -Force));
        }

        protected override bool OnlyOnGround => true;
    }
}