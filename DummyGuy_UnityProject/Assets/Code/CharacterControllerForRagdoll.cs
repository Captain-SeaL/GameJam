using System;
using UnityEngine;

namespace Code
{
	public class CharacterControllerForRagdoll : MonoBehaviour
    {
        [SerializeField] float _walkSpeed = 4f;
        [SerializeField] float _runSpeed = 8f;
        [SerializeField] float _lerpVelocitySpeed = 10f;
        [SerializeField] CharacterController _characterController;
        
        float _speed;
        float _targetRotation;
        public float RotationSmoothTime = 0.12f;
        float _rotationVelocity;
        float _targetSpeed;

        void Awake()
        {
            _targetSpeed = _walkSpeed;
        }

        void Update()
		{
			var inputMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            var InputMagnitude = inputMove.magnitude;
            
            var targetSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
            _targetSpeed = Mathf.Lerp(_targetSpeed, targetSpeed, Time.deltaTime * _lerpVelocitySpeed);

            if (inputMove == Vector2.zero) _targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

            float speedOffset = 0.1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < _targetSpeed - speedOffset ||
                currentHorizontalSpeed > _targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed * InputMagnitude,
                    Time.deltaTime * 10f);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = _targetSpeed;
            }

            Vector3 inputDirection = new Vector3(inputMove.x, 0f, inputMove.y).normalized;

            if (inputMove != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  Camera.main.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            var move = new Vector3(0.0f, Physics.gravity.y, 0.0f) * Time.deltaTime;

            move += targetDirection.normalized * (_speed * Time.deltaTime);
            _characterController.Move(move);
		}
    }
}