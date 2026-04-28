public class Player_DashState : EntityState
{
    private float _originalGravityScale;
    private int _dashDirection;

    public Player_DashState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _dashDirection = EntityPlayer.MoveInput.x != 0
            ? (int)EntityPlayer.MoveInput.x
            : EntityPlayer.FacingDirection;
        StateTimer = EntityPlayer.dashDuration;

        _originalGravityScale = EntityRigidbody.gravityScale;
        EntityRigidbody.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        CancelDashIfNeeded();

        EntityPlayer.SetVelocity(EntityPlayer.dashSpeed * _dashDirection, 0);

        if (StateTimer < 0)
            if (EntityPlayer.GroundDetected)
                EntityStateMachine.ChangeState(EntityPlayer.IdleState);
            else
                EntityStateMachine.ChangeState(EntityPlayer.FallState);
    }

    public override void Exit()
    {
        base.Exit();

        EntityPlayer.SetVelocity(0, 0);
        EntityRigidbody.gravityScale = _originalGravityScale;
    }

    private void CancelDashIfNeeded()
    {
        if (EntityPlayer.WallDetected)
        {
            if (EntityPlayer.GroundDetected)
                EntityStateMachine.ChangeState(EntityPlayer.IdleState);
            else
                EntityStateMachine.ChangeState(EntityPlayer.WallSlideState);
        }
    }
}
