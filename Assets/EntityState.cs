using UnityEngine;

public abstract class EntityState
{
    protected Animator Animator;
    protected string AnimBoolName;
    protected Player Player;

    protected StateMachine StateMachine;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        AnimBoolName = animBoolName;

        Animator = player.Animator;
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
