using System.Threading.Tasks;
using Godot;

namespace Sunbeam
{
    public class FallingObject : LevelReset
    {
        [Export]
        public bool Parent;

        private PackedScene m_current;
        private int m_waitTime = 10;
        private bool m_cancel;

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
                SetPosition(Position + new Vector2(0, 10 * delta));
            }
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            m_cancel = true;
        }

        protected override void EnterArea(object body)
        {
            base.EnterArea(body);
        }

        private void Destroy()
        {
            this.Dispose();
        }

        private void CreateInstance()
        {
            m_current = (PackedScene)ResourceLoader.Load(Resource);

            Task.Run(async () =>
            {
                while(m_current != null)
                {
                    if(m_cancel) return;
                    await Task.Delay(m_waitTime * 10);
                    while(Paused) await PauseDelay;
                }
                CreateInstance();
                return;
            });
        }

        private string Resource => string.Format("res://Prefabs/{0}.tscn", GetName());
        private bool Paused => GetTree().Paused;
        private Task PauseDelay => Task.Delay(10);
    }
}