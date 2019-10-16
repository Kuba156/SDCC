using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class FallCharacterState : ConcreteMovingCharacterState
    {
        private readonly IInputReceiver _inputReceiver;

        public FallCharacterState(
            IInputReceiver inputReceiver,
            ICharacterConcreteMoving concreteMove,
            Transform playerMesh, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(concreteMove, playerMesh, characterController, animator, tickables)
        {
            _inputReceiver = inputReceiver;
        }

        public override void Enter(BaseCharacterState baseCharacterState)
        {
            base.Enter(baseCharacterState);
            _animator.SetBool("Falling", true);
        }


        public override void Exit(BaseCharacterState nextState)
        {
            base.Exit(nextState);
            _animator.SetBool("Falling", false);
        }


        public override Type Tick()
        {
            if (_characterController.isGrounded || _characterController.collisionFlags == CollisionFlags.Below)
            {
                if (_inputReceiver.GetHorizontalMovement() != Vector3.zero || _inputReceiver.GetVerticalMovement() != Vector3.zero)
                {
                    return typeof(WalkCharacterState);
                }
                if (_inputReceiver.Sprinting())
                {
                    return typeof(RunCharacterState);
                }

                return typeof(IdleCharacterState);
            }

            return base.Tick();
        }
    }

}
