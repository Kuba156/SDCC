using UnityEngine;

namespace CharacterControl
{
    public interface ICharacterMoving
    {
        Vector3 CalculateMovement(float accumulatedTime);
    }

}

