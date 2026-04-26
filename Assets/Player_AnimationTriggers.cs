using UnityEngine;

public class Player_AnimationsTriggers : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    public void CurrentStateTrigger()
    {
        _player.CallAnimationTrigger();
    }
}
