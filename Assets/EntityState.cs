using UnityEngine;

public abstract class EntityState
{
    protected Player Player;
    protected StateMachine StateMachine;
    protected string AnimBoolName;

    protected Animator Animator;
    protected Rigidbody2D Rigidbody2D;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        AnimBoolName = animBoolName;

        Animator = player.Animator;
        Rigidbody2D = Player.Rigidbody;
    }

    public virtual void Enter()
    {
        Animator.SetBool(AnimBoolName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        Animator.SetBool(AnimBoolName, false);
    }
}
