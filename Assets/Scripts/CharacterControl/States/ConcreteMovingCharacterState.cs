using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{

    public abstract class ConcreteMovingCharacterState : DynamicCharacterState
    {
        protected readonly ICharacterConcreteMoving _characterConcreteMove;
        private readonly Transform _playerMesh;


        public ConcreteMovingCharacterState(
            ICharacterConcreteMoving characterConcreteMove, Transform playerMesh,
            CharacterController characterController, Animator animator, ITick[] tickables)
            : base(playerMesh, characterController, animator, tickables)
        {
            _characterConcreteMove = characterConcreteMove;
            _playerMesh = playerMesh;
            _characterController = characterController;
            _animator = animator;
            _tickables = tickables;
        }

        public override void Enter(BaseCharacterState baseCharacterState)
        {
            if (baseCharacterState is DynamicCharacterState)
            {
                timeAccumulator = ((DynamicCharacterState)baseCharacterState).timeAccumulator;
            }
            else
            {
                timeAccumulator = 0;
            }

        }

        public override void Exit(BaseCharacterState nextCharacterState)
        {
            _characterConcreteMove.Reset();
        }

        public override Type Tick()
        {
            Vector3 movement = SetUpMovement(_meshTransform.forward);
            ApplyMovement(movement);
            return GetType();
        }

        public virtual Vector3 SetUpMovement(Vector3 direction)
        {
            Vector3 movement = Vector3.zero;
            movement += _characterConcreteMove.CalculateConcreteMove(direction, timeAccumulator);
            return movement;
        }

    }

}