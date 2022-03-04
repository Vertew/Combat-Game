using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : Command
{

    private float maxRotationSpeed;

    public RotateCommand(IEntity _entity, float _time, float _maxRotationSpeed) : base(_entity, _time)
    {

        maxRotationSpeed = _maxRotationSpeed;
        time = _time;

    }

    public override void Execute()
    {
        entity.rb.angularVelocity = maxRotationSpeed;
    }

}
