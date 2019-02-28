using Godot;

namespace Sunbeam.UI
{
    public class Menu : Control
    {
        public void Quit()
        {
            SetPauseMode(PauseModeEnum.Process);
            GetTree().Quit();
        }

        protected SceneManager SceneManager => SceneManager.Instance;
    }
}