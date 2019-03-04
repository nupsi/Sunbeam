using Godot;
using Sunbeam.UI;

namespace Sunbeam.Events
{
    public class DisplayDialog : Event
    {
        [Export]
        public string Text;

        private bool m_showed;

        protected override void TriggerEvent()
        {
            if(!m_showed)
            {
                Dialog.Instance.ShowCutscene(Text);
                m_showed = true;
            }
        }

        protected override bool ListenExit => false;
    }
}