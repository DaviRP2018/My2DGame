using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;
    
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }

    private void Awake()
    {
        _stateMachine = new StateMachine();

        IdleState = new Player_IdleState(this, _stateMachine, "Idle State");
        MoveState = new Player_MoveState(this, _stateMachine, "Move");
    }

    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        _stateMachine.CurrentState.Update();
    }
}
