using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class CharacterWallrun : ICharacterConcreteMoving
    {
        private readonly AnimationCurve _wallrunHorizontalCurve;
        private readonly AnimationCurve _wallrunVerticalCurve;
        private readonly AnimationCurve _walkingCurve;

        private float _timeAccumulator;

        public CharacterWallrun(AnimationCurve wallrunHorizontalCurve, AnimationCurve wallrunVerticalCurve, AnimationCurve walkingCurve)
        {
            _wallrunHorizontalCurve = wallrunHorizontalCurve;
            _wallrunVerticalCurve = wallrunVerticalCurve;
            _walkingCurve = walkingCurve;
        }

        public Vector3 CalculateConcreteMove(Vector3 direction, float timeAcc)
        {
            Vector3 movement = direction.normalized;

            movement *= _walkingCurve.Evaluate(timeAcc); // get the preserved speed
            movement += movement * _wallrunHorizontalCurve.Evaluate(_timeAccumulator); // add the wallrun speed to it
            movement += Vector3.up * _wallrunVerticalCurve.Evaluate(_timeAccumulator); // add the vertical movement

            _timeAccumulator += Time.deltaTime;

            return movement;
        }

        public void Reset()
        {
            _timeAccumulator = 0.00f;
        }

    }

}
