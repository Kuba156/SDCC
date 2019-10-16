using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class WallrunCharacterState : ConcreteMovingCharacterState
    {
        private enum WallPosition { RIGHT, LEFT }

        private readonly IInputReceiver _inputReceiver;
        private readonly AnimationCurve _horizontalWallrunCurve;

        private Vector3 _wallrunDirection;
        private WallPosition _wallPosition;
        private float _localTimeAccumulator;


        public WallrunCharacterState(
            IInputReceiver inputReceiver, AnimationCurve wallrunCurve,
            ICharacterConcreteMoving concreteMove, Transform meshTransform, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(concreteMove, meshTransform, characterController, animator, tickables)
        {
            _inputReceiver = inputReceiver;
            _horizontalWallrunCurve = wallrunCurve;
        }

        public override void Enter(BaseCharacterState baseCharacterState)
        {
            base.Enter(baseCharacterState);
            _localTimeAccumulator = 0.00f;
            _animator.SetBool("Wallrun", true);

            int layerMask = 31;
            RaycastHit hit;

            if (Physics.Raycast(_meshTransform.position, _meshTransform.right, out hit, 2.0f, layerMask))
            {
                //_wallrunDirection = hit.collider.gameObject.transform.forward;
                _wallrunDirection = -Vector3.Cross(hit.normal, Vector3.up);
                _wallPosition = WallPosition.RIGHT;
            }
            else if (Physics.Raycast(_meshTransform.position, -_meshTransform.right, out hit, 2.0f, layerMask))
            {
                //_wallrunDirection = -hit.collider.gameObject.transform.forward;
                _wallrunDirection = Vector3.Cross(hit.normal, Vector3.up);
                _wallPosition = WallPosition.LEFT;
            }

            _meshTransform.forward = _wallrunDirection;

        }

        public override void Exit(BaseCharacterState nextState)
        {
            base.Exit(nextState);
            _localTimeAccumulator = 0.00f;
            _animator.SetBool("Wallrun", false);
        }

        public override Type Tick()
        {
            Vector3 movement = SetUpMovement(_wallrunDirection);

            ApplyMovement(movement);

            if (_characterController.isGrounded || // ground touched
                _characterController.collisionFlags == CollisionFlags.Above || // ceiling hit with head
                _localTimeAccumulator >= _horizontalWallrunCurve.keys[1].time) // time to start falling down
            {
                return typeof(RunCharacterState);
            }

            if (_inputReceiver.Jumping())
            {

            }

            int layerMask = 31;
            RaycastHit hit;

            if (_wallPosition == WallPosition.RIGHT)
            {
                if (!Physics.Raycast(_meshTransform.position, _meshTransform.right, out hit, 2.0f, layerMask))
                {
                    return typeof(FallCharacterState);
                }
            }
            else if (_wallPosition == WallPosition.LEFT)
            {
                if (!Physics.Raycast(_meshTransform.position, -_meshTransform.right, out hit, 2.0f, layerMask))
                {
                    return typeof(FallCharacterState);
                }
            }


            _localTimeAccumulator += Time.deltaTime;

            return GetType();
        }

    }

}
