using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private Animator _animator;

    private void Start() 
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
		Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		input = Vector3.ClampMagnitude(input, 1f) * Time.deltaTime * _movementSpeed;

        _animator.SetFloat(AnimationLookup.Kach.Floats.HORIZONTAL, input.x);
        _animator.SetFloat(AnimationLookup.Kach.Floats.VERTICAL, input.z);
    }
}
