using Godot;

namespace Sunbeam.Events
{
    public class DisplayCutscene : Event
    {
        protected override void TriggerEvent()
        {
            GD.Print("Display Cutscene");
        }

        protected override void ExitEvent()
        {
            GD.Print("Hide Cutscene");
        }

        protected override bool ListenExit => true;
    }
}