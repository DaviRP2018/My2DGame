public class Player_JumpState : EntityState
{
    public Player_JumpState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EntityPlayer.SetVelocity(EntityRigidbody.linearVelocity.y, EntityPlayer.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (EntityRigidbody.linearVelocity.y < 0)
            EntityStateMachine.ChangeState(EntityPlayer.FallState);
    }
}
