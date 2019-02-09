using Godot;

namespace Sunbeam.Effects
{
    public class Ice : Effect
    {
        protected override void ApplyEffect(float delta)
        {
            GD.Print("Player under ice effect! " + (m_target as Player));
        }
    }
}