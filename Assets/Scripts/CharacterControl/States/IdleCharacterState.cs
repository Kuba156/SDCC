using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class IdleCharacterState : BaseCharacterState
    {
        private readonly IInputReceiver _inputReceiver;


        public IdleCharacterState(IInputReceiver inputReceiver,
            Transform playerMesh, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(playerMesh, characterController, animator, tickables)
        {
            _inputReceiver = inputReceiver;
        }


        public override void Enter(BaseCharacterState previousState)
        {
            _animator.SetFloat("Velocity", 0);
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

            if (_characterController.collisionFlags != CollisionFlags.CollidedSides)
            {
                if (_inputReceiver.GetHorizontalMovement() != Vector3.zero || _inputReceiver.GetVerticalMovement() != Vector3.zero)
                {
                    return typeof(WalkCharacterState);
                }
            }

            return GetType();
        }

    }
}

