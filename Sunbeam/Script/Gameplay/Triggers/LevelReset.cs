using Godot;

namespace Sunbeam
{
    public class LevelReset : Trigger
    {
        protected override void EnterArea(object body)
        {
            if(IsTarget(body))
            {
                SceneManager.Instance.Reload();
            }
        }

        protected override void ExitArea(object body) { }
        protected override bool ListenEnter => true;
        protected override bool ListenExit => false;
    }
}