using Godot;

namespace Sunbeam
{
    public class LevelEnd : Area2D
    {
        [Export]
        public string SceneName;

        public override void _Ready()
        {
            Connect("body_entered", this, "LoadScene");
        }

        private void LoadScene(object body)
        {
            var node = (Node)body;
            if(node.GetName() == "Player")
            {
                SceneManager.Instance.EndLevel(SceneName);
            }
        }
    }
}