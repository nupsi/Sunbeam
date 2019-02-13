using Godot;

namespace Sunbeam.UI
{
    public class MainMenu : Menu
    {
        [Export]
        public string PlaySceneName;

        [Export]
        public string PlayTestSceneName;

        private void Play()
        {
            SceneManager.ChangeScene(PlaySceneName);
        }

        private void PlayTest()
        {
            SceneManager.ChangeScene(PlayTestSceneName);
        }
    }
}