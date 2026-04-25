using UnityEngine;

public abstract class EntityState
{
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    protected Player EntityPlayer;
    protected StateMachine EntityStateMachine;
    protected string AnimBoolName;

    protected Animator EntityAnimator;
    protected Rigidbody2D EntityRigidbody;
    protected PlayerInputSet InputSet;

    public EntityState(Player entityPlayer, StateMachine entityStateMachine, string animBoolName)
    {
        EntityPlayer = entityPlayer;
        EntityStateMachine = entityStateMachine;
        AnimBoolName = animBoolName;

        EntityAnimator = entityPlayer.PlayerAnimator;
        EntityRigidbody = entityPlayer.PlayerRigidbody;
        InputSet = entityPlayer.PlayerInputActions;
    }

    public virtual void Enter()
    {
        EntityAnimator.SetBool(AnimBoolName, true);
    }

    public virtual void Update()
    {
        EntityAnimator.SetFloat(YVelocity, EntityRigidbody.linearVelocity.y);
    }

    public virtual void Exit()
    {
        EntityAnimator.SetBool(AnimBoolName, false);
    }
}
