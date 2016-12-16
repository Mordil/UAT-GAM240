using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerInputController))]
public class PlayerInputControllerEditor : Editor
{
    private class SerializedProperties
    {
        public readonly SerializedProperty MovementSpeed;
        public readonly SerializedProperty RotationSpeed;
        public readonly SerializedProperty CharacterManager;
        public readonly SerializedProperty SpellcastingAgent;
        public readonly SerializedProperty Animator;

        public SerializedProperties(SerializedObject obj)
        {
            MovementSpeed = obj.FindProperty("_movementSpeed");
            RotationSpeed = obj.FindProperty("_rotationSpeed");
            CharacterManager = obj.FindProperty("_characterManager");
            SpellcastingAgent = obj.FindProperty("_spellcastingAgent");
            Animator = obj.FindProperty("_animator");
        }
    }

    private bool _isMovementFoldoutOpen = false;
    private bool _isAgentFoldoutOpen = false;

    private SerializedProperties _properties;

    public override void OnInspectorGUI()
    {
        if (_properties == null)
        {
            _properties = new SerializedProperties(serializedObject);
        }

        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_properties.CharacterManager, new GUIContent("Character Manager"));
        EditorGUILayout.PropertyField(_properties.Animator, new GUIContent("Animator"));

        _isAgentFoldoutOpen = EditorGUILayout.Foldout(_isAgentFoldoutOpen, "Agents");
        if (_isAgentFoldoutOpen)
        {
            EditorGUILayout.PropertyField(_properties.SpellcastingAgent);
        }

        _isMovementFoldoutOpen = EditorGUILayout.Foldout(_isMovementFoldoutOpen, "Movement Settings");
        if (_isMovementFoldoutOpen)
        {
            EditorGUILayout.PropertyField(_properties.MovementSpeed);
            EditorGUILayout.PropertyField(_properties.RotationSpeed);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
