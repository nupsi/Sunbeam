using Godot;
using System;
//using System.Threading.Tasks;

namespace Sunbeam.UI
{
    public class Cutscene : Control
    {
        public static Cutscene Instance;

        private int m_blackBarSize = 80;
        private Panel m_top;
        private Panel m_bottom;
        private Label m_label;

        public override void _Ready()
        {
            Instance = this;
            m_top = (Panel)GetChild(0);
            m_bottom = (Panel)GetChild(1);
            m_label = (Label)GetChild(2);
        }

        public void ShowCutscene(string text)
        {
            m_label.Text = text;
            //Show(true);
        }

        public void HideCutscene()
        {
            m_label.Text = "";
            //Show(false);
        }

        //private async void Show(bool show)
        //{
        //    await new Task(async () =>
        //    {
        //        var delay = 10;
        //        var target = show ? m_blackBarSize : 0;
        //        var addition = show ? 2 : -2;

        //        while(m_top.MarginBottom != target)
        //        {
        //            m_top.MarginBottom += addition;
        //            m_bottom.MarginTop -= addition;
        //            await Task.Delay(delay);
        //        }
        //    });
        //}
    }
}
