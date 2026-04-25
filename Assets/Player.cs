using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private PlayerInputSet _input;
    private StateMachine _stateMachine;

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }

    public Vector2 MoveInput { get; private set; }

    [Header("Movement details")]
    public float moveSpeed;

    private bool _facingRight = true;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        _stateMachine = new StateMachine();
        _input = new PlayerInputSet();

        IdleState = new Player_IdleState(this, _stateMachine, "idle");
        MoveState = new Player_MoveState(this, _stateMachine, "move");
    }

    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        _stateMachine.UpdateActiveState();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Movement.performed += context => MoveInput = context.ReadValue<Vector2>();
        _input.Player.Movement.canceled += _ => MoveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rigidbody.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && !_facingRight || xVelocity < 0 && _facingRight)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        _facingRight = !_facingRight;
    }
}
