public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(
        entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (EntityPlayer.MoveInput.x == 0)
            EntityStateMachine.ChangeState(EntityPlayer.IdleState);

        EntityPlayer.SetVelocity(EntityPlayer.MoveInput.x * EntityPlayer.moveSpeed,
            EntityRigidbody.linearVelocity.y);
    }
}
