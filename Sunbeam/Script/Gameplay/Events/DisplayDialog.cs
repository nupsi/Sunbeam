using Godot;
using Sunbeam.UI;
using Sunbeam.Data;

namespace Sunbeam.Events
{
    public class DisplayDialog : Event, IContainer
    {
        [Export]
        public string Text;

        [Export]
        public AudioStream Audio;

        private ActionData m_action;
        private bool m_inside;

        public override void _EnterTree()
        {
            base._EnterTree();
            m_action = new ActionData(GetName());
            SceneManager.Instance.ReqisterContainer(this);
        }

        protected override void EnterArea(object body)
        {
            base.EnterArea(body);
            if(IsTarget(body))
            {
                m_inside = true;
            }
        }

        protected override void ExitArea(object body)
        {
            base.ExitArea(body);
            if(IsTarget(body))
            {
                m_inside = false;
            }
        }

        protected override void TriggerEvent()
        {
            if(!m_action.Completed)
            {
                Dialog.Instance.ShowDialog(Text, Audio);
                m_action.Completed = true;
            }
        }

        public void SetData(IData data)
        {
            m_action = data as ActionData;
        }

        public IData GetData()
        {
            return m_action;
        }

        protected override bool ListenExit => false;
    }
}