using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private static readonly int BasicAttackIndex = Animator.StringToHash("basicAttackIndex");
    private float _attackVelocityTimer;
    private int _comboIndex = 0;
    private readonly int _comboLimit = 2;
    private const int FirstComboIndex = 0;

    private float _lastTimeAttacked;

    public Player_BasicAttackState(Player entityPlayer, StateMachine entityStateMachine,
        string animBoolName) : base(entityPlayer, entityStateMachine, animBoolName)
    {
        if (_comboLimit != EntityPlayer.attackVelocity.Length)
        {
            Debug.LogWarning("Combo limit adjusted according to attack velocity array");
            _comboLimit = EntityPlayer.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIndexIfNeeded();

        EntityAnimator.SetInteger(BasicAttackIndex, _comboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (IsAnimationTriggered)
            EntityStateMachine.ChangeState(EntityPlayer.IdleState);
    }

    public override void Exit()
    {
        base.Exit();

        _comboIndex++;
        _lastTimeAttacked = Time.time;
    }

    private void HandleAttackVelocity()
    {
        _attackVelocityTimer -= Time.deltaTime;

        if (_attackVelocityTimer < 0)
            EntityPlayer.SetVelocity(0, EntityRigidbody.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        _attackVelocityTimer -= EntityPlayer.attackVelocityDuration;
        EntityPlayer.SetVelocity(
            EntityPlayer.attackVelocity[_comboIndex].x * EntityPlayer.FacingDirection,
            EntityPlayer.attackVelocity[_comboIndex].y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > _lastTimeAttacked + EntityPlayer.comboResetTime ||
            _comboIndex > _comboLimit)
            _comboIndex = FirstComboIndex;
    }
}
