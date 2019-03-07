namespace Sunbeam.Events
{
    public abstract class Event : Trigger
    {
        protected override void EnterArea(object body)
        {
            if(IsTarget(body))
            {
                TriggerEvent();
            }
        }

        protected override void ExitArea(object body)
        {
            if(ListenExit)
            {
                if(IsTarget(body))
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