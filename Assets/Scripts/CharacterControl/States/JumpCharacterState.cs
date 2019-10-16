using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public class JumpCharacterState : ConcreteMovingCharacterState
    {
        private readonly AnimationCurve _jumpingCurve;

        private float _localTimeAccumulator;

        private bool _firstIteration;


        public JumpCharacterState(
            AnimationCurve jumpingCurve,
            float micAccTime, float maxAccTime, ICharacterConcreteMoving concreteMove,
            Transform meshTransform, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(concreteMove, meshTransform, characterController, animator, tickables)
        {
            _jumpingCurve = jumpingCurve;
        }

        public override void Enter(BaseCharacterState baseCharacterState)
        {
            base.Enter(baseCharacterState);
            _localTimeAccumulator = 0.00f;
            _firstIteration = true;
            _animator.SetTrigger("Jumping");
        }

        public override void Exit(BaseCharacterState nextState)
        {
            base.Exit(nextState);
            _localTimeAccumulator = 0.00f;
        }

        public override Type Tick()
        {
            if (_firstIteration)
            {
                _firstIteration = false;
                return base.Tick();
            }

            if (_characterController.isGrounded || // ground touched
                _characterController.collisionFlags == CollisionFlags.Above || // ceiling hit with head
                _localTimeAccumulator >= _jumpingCurve.keys[1].time) // time to start falling down
            {
                return typeof(FallCharacterState);
            }

            _localTimeAccumulator += Time.deltaTime;

            return base.Tick();
        }

    }

}
