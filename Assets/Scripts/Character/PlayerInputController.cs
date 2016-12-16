using UnityEngine;

/// <summary>
/// Handles player input and carries commands to other components.
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 4f;
    [SerializeField]
    private float _rotationSpeed = 25f;

    [SerializeField]
    private CharacterManager _characterManager;
    [SerializeField]
    private SpellcastingAgent _spellcastingAgent;

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

        _animator.SetFloat(AnimationParameters.Arissa.Floats.HORIZONTAL, input.x);
        _animator.SetFloat(AnimationParameters.Arissa.Floats.VERTICAL, input.z);

        bool didJump = Input.GetKeyUp(KeyCode.Space);

        if (didJump && input.z >= 0 && input.x == 0)
        {
            _animator.SetTrigger(AnimationParameters.Arissa.Triggers.JUMP);
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
            _animator.SetTrigger(AnimationParameters.Arissa.Triggers.MeleeAttacks.NORMAL);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            _animator.SetTrigger(AnimationParameters.Arissa.Triggers.Spellcasting.FIREBALL);
        }
    }
}
