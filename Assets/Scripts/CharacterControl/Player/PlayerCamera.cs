using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterControl.Player
{
    public class PlayerCamera : ITick
    {
        private Transform _hinge;
        private Transform _camera;
        private float _distance;
        private float _height;
        private float _sensitivity;
        private float _clampAngle;

        private float rotY;
        private float rotX;

        public PlayerCamera(Transform hinge, Transform camera, float distance, float height, float sensitivity, float clampAngle)
        {
            _hinge = hinge;
            _camera = camera;
            _distance = distance;
            _height = height;
            _sensitivity = sensitivity;
            _clampAngle = clampAngle;
        }

        public void Execute()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            RotateHinge(mouseX, mouseY);
            SetCameraPosition();
        }

        private void RotateHinge(float mouseX, float mouseY)
        {
            rotY += mouseX * _sensitivity;
            rotX += mouseY * _sensitivity;

            rotX = Mathf.Clamp(rotX, -_clampAngle, _clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            _hinge.transform.rotation = localRotation;

            _hinge.Rotate(Vector3.up * mouseX * _sensitivity);
        }

        private void SetCameraPosition()
        {
            _camera.position = _hinge.transform.position + Vector3.up * _height + (_hinge.forward * _distance * -1.0f);
        }
    }

}

