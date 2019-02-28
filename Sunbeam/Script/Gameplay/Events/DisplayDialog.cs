using Godot;

namespace Sunbeam.Events
{
    public class DisplayDialog : Event
    {
        protected override void TriggerEvent()
        {
            GD.Print("Display Dialog");
        }

        protected override bool ListenExit => false;
    }
}