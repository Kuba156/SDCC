using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl.Player
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField]
        private CharacterInformation _characterInformation;

        [SerializeField]
        private CharacterController _characterController;

        [SerializeField]
        private Transform _rotationTarget;

        [SerializeField]
        private Transform _hingeTransform;

        [SerializeField]
        private string _horizontalAxisName;

        [SerializeField]
        private string _verticalAxisName;

        [SerializeField]
        private KeyCode _jumpKey;

        [SerializeField]
        private KeyCode _sprintKey;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Transform _cameraTransform;

        [SerializeField]
        private float _cameraDistance;

        [SerializeField]
        private float _cameraHeight;

        [SerializeField]
        private float _mouseSensitivity;

        [SerializeField]
        private float _cameraClampAngle;

        private Dictionary<Type, BaseCharacterState> _statesDictionary;
        private BaseCharacterState _currentState;

        

        private void Awake()
        {
            ICharacterConcreteMoving gravity = new CharacterGravity(_characterInformation.gravityCurve, _characterInformation.movementCurve);
            ICharacterConcreteMoving playerJump = new CharacterJump(_characterInformation.jumpingCurve, _characterInformation.movementCurve);

            PlayerCamera camera = new PlayerCamera(_hingeTransform, _cameraTransform, _cameraDistance, _cameraHeight, _mouseSensitivity, _cameraClampAngle);
            PlayerInputReceiver playerInput = new PlayerInputReceiver(_horizontalAxisName, _verticalAxisName, _hingeTransform, _jumpKey, _sprintKey);
            CharacterMovement playerMovement = new CharacterMovement(_characterController, _characterInformation.movementCurve, _rotationTarget, playerInput);
            CharacterWallrun wallrun = new CharacterWallrun(_characterInformation.wallrunHorizontalCurve, _characterInformation.wallrunVerticalCurve, _characterInformation.movementCurve);
           

            ITick[] tickables =
            {
                camera
            };

            ICharacterMoving[] charMovables1 =
            {
                playerMovement
            };



            _statesDictionary = new Dictionary<Type, BaseCharacterState>()
            {
                { typeof(IdleCharacterState), new IdleCharacterState(
                    playerInput,
                    _rotationTarget, _characterController, _animator, tickables) },

                { typeof(WalkCharacterState), new WalkCharacterState(
                    _characterInformation.movementCurve, playerInput,
                    _characterInformation.movementCurve.keys[0].time, _characterInformation.movementCurve.keys[1].time, charMovables1,
                    _rotationTarget, _characterController, _animator, tickables) },

                { typeof(RunCharacterState), new RunCharacterState(
                    _characterInformation.movementCurve, playerInput,
                    _characterInformation.movementCurve.keys[1].time, _characterInformation.movementCurve.keys[2].time, charMovables1,
                    _rotationTarget, _characterController, _animator, tickables) },

                { typeof(JumpCharacterState), new JumpCharacterState(
                    _characterInformation.jumpingCurve,
                    _characterInformation.movementCurve.keys[0].time, _characterInformation.movementCurve.keys[2].time, playerJump,
                    _rotationTarget, _characterController, _animator, tickables) },

                { typeof(FallCharacterState), new FallCharacterState(
                    playerInput,
                    gravity,
                    _rotationTarget, _characterController, _animator, tickables) },

                { typeof(WallrunCharacterState), new WallrunCharacterState(
                    playerInput, _characterInformation.wallrunHorizontalCurve,
                    wallrun,
                    _rotationTarget, _characterController, _animator, tickables) },
            };
        }

        void Start()
        {
            _currentState = _statesDictionary[typeof(IdleCharacterState)];
        }

        void Update()
        {
            Debug.Log(_currentState);
            try
            {
                _currentState.EarlyTick();
                Type nextStateType = _currentState.Tick();
                BaseCharacterState nextState = _statesDictionary[nextStateType];
                
                if (nextState != _currentState)
                {
                    ToState(nextState);
                }
            }
            catch(NullReferenceException)
            {
                Debug.LogError("Next type for character state machine is null.");
            }

        }


        private void ToState(BaseCharacterState nextState)
        {
            BaseCharacterState currentStateTemp = _currentState;
            _currentState.Exit(nextState);
            _currentState = nextState;
            _currentState.Enter(currentStateTemp);
        }

    }

}
