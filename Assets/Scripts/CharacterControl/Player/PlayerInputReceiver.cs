using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl.Player
{
    public class PlayerInputReceiver : IInputReceiver
    {
        private readonly string _horizontalInputName;
        private readonly string _verticalInputName;
        private readonly Transform _hingeTransform;
        private readonly KeyCode _jumpKey;
        private readonly KeyCode _sprintKey;


        public PlayerInputReceiver(string horizontalInputName, string verticalInputName, Transform hingeTransform, KeyCode jumpKey, KeyCode sprintKey)
        {
            _horizontalInputName = horizontalInputName;
            _verticalInputName = verticalInputName;
            _hingeTransform = hingeTransform;
            _jumpKey = jumpKey;
            _sprintKey = sprintKey;
        }

        public Vector3 GetHorizontalMovement()
        {
            float horizontalInput = Input.GetAxis(_horizontalInputName);
            return _hingeTransform.right * horizontalInput;
        }

        public Vector3 GetVerticalMovement()
        {
            float verticalInput = Input.GetAxis(_verticalInputName);
            return _hingeTransform.forward * verticalInput;
        }

        public bool Jumping()
        {
            if (Input.GetKeyDown(_jumpKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Sprinting()
        {
            if (Input.GetKey(_sprintKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}

