public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (InputSet.Player.Jump.WasPressedThisFrame())
            EntityStateMachine.ChangeState(EntityPlayer.WallJumpState);

        if (!EntityPlayer.WallDetected)
            EntityStateMachine.ChangeState(EntityPlayer.FallState);

        if (EntityPlayer.GroundDetected)
        {
            EntityStateMachine.ChangeState(EntityPlayer.IdleState);
            EntityPlayer.Flip();
        }
    }

    private void HandleWallSlide()
    {
        float yVelocity;

        if (EntityPlayer.MoveInput.y < 0)
            yVelocity = EntityRigidbody.linearVelocity.y;
        else
            yVelocity = EntityRigidbody.linearVelocity.y * EntityPlayer.wallSlideMultiplier;

        EntityPlayer.SetVelocity(EntityPlayer.MoveInput.x, yVelocity);
    }
}
