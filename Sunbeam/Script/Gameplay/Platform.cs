using Godot;
using System.Threading.Tasks;

namespace Sunbeam
{
    public class Platform : KinematicBody2D
    {
        [Export]
        public int BreakTime = 1;

        [Export]
        public int RespawnTime = 5;

        private string m_targetName = "Player";
        private CollisionShape2D m_collision;
        private ProgressBar m_bar;
        private bool m_triggered;

        public override void _Ready()
        {
            m_bar = (ProgressBar)GetNode("ProgressBar");
            m_collision = (CollisionShape2D)GetNode("CollisionShape2D");
            (GetNode("Area2D") as Area2D)?.Connect("body_entered", this, "BodyEnter");
        }

        public void BodyEnter(object body)
        {
            var player = body as KinematicBody2D;
            if(player.GetName() == m_targetName)
            {
                if(player.IsOnFloor() && !m_triggered)
                {
                    StartBreakTimer();
                }
            }
        }

        private async void StartBreakTimer()
        {
            m_triggered = true;
            await Task.Run(async () =>
            {
                while(m_bar.Value < 100)
                {
                    m_bar.Value++;
                    GD.Print(m_bar.Value);
                    await Task.Delay(BreakTime * 10);
                }
                StartRespawnTimer();
            });
        }

        private async void StartRespawnTimer()
        {
            Enabled(false);
            await Task.Run(async () =>
            {
                await Task.Delay(RespawnTime * 1000);
                m_bar.Value = 0;
                m_triggered = false;
                Enabled(true);
            });
        }

        private void Enabled(bool enabled)
        {
            Visible = enabled;
            m_collision.Disabled = !enabled;
        }
    }
}