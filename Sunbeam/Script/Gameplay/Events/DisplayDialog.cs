using Godot;
using Sunbeam.UI;
using Sunbeam.Data;

namespace Sunbeam.Events
{
    public class DisplayDialog : Event, IContainer
    {
        [Export]
        public string Text;

        private ActionData m_action;

        public override void _EnterTree()
        {
            base._EnterTree();
            m_action = new ActionData(GetName());
            SceneManager.Instance.ReqisterContainer(this);
        }

        protected override void TriggerEvent()
        {
            if(!m_action.Completed)
            {
                Dialog.Instance.ShowDialog(Text);
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