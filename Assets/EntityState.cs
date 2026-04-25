using UnityEngine;

public abstract class EntityState
{
    protected Player Player;
    
    protected StateMachine StateMachine;
    protected string StateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        Player = player;
        StateMachine = stateMachine;
        StateName = stateName;
    }
    
    public virtual void Enter()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}
