using Godot;
using Sunbeam.UI;

namespace Sunbeam.Events
{
    public class DisplayCutscene : Event
    {
        [Export]
        public string Text;

        protected override void TriggerEvent()
        {
            Cutscene.Instance.ShowCutscene(Text);
        }

        protected override void ExitEvent()
        {
            Cutscene.Instance.HideCutscene();
        }

        protected override bool ListenExit => true;
    }
}