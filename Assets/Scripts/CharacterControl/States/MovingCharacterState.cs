using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{

    public abstract class MovingCharacterState : DynamicCharacterState
    {
        protected readonly float _timeAccMin;
        protected readonly float _timeAccMax;

        private readonly ICharacterMoving[] _characterMovings;

        public bool buildingAcc { get; set; }
        

        public MovingCharacterState(
            float timeAccMin, float timeAccMax, ICharacterMoving[] characterMovings,
            Transform playerMesh, CharacterController characterController, Animator animator, ITick[] tickables)
            : base(playerMesh, characterController, animator, tickables)
        {
            _timeAccMin = timeAccMin;
            _timeAccMax = timeAccMax;
            _characterController = characterController;
            _animator = animator;
            _characterMovings = characterMovings;
            _tickables = tickables;
        }

        public override void Enter(BaseCharacterState previousCharacterState)
        {
            if (previousCharacterState is DynamicCharacterState)
            {
                timeAccumulator = ((DynamicCharacterState)previousCharacterState).timeAccumulator;
            }

            buildingAcc = true;
        }

        public override void Exit(BaseCharacterState nextCharacterState)
        {
            
        }

        public override Type Tick()
        {
            Vector3 movement = SetUpMovement();
            ApplyMovement(movement);
            TimeAccBuilding();
            return GetType();
        }


        public Vector3 SetUpMovement()
        {
            Vector3 movement = Vector3.zero;

            foreach (ICharacterMoving characterMovable in _characterMovings)
            {
                movement += characterMovable.CalculateMovement(timeAccumulator);
            }

            return movement;
        }

        private void TimeAccBuilding()
        {
            if (buildingAcc)
            {
                timeAccumulator += Time.deltaTime;
                if (timeAccumulator > _timeAccMax)
                {
                    timeAccumulator = _timeAccMax;
                }

            }
            else
            {
                timeAccumulator -= Time.deltaTime;
                if (timeAccumulator < _timeAccMin)
                {
                    timeAccumulator = _timeAccMin;
                }
            }
        }

    }

}