public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(
        entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EntityPlayer.SetVelocity(0, EntityRigidbody.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (EntityPlayer.MoveInput.x != 0)
            EntityStateMachine.ChangeState(EntityPlayer.MoveState);
    }
}
