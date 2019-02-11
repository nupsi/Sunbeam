using Godot;

namespace Sunbeam.Effects
{
    /// <summary>
    /// Icey Ground effect.
    /// Makes the target slide on the ground.
    /// Stores targets previous velocity and adds it as external 
    /// force on next physics event.
    /// </summary>
    public class Ice : Effect
    {
        /// <summary>
        /// Targets previous velocity.
        /// </summary>
        private Vector2 m_previousVelocity;

        protected override void ApplyEffect(float delta)
        {
            var player = (Player)m_target;
            player.AddExternalForce(new Vector2(m_previousVelocity.x * 0.95f, 0));
            m_previousVelocity = player.Velocity;
        }

        protected override bool OnlyOnGround => true;
    }
}