using Godot;

namespace Sunbeam
{
    public class WallJump : KinematicBody2D
    {
        private const int Gravity = 1200;
        private Direction m_previousDirection;
        private Direction m_wallDirection;
        private Direction m_jumpDirection;
        private Vector2 m_velocity;
        private Vector2 m_externalForce;
        private AnimatedSprite m_sprite;
        private bool m_jumpRequested;
        private bool m_leftRequested;
        private bool m_rightRequested;
        private bool m_fromGround;
        private bool m_jumping = false;
        private int m_jumpForce = -500;
        private int m_speed = 200;
        private int m_maxSpeed = 200;

        public override void _Ready()
        {
            SetScale(Vector2.One);
            m_velocity = new Vector2(0, 0);
        }

        public override void _Process(float delta)
        {
            UpdateInput();
        }

        public override void _PhysicsProcess(float delta)
        {
            UpdateVelocity(delta);
            m_velocity = MoveAndSlide(m_velocity, new Vector2(0, -1), floorMaxAngle: 0.85f);
        }

        private void UpdateVelocity(float delta)
        {
            m_velocity.y += Gravity * delta;

            UpdateJump(delta);
            UpdateHorizontalVelocity();
            m_jumpRequested = false;
            m_leftRequested = false;
            m_rightRequested = false;
        }

        private void UpdateHorizontalVelocity()
        {
            if(!m_jumping)
            {
                m_velocity.x = Mathf.Lerp(m_velocity.x, 0, 1f);
            }

            m_velocity.x += HorizontalVelocity;

            m_velocity += m_externalForce;
            m_velocity.x = Mathf.Clamp(m_velocity.x, -m_maxSpeed, m_maxSpeed);
            m_externalForce = new Vector2();
        }

        private void UpdateInput()
        {
            if(!JumpButton)
            {
                m_fromGround = false;
            }
            m_jumpRequested = JumpButton;
            m_rightRequested = RightButton;
            m_leftRequested = LeftButton;
        }

        private void UpdateJump(float delta)
        {
            if(IsOnFloor())
            {
                m_previousDirection = Direction.None;
                if(m_jumping)
                {
                    m_jumping = false;
                }
            }


            if(m_jumpRequested)
            {
                if(IsOnFloor())
                {
                    if(!m_fromGround)
                    {
                        m_jumpDirection = HorizontalDirection;
                        m_fromGround = true;
                        Jump();
                        GD.Print("Jump! " + m_jumpDirection);
                    }
                }
                else if(IsOnWall())
                {
                    m_jumpDirection = Direction.None;
                    JumpWall();
                    m_wallDirection = m_previousDirection;
                }
            }
            else
            {
                m_velocity.y -= m_jumpForce * delta;
            }
        }

        private void Jump()
        {
            m_jumping = true;
            m_velocity.y = m_jumpForce;
        }

        private void JumpWall()
        {
            var offset = (int)Mathf.Clamp(GetSlideCollision(0).Position.x - Position.x, -1, 1);
            var direction = (offset == 0)
                ? Direction.None
                : (offset < 0)
                    ? Direction.Left
                    : Direction.Right;

            if(m_jumping && direction != m_previousDirection)
            {
                Jump();
                m_previousDirection = direction;
            }
        }

        private Direction HorizontalDirection 
            => (m_velocity.x == 0) 
                ? Direction.None 
                : (m_velocity.x < 0) 
                    ? Direction.Left 
                    : Direction.Right;

        private int HorizontalVelocity
        {
            get
            {
                return m_rightRequested
                    ? RightVelocity
                    : m_leftRequested
                        ? LeftVelocity
                        : 0;
            }
        }

        private int RightVelocity
        {
            get
            {
                var velocity = m_speed;
                if(m_jumping)
                {
                    if(m_jumpDirection == Direction.Left)
                    {
                        velocity /= 2;
                    }
                }
                return velocity;
            }
        }

        private int LeftVelocity
        {
            get
            {
                var velocity = m_speed;
                if(m_jumping)
                {
                    if(m_jumpDirection == Direction.Right)
                    {
                        velocity /= 2;
                    }
                }
                return -velocity;
            }
        }

        private bool RightButton => Input.IsActionPressed("game_right");
        private bool LeftButton => Input.IsActionPressed("game_left");
        private bool JumpButton => Input.IsActionPressed("game_jump");
        public Vector2 Velocity => m_velocity;
    }
}