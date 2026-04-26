public class Player_JumpState : Player_AirState
{
    public Player_JumpState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // TODO: O cara colocou X aqui, mas acho que é Y
        EntityPlayer.SetVelocity(EntityRigidbody.linearVelocity.y, EntityPlayer.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (EntityRigidbody.linearVelocity.y < 0)
            EntityStateMachine.ChangeState(EntityPlayer.FallState);
    }
}
