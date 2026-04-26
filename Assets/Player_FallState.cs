public class Player_FallState : Player_AirState
{
    public Player_FallState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (EntityPlayer.groundDetected)
            EntityStateMachine.ChangeState(EntityPlayer.IdleState);
    }
}
