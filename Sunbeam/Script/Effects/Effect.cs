using Godot;

namespace Sunbeam.Effects
{
    public abstract class Effect : Area2D
    {
        protected Node m_target;
        private string m_targetName = "Player";

        public override void _Ready()
        {
            Connect("body_entered", this, "EnterArea");
            Connect("body_exited", this, "ExitArea");
        }

        public override void _PhysicsProcess(float delta)
        {
            if(m_target != null)
            {
                ApplyEffect(delta);
            }
        }

        protected virtual void EnterArea(object body)
        {
            if((body as Node)?.GetName() == m_targetName)
            {
                m_target = (Node)body;
            }
        }

        protected virtual void ExitArea(object body)
        {
            if((body as Node)?.GetName() == m_targetName)
            {
                m_target = null;
            }
        }

        protected abstract void ApplyEffect(float delta);
    }
}