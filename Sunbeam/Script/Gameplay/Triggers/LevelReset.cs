using Godot;

namespace Sunbeam
{
    public class LevelReset : Trigger
    {
        protected override void EnterArea(object body)
        {
            var node = (Node)body;
            if(node.GetName() == m_targetName)
            {
                SceneManager.Instance.Reload();
            }
        }

        protected override void ExitArea(object body) { }
        protected override bool ListenEnter => true;
        protected override bool ListenExit => false;
    }
}