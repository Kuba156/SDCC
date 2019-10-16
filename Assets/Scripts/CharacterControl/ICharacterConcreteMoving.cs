using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public interface ICharacterConcreteMoving
    {
        Vector3 CalculateConcreteMove(Vector3 direction, float timeAcc);
        void Reset();
    }

}
