public class Player_WallJumpState : EntityState
{
    public Player_WallJumpState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EntityPlayer.SetVelocity(EntityPlayer.wallJumpForce.x * -EntityPlayer.FacingDirection,
            EntityPlayer.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (EntityRigidbody.linearVelocity.y < 0)
            EntityStateMachine.ChangeState(EntityPlayer.FallState);

        if (EntityPlayer.WallDetected)
            EntityStateMachine.ChangeState(EntityPlayer.WallSlideState);
    }
}
