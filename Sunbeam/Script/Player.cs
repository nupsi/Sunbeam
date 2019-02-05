using Godot;

public class Player : KinematicBody2D
{
    private const int Gravity = 1200;
    private Direction m_previousDirection;
    private Vector2 m_velocity;
    private Sprite m_sprite;
    private bool m_jumping = false;
    private int m_jumpForce = -500;
    private int m_speed = 200;

    public override void _Ready()
    {
        //Force scale to 1.
        SetScale(Vector2.One);
        m_velocity = new Vector2(0, Gravity);
        m_sprite = (Sprite)GetNode("Sprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        UpdateInput();
        m_velocity.y += Gravity * delta;

        if(IsOnFloor())
        {
            m_previousDirection = Direction.None;
            if(m_jumping)
            {
                m_jumping = false;
            }
        }

        m_velocity = MoveAndSlide(m_velocity, new Vector2(0, -1), floorMaxAngle: 0.85f);
    }

    public void UpdateInput()
    {
        m_velocity.x = 0;

        if(JumpButton)
        {
            if(IsOnFloor())
            {
                Jump();
            }
            else if(IsOnWall())
            {
                WallJump();
            }
        }

        if(RightButton)
        {
            m_velocity.x += m_speed;
            UpdateVisuals();
        }
        if(LeftButton)
        {
            m_velocity.x -= m_speed;
            UpdateVisuals();
        }
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

        if(direction != m_previousDirection)
        {
            Jump();
            m_previousDirection = direction;
        }
    }

    private void UpdateVisuals()
    {
        m_sprite.SetFlipH(m_velocity.x < 0);
    }

    private bool RightButton => Input.IsActionPressed("game_right");
    private bool LeftButton => Input.IsActionPressed("game_left");
    private bool JumpButton => Input.IsActionPressed("game_jump");
}