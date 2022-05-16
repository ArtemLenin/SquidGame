using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float Speed = 6f;
    public float TurnSmoothTime = 0.1f;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator _animator;

    private float _turnSmoothVelocity;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float movementSpeed = direction.magnitude;
        _animator.SetFloat("Speed", movementSpeed);
        if (movementSpeed >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir * Speed * Time.deltaTime);
        }
        else
        {
            _controller.Move(Vector3.zero);
        }
    }
}
