using Godot;

namespace Sunbeam
{
    public class FallingObject : LevelReset
    {
        [Export]
        public bool Parent;

        [Export]
        public string PrefabName;

        private FallingObject m_parent;
        private Node m_current;

        public override void _Ready()
        {
            base._Ready();
            if(Parent)
            {
                CreateInstance();
            }
        }

        public override void _Process(float delta)
        {
            if(!Parent)
            {
                SetPosition(Position + new Vector2(0, 100 * delta));
            }
        }

        protected override void EnterArea(object body)
        {
            base.EnterArea(body);
            if(!Parent)
            {
                if((body as TileMap) != null)
                {
                    m_parent.ClearInstance(this);
                }
            }
        }

        public void SetParent(FallingObject parent)
        {
            m_parent = parent;
        }

        public void ClearInstance(Node node)
        {
            node.QueueFree();
            m_current = node;
            CreateInstance();
        }

        private void CreateInstance()
        {
            m_current = LoadPrefab().Instance();
            ((FallingObject)m_current).SetParent(this);
            this.AddChild(m_current);
        }

        private PackedScene LoadPrefab()
        {
            return (PackedScene)ResourceLoader.Load(Resource);
        }

        private string Resource => string.Format("res://Prefabs/{0}.tscn", PrefabName);
    }
}