using Godot;

namespace Sunbeam
{
    public class Player : KinematicBody2D
    {
        private const string IdleAnimation = "Idle";
        private const string WalkAnimation = "Walk";
        private const string JumpAnimation = "Jump";
        private const string FallAnimation = "Fall";
        private const int Gravity = 1200;
        private Direction m_previousDirection;
        private Direction m_wallDirection;
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
        private AudioStreamPlayer2D m_jumpSound;

        public override void _Ready()
        {
            SetScale(Vector2.One);
            m_velocity = new Vector2(0, 0);
            m_sprite = (AnimatedSprite)GetNode("Sprite");
            m_jumpSound = (AudioStreamPlayer2D)GetNode("JumpSound");
        }

        public override void _Process(float delta)
        {
            UpdateInput();
        }

        public override void _PhysicsProcess(float delta)
        {
            UpdateVelocity(delta);
            UpdateVisuals();
            m_velocity = MoveAndSlide(m_velocity, new Vector2(0, -1), floorMaxAngle: 0.85f);
        }

        public void AddExternalForce(Vector2 force)
        {
            m_externalForce += force;
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
            if(m_velocity.x != 0)
            {
                m_velocity.x += m_velocity.x < 0 ? 20 : -20;
            }

            if(m_rightRequested)
            {
                m_velocity.x += m_speed;
            }

            if(m_leftRequested)
            {
                m_velocity.x -= m_speed;
            }

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
                        m_fromGround = true;
                        Jump();
                    }
                }
                else if(IsOnWall())
                {
                    WallJump();
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
            m_jumpSound.Play();
            m_jumping = true;
            m_velocity.y = m_jumpForce;
        }

        private void WallJump()
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

        private void UpdateVisuals()
        {
            if(m_sprite == null) return;
            m_sprite.SetFlipH((m_velocity.x == 0) ? m_sprite.IsFlippedH() : (m_velocity.x < 0));
            var animation = IsOnFloor()
                ? (m_velocity.x == 0 ? IdleAnimation : WalkAnimation)
                : (m_velocity.y < 0 ? JumpAnimation : FallAnimation);
            if(m_sprite.GetAnimation() != animation)
            {
                m_sprite.SetAnimation(animation);
            }
        }

        private bool RightButton => Input.IsActionPressed("game_right");
        private bool LeftButton => Input.IsActionPressed("game_left");
        private bool JumpButton => Input.IsActionPressed("game_jump");
        public Vector2 Velocity => m_velocity;
    }
}