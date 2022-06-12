using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer
{
    // TODO в класс PlayerFSM добавить событие на смену состояния
    // событие возвращает строку с названием соответствующего состояния;
    // В этом классе по событию менять анимации в типе Animator

    private Animator _controller;
    private Dictionary<string, int> _triggersMap;
    private ICharacterMover _characterMover;
    private int _speedHash;

    private readonly string _runTriggerName = "Run";
    private readonly string _jumpTriggerName = "Jump";
    private readonly string _speedFloatName = "Speed";
    private readonly string _dashFloatName = "Dash";

    public AnimationPlayer(Animator controller, ICharacterMover characterMover)
    {
        _characterMover = characterMover;
        _controller = controller;
        _triggersMap = new Dictionary<string, int>();
        _triggersMap.Add(MovementNames.MoveName, Animator.StringToHash(_runTriggerName));
        _triggersMap.Add(MovementNames.JumpName, Animator.StringToHash(_jumpTriggerName));
        _triggersMap.Add(MovementNames.DashName, Animator.StringToHash(_dashFloatName));
        _speedHash = Animator.StringToHash(_speedFloatName);
    }

    public void MakeTransition(string stateName)
    {
        if (string.IsNullOrEmpty(stateName)) 
        {
            return;
        }
        _controller.SetTrigger(_triggersMap[stateName]);
    }

    public void Update()
    {
        Vector3 velocity = _characterMover.Velocity;
        Vector2 planarVelocity = new Vector2(velocity.x, velocity.z);
        float speed = planarVelocity.magnitude;
        _controller.SetFloat(_speedHash, speed);
    }


}
