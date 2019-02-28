using Godot;

namespace Sunbeam.Events
{
    public abstract class Event : Trigger
    {
        protected override void EnterArea(object body)
        {
            if((body as Node)?.GetName() == m_targetName)
            {
                TriggerEvent();
            }
        }

        protected override void ExitArea(object body)
        {
            if(ListenExit)
            {
                if((body as Node)?.GetName() == m_targetName)
                {
                    ExitEvent();
                }
            }
        }

        protected abstract void TriggerEvent();
        protected virtual void ExitEvent() { }
        protected override bool ListenEnter => true;
    }
}