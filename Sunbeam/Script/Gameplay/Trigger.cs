using Godot;

namespace Sunbeam
{
    public abstract class Trigger : Area2D
    {
        protected string m_targetName = "Player";

        public override void _Ready()
        {
            if(ListenEnter)
            {
                Connect("body_entered", this, "EnterArea");
            }

            if(ListenExit)
            {
                Connect("body_exited", this, "ExitArea");
            }
        }

        protected bool IsTarget(object body)
        {
            return (body as Node)?.GetName() == m_targetName;
        }

        protected abstract void EnterArea(object body);
        protected abstract void ExitArea(object body);
        protected abstract bool ListenEnter { get; }
        protected abstract bool ListenExit { get; }
    }
}