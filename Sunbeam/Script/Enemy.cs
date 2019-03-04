using Godot;

namespace Sunbeam.Script
{
    public class Enemy : LevelReset
    {
        [Export]
        public int Range = 150;

        private Direction m_direction;
        private Vector2 m_startPosition;
        private Sprite m_sprite;
        private bool m_cancel;
        private int m_speed = 1;

        public override void _Ready()
        {
            base._Ready();
            m_startPosition = Position;
            m_direction = Direction.Left;
            m_sprite = (Sprite)GetNode("Sprite");
            m_sprite.SetFlipH(true);
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            m_cancel = true;
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            var offset = new Vector2();
            switch(m_direction)
            {
                case Direction.Left:
                    offset.x -= m_speed;
                    if((Position + offset).x < m_startPosition.x - Range)
                    {
                        m_sprite.SetFlipH(false);
                        m_direction = Direction.Right;
                    }
                    break;

                case Direction.Right:
                    offset.x += m_speed;
                    if((Position + offset).x > m_startPosition.x + Range)
                    {
                        m_sprite.SetFlipH(true);
                        m_direction = Direction.Left;
                    }
                    break;
            }
            SetPosition(Position + offset);
        }
    }
}