using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl
{

    public abstract class BaseCharacterState
    {
        protected Transform _meshTransform;
        protected CharacterController _characterController;
        protected Animator _animator;
        protected ITick[] _tickables;

        public BaseCharacterState[] transitions { get; set; }

        public BaseCharacterState(Transform meshTransform, CharacterController characterController, Animator animator, ITick[] tickables)
        {
            _meshTransform = meshTransform;
            _characterController = characterController;
            _animator = animator;
            _tickables = tickables;
        }

        public void EarlyTick()
        {
            foreach (ITick tickable in _tickables)
            {
                tickable.Execute();
            }
        }

        public abstract Type Tick();

        public abstract void Enter(BaseCharacterState previousState);

        public abstract void Exit(BaseCharacterState nextState);

    }

}