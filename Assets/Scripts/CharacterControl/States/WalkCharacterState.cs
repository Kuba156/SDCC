using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class WalkCharacterState : MovingCharacterState
    {
        private readonly AnimationCurve _walkingCurve;

        private readonly IInputReceiver _inputReceiver;


        public WalkCharacterState(
            AnimationCurve walkingCurve, IInputReceiver inputReceiver,
            float minAccTime, float maxAccTime, ICharacterMoving[] characterMovings,
            Transform playerMesh, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(minAccTime, maxAccTime, characterMovings, playerMesh, characterController, animator, tickables)
        {
            _walkingCurve = walkingCurve;
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
                // the movement vector disappears before the timeAccu can actually go down and IdleState can be introduced. Dont play anim after we're not moving.
                buildingAcc = false;

                return typeof(IdleCharacterState);
            }


            if (_inputReceiver.Sprinting())
            {
                buildingAcc = true;

                if (timeAccumulator >= _timeAccMax)
                {
                    return typeof(RunCharacterState);
                }
                
            }

            _animator.SetFloat("Velocity", timeAccumulator);

            return base.Tick();

        }

    }

}
