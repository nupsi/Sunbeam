using Godot;

namespace Sunbeam.UI
{
    public class PauseMenu : Menu
    {
        private bool m_returnState;

        public override void _Ready()
        {
            SetProcessInput(true);
            TogglePause(false, false);
        }

        public override void _Input(InputEvent @event)
        {
            if(Escape)
            {
                if(!Visible)
                {
                    m_returnState = GetTree().Paused;
                    Pause();
                }
                else
                {
                    Continue();
                }
            }
        }

        public void Pause()
        {
            TogglePause(true, true);
            SceneManager.Paused = true;
        }

        public void Continue()
        {
            TogglePause(false, m_returnState);
            SceneManager.Paused = false;
        }

        public void Restart()
        {
            GameManager.Instance.ResetSceneData();
            SceneManager.Reload();
        }

        public void MainMenu()
        {
            SceneManager.ChangeScene("MainMenu");
        }

        private void TogglePause(bool visible, bool paused)
        {
            GetTree().Paused = paused;
            Visible = visible;
        }

        private bool Escape => Input.IsActionJustPressed("ui_cancel");
    }
}