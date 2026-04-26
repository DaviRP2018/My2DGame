public class Player_AirState : EntityState
{
    public Player_AirState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (EntityPlayer.MoveInput.x != 0)
            EntityPlayer.SetVelocity(
                EntityPlayer.MoveInput.x *
                (EntityPlayer.moveSpeed * EntityPlayer.inAirMoveMultiplier),
                EntityRigidbody.linearVelocity.y);
    }
}
