using Godot;

namespace Sunbeam.UI
{
    public class Menu : Control
    {
        public void Quit()
        {
            GetTree().Quit();
        }

        protected SceneManager SceneManager => SceneManager.Instance;
    }
}