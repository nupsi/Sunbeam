using Godot;

namespace Sunbeam.Effects
{
    public abstract class Effect : Trigger
    {
        protected Node m_target;

        public override void _PhysicsProcess(float delta)
        {
            if(m_target != null)
            {
                if(OnlyOnGround)
                {
                    if(!((KinematicBody2D)m_target).IsOnFloor())
                    {
                        return;
                    }
                }
                ApplyEffect(delta);
            }
        }

        protected override void EnterArea(object body)
        {
            if(IsTarget(body))
            {
                m_target = (Node)body;
            }
        }

        protected override void ExitArea(object body)
        {
            if(IsTarget(body))
            {
                m_target = null;
            }
        }

        protected abstract void ApplyEffect(float delta);
        protected abstract bool OnlyOnGround { get; }
        protected override bool ListenEnter => true;
        protected override bool ListenExit => true;
    }
}