using Godot;

namespace Sunbeam
{
    public class Checkpoint : Trigger
    {
        [Export]
        public int CheckpointIndex;

        public override void _EnterTree()
        {
            SceneManager.Instance.ReqisterCheckpoint(this);
        }

        protected override void EnterArea(object body)
        {
            var node = (Node)body;
            if(node.GetName() == m_targetName)
            {
                GameManager.Instance.SetCheckpoint(CheckpointIndex);
            }
        }

        protected override void ExitArea(object body) { }
        protected override bool ListenEnter => true;
        protected override bool ListenExit => false;
    }
}