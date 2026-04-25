public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animBoolName) : base(
        player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Player.MoveInput.x != 0)
            StateMachine.ChangeState(Player.MoveState);
    }
}
