using Godot;

namespace Sunbeam.UI
{
    public class Dialog : Control
    {
        public static Dialog Instance;
        private static string Color = "font_color";

        private Tween.TransitionType transition = Tween.TransitionType.Linear;
        private Tween.EaseType ease = Tween.EaseType.In;
        private float m_topBarSize = 80;
        private float m_bottomBarSize = 140;
        private float time = 0.5f;
        private float m_alpha;
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
            m_tween.Connect("tween_completed", this, "TweenCompleted");
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
                    HideDialog();
                }
            }
        }

        public override void _Process(float delta)
        {
            if(m_hint.Visible)
            {
                var color = FontColor;
                color.a += m_alpha * delta;
                if(color.a <= 0) m_alpha = 1;
                if(color.a >= 1) m_alpha = -1;
                FontColor = color;
            }
        }

        public void ShowDialog(string text)
        {
            SetLabelsVisible(false);
            FontColor = new Color(1, 1, 1, 1);
            m_alpha = -1;
            Paused = true;
            m_label.Text = text;
            DoTween(m_topBarSize, m_bottomBarSize);
        }

        public void HideDialog()
        {
            SetLabelsVisible(false);
            m_label.Text = string.Empty;
            DoTween(0, 0);
            Paused = false;
        }

        private void DoTween(float top, float bottom)
        {
            m_tween.InterpolateMethod(this, "Top", m_top.MarginBottom, top, time, transition, ease);
            m_tween.InterpolateMethod(this, "Bottom", m_bottom.MarginTop, -bottom, time, transition, ease);
            m_tween.Start();
        }

        public void TweenCompleted(object sender, NodePath nodePath)
        {
            if(nodePath == ":Bottom")
            {
                SetLabelsVisible(!(m_bottom.MarginTop == 0));
            }
        }

        private void SetLabelsVisible(bool visible)
        {
            m_hint.Visible = visible;
            m_label.Visible = visible;
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

        private Color FontColor
        {
            get => m_hint.GetColor(Color);
            set => m_hint.AddColorOverride(Color, value);
        }

        private bool Paused
        {
            get => GetTree().Paused;
            set => GetTree().Paused = value;
        }
    }
}