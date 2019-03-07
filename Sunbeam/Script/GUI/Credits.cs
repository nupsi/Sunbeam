using Godot;

namespace Sunbeam.UI
{
    public class Credits : Control
    {
        public void ShowCredits()
        {
            this.SetVisible(true);
        }

        public void HideCredits()
        {
            this.SetVisible(false);
        }
    }
}
