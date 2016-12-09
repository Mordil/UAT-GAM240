using L4.Unity.Common;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AIInputController : MonoBehaviour, IInputController
{
    private const float OFF_MESH_LINK_MOVEMENT_CLOSE_ENOUGH_CONSTANT = .04f;

    public Transform Target { get { return _targetTransform; } }

    [Header("Components")]
    [SerializeField]
    private Transform _myTransform;
    [SerializeField]
    private Transform _targetTransform;

    private Vector3 _desiredVelocity;
    private Coroutine _offMeshLinkCoroutine;

    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private Animator _animator;

    [Header("Spellcasting Settings")]

    [SerializeField]
    [Tooltip("Can the AI cast spells at a target?")]
    private bool _canCastSpells = true;
    private bool _isCastingASpell;

    [SerializeField]
    private float _maxDistance = 10f;
    [SerializeField]
    [Range(0, 5)]
    private float _spellcastingDelay;
    private float _spellcastingTimer;

    [SerializeField]
    private SpellcastingAgent _spellcastingAgent;

    private void Awake()
    {
        if (_navMeshAgent == null)
        {
            _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            _navMeshAgent.updateRotation = true;
        }

        if (_targetTransform == null)
        {
            _targetTransform = FindObjectOfType<PlayerInputController>().transform;
        }

        if (_myTransform == null)
        {
            _myTransform = transform;
        }

        if (_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }

        if (_canCastSpells &&
            _spellcastingAgent == null)
        {
            _spellcastingAgent = GetComponent<SpellcastingAgent>();
        }

        _spellcastingAgent.OnSpellCast.AddListener((spellName) => { _isCastingASpell = false; });

        _targetTransform.gameObject.GetComponent<Health>().OnKilled.AddListener(() => {
            _canCastSpells = false;
        });
        GetComponentInChildren<Health>().OnKilled.AddListener(() => { _navMeshAgent.enabled = false; });
        GameManager.Instance.CurrentScene.As<GameplayLevel>().OnLevelPaused.AddListener(() => { _animator.speed = 0; });
        GameManager.Instance.CurrentScene.As<GameplayLevel>().OnLevelPaused.AddListener(() => { _animator.speed = 1; });
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            this.enabled = false;
            return;
        }

        if (_canCastSpells && !_isCastingASpell &&
            _spellcastingAgent != null && _targetTransform != null)
        {
            if (_spellcastingTimer >= _spellcastingDelay)
            {
                _spellcastingTimer = 0;

                if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance <= _maxDistance)
                {
                    _isCastingASpell = true;
                    _animator.SetTrigger(AnimationParameters.Arissa.Triggers.Spellcasting.FIREBALL);
                }
            }
            else
            {
                _spellcastingTimer += Time.deltaTime;
            }
        }
    }

    private void OnAnimatorMove()
    {
        if (_navMeshAgent.updatePosition)
        {
            _navMeshAgent.velocity = _animator.velocity;
        }
        else
        {
            _animator.ApplyBuiltinRootMotion();
        }
    }

    private void LateUpdate()
    {
        if (_offMeshLinkCoroutine != null || !_navMeshAgent.enabled)
        {
            return;
        }

        _navMeshAgent.SetDestination(_targetTransform.position);

        if (_navMeshAgent.isOnOffMeshLink)
        {
            _offMeshLinkCoroutine = StartCoroutine(TraverseOffMeshLink());
            _animator.SetTrigger("Off Mesh Link Movement");
        }
        else
        {
            UpdateAnimations();
        }
    }

    private void UpdateAnimations()
    {
        _desiredVelocity = Vector3.MoveTowards(
            _desiredVelocity,
            _navMeshAgent.desiredVelocity,
            _navMeshAgent.acceleration * Time.deltaTime);

        Vector3 input = _desiredVelocity;
        input = _myTransform.InverseTransformDirection(input);

        _animator.SetFloat(AnimationParameters.Arissa.Floats.HORIZONTAL, input.x);
        _animator.SetFloat(AnimationParameters.Arissa.Floats.VERTICAL, input.z);
    }

    private IEnumerator TraverseOffMeshLink()
    {
        _navMeshAgent.Stop();
        _navMeshAgent.ActivateCurrentOffMeshLink(false);

        OffMeshLinkData linkData = _navMeshAgent.currentOffMeshLinkData;
        _navMeshAgent.updatePosition = false;

        int oldAvoidancePriority = _navMeshAgent.avoidancePriority;
        _navMeshAgent.avoidancePriority = 99;

        Vector3 offset;

        do
        {
            offset = linkData.startPos - _myTransform.position;

            // Stay Grounded
            RaycastHit hitInfo;

            if (Physics.Raycast(_myTransform.position + Vector3.up, Vector3.down, out hitInfo, 2f, 1, QueryTriggerInteraction.Ignore))
            {
                _myTransform.position = hitInfo.point;
            }

            // Line up with start node
            _myTransform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(offset, Vector3.up));

            // Move
            Vector3 input = _myTransform.InverseTransformDirection(offset.normalized * _navMeshAgent.speed);
            _animator.SetFloat(AnimationParameters.Arissa.Floats.HORIZONTAL, input.x);
            _animator.SetFloat(AnimationParameters.Arissa.Floats.VERTICAL, input.z);

            yield return null;
        }
        while (offset.sqrMagnitude > OFF_MESH_LINK_MOVEMENT_CLOSE_ENOUGH_CONSTANT);

        _myTransform.position = linkData.endPos;
        _navMeshAgent.avoidancePriority = oldAvoidancePriority;
        _navMeshAgent.updatePosition = true;
        _navMeshAgent.ActivateCurrentOffMeshLink(true);
        _navMeshAgent.CompleteOffMeshLink();
        _navMeshAgent.Resume();

        _offMeshLinkCoroutine = null;
    }
}
