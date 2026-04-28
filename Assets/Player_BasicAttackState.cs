using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private static readonly int BasicAttackIndex = Animator.StringToHash("basicAttackIndex");
    private float _attackVelocityTimer;
    private int _comboIndex;
    private readonly int _comboLimit = 2;
    private const int FirstComboIndex = 0;
    private bool _comboAttackQueued;
    private int _attackDirection;
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

        _comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        _attackDirection = EntityPlayer.MoveInput.x != 0
            ? (int)EntityPlayer.MoveInput.x
            : EntityPlayer.FacingDirection;

        EntityAnimator.SetInteger(BasicAttackIndex, _comboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (InputSet.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (IsAnimationTriggered)
            HandleStateExit();
    }

    private void HandleStateExit()
    {
        if (_comboAttackQueued)
        {
            EntityAnimator.SetBool(AnimBoolName, false);
            EntityPlayer.EnterAttackStateWithDelay();
        }
        else
            EntityStateMachine.ChangeState(EntityPlayer.IdleState);
    }

    public override void Exit()
    {
        base.Exit();

        _comboIndex++;
        _lastTimeAttacked = Time.time;
    }

    private void QueueNextAttack()
    {
        if (_comboIndex < _comboLimit)
            _comboAttackQueued = true;
    }

    private void HandleAttackVelocity()
    {
        _attackVelocityTimer -= Time.deltaTime;

        if (_attackVelocityTimer < 0)
            EntityPlayer.SetVelocity(0, EntityRigidbody.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = EntityPlayer.attackVelocity[_comboIndex];

        _attackVelocityTimer -= EntityPlayer.attackVelocityDuration;
        EntityPlayer.SetVelocity(attackVelocity.x * _attackDirection, attackVelocity.y);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > _lastTimeAttacked + EntityPlayer.comboResetTime ||
            _comboIndex > _comboLimit)
            _comboIndex = FirstComboIndex;
    }
}
