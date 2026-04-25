using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet _input;
    private StateMachine _stateMachine;
    public Animator Animator { get; private set; }

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }

    public Vector2 MoveInput { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();

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
}
