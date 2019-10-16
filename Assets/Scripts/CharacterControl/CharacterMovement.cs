using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class CharacterMovement : ICharacterMoving
    {
        private readonly CharacterController _characterController;
        private readonly AnimationCurve _movingCurve;
        private Transform _rotationTarget;

        private readonly IInputReceiver _inputReceiver;

        public CharacterMovement(
            CharacterController characterController, AnimationCurve walkingCurve, Transform rotationTarget,
            IInputReceiver inputReceiver)
        {
            _characterController = characterController;
            _movingCurve = walkingCurve;
            _rotationTarget = rotationTarget;
            _inputReceiver = inputReceiver;
        }

        public Vector3 CalculateMovement(float timeAccumulator)
        {
            Vector3 movement = Vector3.zero;

            Vector3 horizontalMovement = _inputReceiver.GetHorizontalMovement();
            Vector3 verticalMovement = _inputReceiver.GetVerticalMovement();

            if (horizontalMovement == Vector3.zero && verticalMovement == Vector3.zero)
            {
                return movement;
            }

            Vector3 movementSum = (horizontalMovement + verticalMovement);
            Vector3 flattenedMovementSum = new Vector3(movementSum.x, 0, movementSum.z);
            Vector3 normalisedFlattenedMovementSum = flattenedMovementSum.normalized;

            movement = flattenedMovementSum * _movingCurve.Evaluate(timeAccumulator);
            _rotationTarget.transform.forward = flattenedMovementSum;

            return movement;
            
        }

    }

}
