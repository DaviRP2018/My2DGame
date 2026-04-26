public class Player_GroundedState : EntityState
{
    public Player_GroundedState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (EntityRigidbody.linearVelocity.y < 0 && !EntityPlayer.GroundDetected)
            EntityStateMachine.ChangeState(EntityPlayer.FallState);

        if (InputSet.Player.Jump.WasPerformedThisFrame())
            EntityStateMachine.ChangeState(EntityPlayer.JumpState);

        if (InputSet.Player.Attack.WasPerformedThisFrame())
            EntityStateMachine.ChangeState(EntityPlayer.BasicAttackState);
    }
}
