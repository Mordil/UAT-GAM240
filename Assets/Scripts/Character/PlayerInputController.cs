using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour
{
    private struct AnimationParameters
    {
        public const string JUMP_TRIGGER = "Jump";
        public const string VERTICAL = "Vertical";
        public const string HORIZONTAL = "Horizontal";
        public const string NORMAL_ATTACK_TRIGGER = "Normal Attack";
    }

    [SerializeField]
    private float _movementSpeed = 4f;
    [SerializeField]
    private float _rotationSpeed = 25f;

    [SerializeField]
    private CharacterManager _characterManager;

    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        if (_characterManager == null)
        {
            _characterManager = GetComponent<CharacterManager>();
        }
    }

    private void Update()
    {
        HandlePositionInput();
        HandleRotationInput();
        HandleActions();
    }

    private void HandlePositionInput()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        input = Vector3.ClampMagnitude(input, 1f) * _movementSpeed;

        _animator.SetFloat(AnimationParameters.HORIZONTAL, input.x);
        _animator.SetFloat(AnimationParameters.VERTICAL, input.z);

        bool didJump = Input.GetKeyUp(KeyCode.Space);

        if (didJump && input.z >= 0 && input.x == 0)
        {
            _animator.SetTrigger(AnimationParameters.JUMP_TRIGGER);
        }
    }

    private void HandleRotationInput()
    {
        float angle = Input.GetAxis("Mouse X") * _rotationSpeed;
        // Maintain current "up" position, to rotate horizontally.
        Vector3 rotateVector = Vector3.up * angle;

        // apply the rotation
        transform.Rotate(rotateVector, Space.World);
    }

    private void HandleActions()
    {
        if (Input.GetKeyUp(KeyCode.F) &&
            _characterManager.WeaponAgentComponent.HasWeaponEquipped)
        {
            _animator.SetTrigger(AnimationParameters.NORMAL_ATTACK_TRIGGER);
        }
    }
}
