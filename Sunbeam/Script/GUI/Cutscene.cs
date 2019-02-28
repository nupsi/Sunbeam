using Godot;

namespace Sunbeam.UI
{
    public class Cutscene : Control
    {
        public static Cutscene Instance;

        private Tween.TransitionType transition = Tween.TransitionType.Linear;
        private Tween.EaseType ease = Tween.EaseType.In;
        private float m_blackBarSize = 80;
        private float time = 0.5f;
        private Panel m_top;
        private Panel m_bottom;
        private Label m_label;
        private Tween m_tween;

        public override void _Ready()
        {
            Instance = this;
            m_top = (Panel)GetNode("Top");
            m_bottom = (Panel)GetNode("Bottom");
            m_label = (Label)GetNode("Label");
            m_tween = (Tween)GetNode("Tween");
            m_label.Text = "";
        }

        public void ShowCutscene(string text)
        {
            m_label.Text = text;
            DoTween(m_blackBarSize);
        }

        public void HideCutscene()
        {
            m_label.Text = string.Empty;
            DoTween(0);
        }

        private void DoTween(float target)
        {
            m_tween.InterpolateMethod(this, "Top", m_top.MarginBottom, target, time, transition, ease);
            m_tween.InterpolateMethod(this, "Bottom", m_bottom.MarginTop, -target, time, transition, ease);
            m_tween.Start();
        }

        private void Top(float value)
        {
            m_top.MarginBottom = value;
        }

        private void Bottom(float value)
        {
            m_bottom.MarginTop = value;
        }
    }
}