using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float _attackVelocityTimer;

    public Player_BasicAttackState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GenerateAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (IsAnimationTriggered)
            EntityStateMachine.ChangeState(EntityPlayer.IdleState);
    }

    private void HandleAttackVelocity()
    {
        _attackVelocityTimer -= Time.deltaTime;

        if (_attackVelocityTimer < 0)
            EntityPlayer.SetVelocity(0, EntityRigidbody.linearVelocity.y);
    }

    private void GenerateAttackVelocity()
    {
        _attackVelocityTimer -= EntityPlayer.attackVelocityDuration;
        EntityPlayer.SetVelocity(EntityPlayer.attackVelocity.x * EntityPlayer.FacingDirection,
            EntityPlayer.attackVelocity.y);
    }
}
