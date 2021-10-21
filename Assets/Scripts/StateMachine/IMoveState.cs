using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveState
{
    void SetVelocity(Vector2 movementVelocity);
    Vector2 movementVelocity { get; set; }

    float movementSpeed { get; set; }
}
