using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{
    public abstract class DynamicCharacterState : BaseCharacterState
    {
        public float timeAccumulator { get; protected set; }

        public DynamicCharacterState(
            Transform playerMesh, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(playerMesh, characterController, animator, tickables)
        {
            _characterController = characterController;
            _animator = animator;
            _tickables = tickables;
        }

        public override void Enter(BaseCharacterState previousCharacterState)
        {
            if (previousCharacterState is DynamicCharacterState)
            {
                timeAccumulator = ((DynamicCharacterState)previousCharacterState).timeAccumulator;
            }
        }

        public override void Exit(BaseCharacterState nextCharacterState)
        {

        }

        protected void ApplyMovement(Vector3 movement)
        {
            if (movement.y == 0)
            {
                _characterController.SimpleMove(movement);
            }
            else
            {
                _characterController.Move(movement * Time.deltaTime);
            }
        }

    }

}