public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(
        player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Player.MoveInput.x == 0)
            StateMachine.ChangeState(Player.IdleState);

        Player.SetVelocity(Player.MoveInput.x * Player.moveSpeed, Rigidbody2D.linearVelocity.y);
    }
}
