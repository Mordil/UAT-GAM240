using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{
    private struct AnimationParameters
    {
        public const string JUMP = "Jump";
        public const string VERTICAL = "Vertical";
        public const string HORIZONTAL = "Horizontal";
    }

    [SerializeField]
    private float _movementSpeed = 4f;
    [SerializeField]
    private float _rotationSpeed = 25f;

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
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        input = Vector3.ClampMagnitude(input, 1f) * _movementSpeed;

        _animator.SetFloat(AnimationParameters.HORIZONTAL, input.x);
        _animator.SetFloat(AnimationParameters.VERTICAL, input.z);

        bool didJump = Input.GetKeyUp(KeyCode.Space);

        if (didJump && input.z >= 0 && input.x == 0)
        {
            _animator.SetTrigger(AnimationParameters.JUMP);
        }
    }

    private void UpdateRotation()
    {
        float angle = Input.GetAxis("Mouse X") * _rotationSpeed;
        // Maintain current "up" position, to rotate horizontally.
        Vector3 rotateVector = Vector3.up * angle;

        // apply the rotation
        transform.Rotate(rotateVector, Space.World);
    }
}
