using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class RunCharacterState : MovingCharacterState
    {
        private readonly AnimationCurve _sprintingCurve;

        private readonly IInputReceiver _inputReceiver;


        public RunCharacterState(
            AnimationCurve sprintingCurve, IInputReceiver inputReceiver,
            float minAccTime, float maxAccTime, ICharacterMoving[] characterMovings,
            Transform playerMesh, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(minAccTime, maxAccTime, characterMovings, playerMesh, characterController, animator, tickables)
        {
            _sprintingCurve = sprintingCurve;
            _inputReceiver = inputReceiver;
        }


        public override void Enter(BaseCharacterState baseCharacterState)
        {
            base.Enter(baseCharacterState);
        }

        public override void Exit(BaseCharacterState nextState)
        {

        }

        public override Type Tick()
        {
            int layerMask = 31;
            RaycastHit hit;
            if (Physics.Raycast(_meshTransform.position, _meshTransform.right, out hit, 2.0f, layerMask))
            {
                if (WallrunCheck(hit) && _inputReceiver.Jumping())
                {
                    return typeof(WallrunCharacterState);
                }
            }

            if (Physics.Raycast(_meshTransform.position, -_meshTransform.right, out hit, 2.0f, layerMask))
            {
                if (WallrunCheck(hit) && _inputReceiver.Jumping())
                {
                    return typeof(WallrunCharacterState);
                }
            }

            if (!_characterController.isGrounded)
            {
                return typeof(FallCharacterState);
            }

            if (_inputReceiver.Jumping())
            {
                return typeof(JumpCharacterState);
            }

            if (_inputReceiver.GetHorizontalMovement() == Vector3.zero && _inputReceiver.GetVerticalMovement() == Vector3.zero)
            {
                return typeof(IdleCharacterState);
            }

            if (!_inputReceiver.Sprinting())
            {
                buildingAcc = false;

                if (timeAccumulator <= _timeAccMin)
                {
                    return typeof(WalkCharacterState);
                }
                
            }
            else
            {
                buildingAcc = true;
            }

            _animator.SetFloat("Velocity", timeAccumulator);

            return base.Tick();

        }

        private bool WallrunCheck(RaycastHit hit)
        {
            float attackAngleValue = Vector3.Dot(_meshTransform.forward, hit.normal);

            if (Mathf.Abs(attackAngleValue) < 0.8)
            {
                return true;
            }

            return false;
        }

    }

    

}
