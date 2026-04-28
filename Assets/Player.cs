using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator PlayerAnimator { get; private set; }
    public Rigidbody2D PlayerRigidbody { get; private set; }

    public PlayerInputSet PlayerInputActions { get; private set; }
    private StateMachine _stateMachine;

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_DashState DashState { get; private set; }
    public Player_BasicAttackState BasicAttackState { get; private set; }

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetTime = 1;

    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5;
    public Vector2 wallJumpForce;
    private bool _facingRight = true;
    public int FacingDirection { get; private set; } = 1;
    public Vector2 MoveInput { get; private set; }
    [Range(0, 1)]
    public float inAirMoveMultiplier = .8f;
    [Range(0, 1)]
    public float wallSlideMultiplier = .3f;
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20;

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayerMask;
    public bool GroundDetected { get; private set; }
    public bool WallDetected { get; private set; }

    private void Awake()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();

        _stateMachine = new StateMachine();
        PlayerInputActions = new PlayerInputSet();

        IdleState = new Player_IdleState(this, _stateMachine, "idle");
        MoveState = new Player_MoveState(this, _stateMachine, "move");
        JumpState = new Player_JumpState(this, _stateMachine, "jumpFall");
        FallState = new Player_FallState(this, _stateMachine, "jumpFall");
        WallSlideState = new Player_WallSlideState(this, _stateMachine, "wallSlide");
        WallJumpState = new Player_WallJumpState(this, _stateMachine, "jumpFall");
        DashState = new Player_DashState(this, _stateMachine, "dash");
        BasicAttackState = new Player_BasicAttackState(this, _stateMachine, "basicAttack");
    }

    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        _stateMachine.UpdateActiveState();
    }

    private void OnEnable()
    {
        PlayerInputActions.Enable();

        PlayerInputActions.Player.Movement.performed +=
            context => MoveInput = context.ReadValue<Vector2>();
        PlayerInputActions.Player.Movement.canceled += _ => MoveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }

    public void CallAnimationTrigger()
    {
        _stateMachine.CurrentState.CallAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        PlayerRigidbody.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !_facingRight || xVelocity < 0 && _facingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        _facingRight = !_facingRight;
        FacingDirection *= -1;
    }

    private void HandleCollisionDetection()
    {
        Vector3 origin = transform.position;

        Vector2 downDirection = Vector2.down;
        GroundDetected =
            Physics2D.Raycast(origin, downDirection, groundCheckDistance, groundLayerMask);

        Vector2 wallCheckDirection = Vector2.right * FacingDirection;
        WallDetected =
            Physics2D.Raycast(origin, wallCheckDirection, wallCheckDistance, groundLayerMask);
    }

    private void OnDrawGizmos()
    {
        Vector3 startingPoint = transform.position;

        Vector3 groundCheckPoint = startingPoint + new Vector3(0, -groundCheckDistance);
        Gizmos.DrawLine(startingPoint, groundCheckPoint);

        Vector3 wallCheckPoint =
            startingPoint + new Vector3(wallCheckDistance * FacingDirection, 0);
        Gizmos.DrawLine(startingPoint, wallCheckPoint);
    }
}
