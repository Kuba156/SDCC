using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class CharacterJump : ICharacterConcreteMoving
    {
        private readonly AnimationCurve _jumpingCurve;
        private readonly AnimationCurve _walkingCurve;

        private float _timeAccumulator;

        public CharacterJump(AnimationCurve jumpingCurve, AnimationCurve walkingCurve)
        {
            _jumpingCurve = jumpingCurve;
            _walkingCurve = walkingCurve;
        }

        public Vector3 CalculateConcreteMove(Vector3 direction, float timeAcc)
        {
            Vector3 movement = Vector3.zero;

            movement = Vector3.up * _jumpingCurve.Evaluate(_timeAccumulator);
            movement += direction.normalized * _walkingCurve.Evaluate(timeAcc);

            _timeAccumulator += Time.deltaTime;

            return movement;
        }

        public void Reset()
        {
            _timeAccumulator = 0.00f;
        }

    }

}
