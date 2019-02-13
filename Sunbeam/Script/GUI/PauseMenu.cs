using Godot;

namespace Sunbeam.UI
{
    public class PauseMenu : Menu
    {
        public override void _Ready()
        {
            SetProcessInput(true);
            TogglePause(false);
        }

        public override void _Input(InputEvent @event)
        {
            if(Escape)
            {
                TogglePause(!GetTree().Paused);
            }
        }

        public void Pause()
        {
            TogglePause(true);
        }

        public void Continue()
        {
            TogglePause(false);
        }

        public void Restart()
        {
            SceneManager.Reload();
        }

        public void MainMenu()
        {
            SceneManager.ChangeScene("MainMenu");
        }

        private void TogglePause(bool paused)
        {
            GetTree().Paused = paused;
            Visible = paused;
        }

        private bool Escape => Input.IsActionJustPressed("ui_cancel");
    }
}