using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public interface IInputReceiver
    {
        Vector3 GetHorizontalMovement();
        Vector3 GetVerticalMovement();
        bool Jumping();
        bool Sprinting();
    }

}
