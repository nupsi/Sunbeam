using Godot;

namespace Sunbeam.UI
{
    public class Dialog : Control
    {
        public static Dialog Instance;

        private Tween.TransitionType transition = Tween.TransitionType.Linear;
        private Tween.EaseType ease = Tween.EaseType.In;
        private float m_topBarSize = 80;
        private float m_bottomBarSize = 140;
        private float time = 0.5f;
        private Panel m_top;
        private Panel m_bottom;
        private Label m_label;
        private Label m_hint;
        private Tween m_tween;

        public override void _Ready()
        {
            Instance = this;
            SetProcessInput(true);
            SetPauseMode(PauseModeEnum.Process);
            m_top = (Panel)GetNode("Top");
            m_bottom = (Panel)GetNode("Bottom");
            m_tween = (Tween)GetNode("Tween");
            m_label = (Label)GetNode("Label");
            m_label.MarginTop = -m_bottomBarSize;
            m_label.Text = "";
            m_hint = (Label)GetNode("Hint");
            m_hint.Visible = false;
        }

        public override void _Input(InputEvent @event)
        {
            if(m_hint.Visible)
            {
                if(CloseInput)
                {
                    HideCutscene();
                }
            }
        }

        public void ShowCutscene(string text)
        {
            GetTree().Paused = true;
            m_hint.Visible = true;
            m_label.Text = text;
            DoTween(m_topBarSize, m_bottomBarSize);
        }

        public void HideCutscene()
        {
            m_hint.Visible = false;
            m_label.Text = string.Empty;
            DoTween(0, 0);
            GetTree().Paused = false;
        }

        private void DoTween(float top, float bottom)
        {
            m_tween.InterpolateMethod(this, "Top", m_top.MarginBottom, top, time, transition, ease);
            m_tween.InterpolateMethod(this, "Bottom", m_bottom.MarginTop, -bottom, time, transition, ease);
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

        private bool CloseInput
        {
            get
            {
                return (!SceneManager.Instance.Paused
                    && Input.IsActionJustReleased("game_jump"));
            }
        }
    }
}