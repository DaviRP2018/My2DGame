using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet _input;
    private StateMachine _stateMachine;

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }

    public Vector2 MoveInput { get; private set; }

    private void Awake()
    {
        _stateMachine = new StateMachine();
        _input = new PlayerInputSet();

        IdleState = new Player_IdleState(this, _stateMachine, "Idle State");
        MoveState = new Player_MoveState(this, _stateMachine, "Move");
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

    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        _stateMachine.UpdateActiveState();
    }
}
