using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class CharacterGravity : ICharacterConcreteMoving
    {
        private readonly AnimationCurve _gravityCurve;
        private readonly AnimationCurve _walkingCurve;

        private float _timeAccumulator;

        public CharacterGravity(AnimationCurve gravityCurve, AnimationCurve walkingCurve)
        {
            _gravityCurve = gravityCurve;
            _walkingCurve = walkingCurve;
        }

        public Vector3 CalculateConcreteMove(Vector3 direction, float timeAcc)
        {
            Vector3 movement = Vector3.zero;

            movement = Physics.gravity * _gravityCurve.Evaluate(_timeAccumulator);
            movement += direction * _walkingCurve.Evaluate(timeAcc);

            _timeAccumulator += Time.deltaTime;

            return movement;
        }

        public void Reset()
        {
            _timeAccumulator = 0.00f;
        }

    }

}

