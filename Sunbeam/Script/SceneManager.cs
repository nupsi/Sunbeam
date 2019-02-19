using Godot;

namespace Sunbeam
{
    public class SceneManager : Node
    {
        public static SceneManager Instance;

        [Export]
        public bool TrackTime;

        private float m_time;

        public override void _Ready()
        {
            Instance = this;
            m_time = 0;
            Tree.Paused = false;
        }

        public override void _Process(float delta)
        {
            if(TrackTime)
            {
                m_time += delta;
            }
        }

        public void EndLevel(string nextLevel)
        {
            if(TrackTime)
            {
                GD.Print("Level completed in " + (Mathf.Round(m_time * 10) / 10) + " seconds!");
            }
            ChangeScene(nextLevel);
        }

        public void ChangeScene(string name)
        {
            Tree.ChangeScene(GetScene(name));
        }

        public void Reload()
        {
            Tree.ReloadCurrentScene();
        }

        private void RealoadScene(object body)
        {
            var node = (Node)body;
            if(node.GetName() == "Player")
            {
                Reload();
            }
        }

        public static string GetScene(string _name)
        {
            return string.Format("res://Scenes/{0}.tscn", _name.Trim());
        }

        private SceneTree Tree => GetTree();
    }
}