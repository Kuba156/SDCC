using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraBehaviours
{
    public class CameraHinge : MonoBehaviour
    {
        [SerializeField]
        private Transform _hinge;

        [SerializeField]
        private Transform _cameraTransform;

        [SerializeField]
        private float _cameraDistance;

        [SerializeField]
        private float _cameraHeight;

        [SerializeField]
        private float _mouseSensitivity;

        [SerializeField]
        private float _clampAngle;

        private float rotY;
        private float rotX;


        void Start()
        {
            Vector3 rot = transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
        }

        void Update()
        {
            _hinge.transform.position = _hinge.transform.position;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            RotateHinge(mouseX, mouseY);
            SetCameraPosition();
        }


        private void RotateHinge(float mouseX, float mouseY)
        {
            rotY += mouseX * _mouseSensitivity;
            rotX += mouseY * _mouseSensitivity;

            rotX = Mathf.Clamp(rotX, -_clampAngle, _clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

            _hinge.Rotate(Vector3.up * mouseX * _mouseSensitivity);
        }

        private void SetCameraPosition()
        {
            _cameraTransform.position = _hinge.transform.position + Vector3.up * _cameraHeight + (_hinge.forward * _cameraDistance * -1.0f);
        }

    }
}
