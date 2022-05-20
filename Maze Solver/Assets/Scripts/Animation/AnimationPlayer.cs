using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer 
{
    // TODO � ����� PlayerFSM �������� ������� �� ����� ���������
    // ������� ���������� ������ � ��������� ���������������� ���������;
    // � ���� ������ �� ������� ������ �������� � ���� Animator

    private Animator _controller;
    private Dictionary<string, int> _triggersMap;

    private readonly string _runTriggerName = "Run";
    private readonly string _jumpTriggerName = "Jump";

    public AnimationPlayer(Animator controller)
    {
        _controller = controller;

        _triggersMap = new Dictionary<string, int>();
        _triggersMap.Add(_runTriggerName, Animator.StringToHash(_runTriggerName));
        _triggersMap.Add(_jumpTriggerName, Animator.StringToHash(_jumpTriggerName));
    }

    public void MakeTransition(string stateName)
    {
        _controller.SetTrigger(_triggersMap[stateName]);
    }
}
