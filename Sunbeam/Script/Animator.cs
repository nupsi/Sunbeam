using Godot;

namespace Sunbeam
{
    public class Animator : AnimationPlayer
    {
        [Export]
        public string AnimationName;

        private bool m_played = false;

        public void PlayAnimation(object body)
        {
            if(!m_played)
            {
                if((body as Node)?.GetName() == "Player")
                {
                    this.GetAnimation(AnimationName).Loop = false;
                    this.Play(AnimationName);
                    m_played = true;
                }
            }
        }
    }
}
