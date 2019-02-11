using Godot;

namespace Sunbeam.Effects
{
    /// <summary>
    /// Base class for creating special effects.
    /// Inherits Area2D and uses body_entered and body_exited to
    /// call ApplyEffect(delta) while player is inside the area.
    /// 
    /// To use this class create a class that inherits this class and 
    /// then create ApplyEffec(delta) and OnlyOnGround.
    /// </summary>
    public abstract class Effect : Area2D
    {
        /// <summary>
        /// Current target node colliding with the effect area.
        /// </summary>
        protected Node m_target;

        /// <summary>
        /// Node name we're looking for during body_entered and body_exited events.
        /// </summary>
        private string m_targetName = "Player";

        public override void _Ready()
        {
            //Add signals to body_entered and body_exited.
            //These are used to detect when is the target inside the area.
            Connect("body_entered", this, "EnterArea");
            Connect("body_exited", this, "ExitArea");
        }

        public override void _PhysicsProcess(float delta)
        {
            if(m_target != null)
            {
                if(OnlyOnGround)
                {
                    if(!((KinematicBody2D)m_target).IsOnFloor())
                    {
                        return;
                    }
                }
                ApplyEffect(delta);
            }
        }

        /// <summary>
        /// Checks if the target node has collided with the effect.
        /// </summary>
        /// <param name="body">Colliding node.</param>
        protected virtual void EnterArea(object body)
        {
            if((body as Node)?.GetName() == m_targetName)
            {
                m_target = (Node)body;
            }
        }

        /// <summary>
        /// Checks if the target node has stopped colliding with the effect.
        /// </summary>
        /// <param name="body">Colliding node.</param>
        protected virtual void ExitArea(object body)
        {
            if((body as Node)?.GetName() == m_targetName)
            {
                m_target = null;
            }
        }

        /// <summary>
        /// Applies effect on target node while it's colliding with the effect.
        /// </summary>
        /// <param name="delta">Physics Process delta time.</param>
        protected abstract void ApplyEffect(float delta);

        /// <summary>
        /// Is it required that the target node is on ground?
        /// </summary>
        protected abstract bool OnlyOnGround { get; }
    }
}