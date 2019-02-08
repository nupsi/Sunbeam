using Godot;

public class Player : KinematicBody2D
{
    private const int Gravity = 1200;
    private Direction m_previousDirection;
    private Vector2 m_velocity;
    private Sprite m_sprite;
    private bool m_jumpRequested;
    private bool m_leftRequested;
    private bool m_rightRequested;
    private bool m_fromGround;
    private bool m_jumping = false;
    private int m_jumpForce = -500;
    private int m_speed = 200;

    public override void _Ready()
    {
        SetScale(Vector2.One);
        m_velocity = new Vector2(0, 0);
        m_sprite = (Sprite)GetNode("Sprite");
    }

    public override void _Process(float delta)
    {
        if(!JumpButton)
        {
            m_fromGround = false;
        }
        m_jumpRequested = JumpButton;
        m_rightRequested = RightButton;
        m_leftRequested = LeftButton;
    }

    public override void _PhysicsProcess(float delta)
    {
        m_velocity.y += Gravity * delta;
        if(IsOnFloor())
        {
            m_previousDirection = Direction.None;
            if(m_jumping)
            {
                m_jumping = false;
            }
        }

        UpdateInput(delta);
        UpdateVisuals();
        m_velocity = MoveAndSlide(m_velocity, new Vector2(0, -1), floorMaxAngle: 0.85f);
    }

    public void UpdateInput(float delta)
    {
        m_velocity.x = 0;
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
            }
        }
        else
        {
            m_velocity.y -= m_jumpForce * delta;
        }

        if(m_rightRequested)
        {
            m_velocity.x += m_speed;
        }

        if(m_leftRequested)
        {
            m_velocity.x -= m_speed;
        }

        m_jumpRequested = false;
        m_leftRequested = false;
        m_rightRequested = false;
    }

    private void Jump()
    {
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
        m_sprite?.SetFlipH(m_velocity.x < 0);
    }

    private bool RightButton => Input.IsActionPressed("game_right");
    private bool LeftButton => Input.IsActionPressed("game_left");
    private bool JumpButton => Input.IsActionPressed("game_jump");
}