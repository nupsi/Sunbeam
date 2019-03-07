using Godot;

namespace Sunbeam.UI
{
    public class MainMenu : Menu
    {
        [Export]
        public string PlaySceneName;

        private void Play()
        {
            SceneManager.ChangeScene(PlaySceneName);
        }
    }
}