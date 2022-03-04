using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCommand : Command
{

    private float speed;
    private Vector3 direction;

    public DriveCommand(IEntity _entity, float _time, Vector3 _direction, float _speed) : base(_entity, _time)
    {

        direction = _direction;
        speed = _speed;
        time = _time;

    }

    public override void Execute()
    {
        entity.rb.velocity = (speed) * direction;
    }


}
