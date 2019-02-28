using Godot;

namespace Sunbeam.Effects
{
    public class Wind : Effect
    {
        [Export]
        public Direction Direction;

        [Export]
        public int Strength = 100;

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
                    switch(Direction)
                    {
                        case Direction.Down:
                            m_force = new Vector2(0, Strength);
                            break;

                        case Direction.Up:
                            m_force = new Vector2(0, -Strength);
                            break;

                        case Direction.Left:
                            m_force = new Vector2(-Strength, 0);
                            break;

                        case Direction.Right:
                            m_force = new Vector2(Strength, 0);
                            break;

                        default:
                            GD.Print("Set Wind Direction!");
                            break;
                    }
                }

                return m_force;
            }
        }
    }
}