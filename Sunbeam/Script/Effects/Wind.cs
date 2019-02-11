using Godot;

namespace Sunbeam.Effects
{
    public class Wind : Effect
    {
        private int m_streinght = 100;
        private Vector2 m_force;

        protected override void ApplyEffect(float delta)
        {
            var player = (Player)m_target;
            player.AddExternalForce(Force);
        }

        protected override bool OnlyOnGround => false;

        private Vector2 Force
        {
            get
            {
                if(m_force == Vector2.Zero)
                {
                    var name = GetName().ToLower();
                    if(name.Contains("left"))
                        m_force = new Vector2(-m_streinght, 0);
                    else if(name.Contains("right"))
                        m_force = new Vector2(m_streinght, 0);
                    else if(name.Contains("up"))
                        m_force = new Vector2(0, -m_streinght);
                    else if(name.Contains("down"))
                        m_force = new Vector2(0, m_streinght);
                    else
                        GD.Print("Add 'up', 'down', 'left' or 'right' to Area2D name that contains Wind script to define the wind direction");
                }

                return m_force;
            }
        }
    }
}