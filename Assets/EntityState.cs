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

    protected float StateTimer;
    protected bool IsAnimationTriggered;

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
        IsAnimationTriggered = false;
    }

    public virtual void Update()
    {
        StateTimer -= Time.deltaTime;
        EntityAnimator.SetFloat(YVelocity, EntityRigidbody.linearVelocity.y);

        if (InputSet.Player.Dash.WasPressedThisFrame() && CanDash())
            EntityStateMachine.ChangeState(EntityPlayer.DashState);
    }

    public virtual void Exit()
    {
        EntityAnimator.SetBool(AnimBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        IsAnimationTriggered = true;
    }

    private bool CanDash()
    {
        if (EntityPlayer.WallDetected)
            return false;

        if (EntityStateMachine.CurrentState == EntityPlayer.DashState)
            return false;

        return true;
    }
}
