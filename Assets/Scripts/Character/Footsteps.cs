using UnityEngine;

public class Footsteps : MonoBehaviour, ILocomotionAnimationHandler
{
    [SerializeField]
    private AudioSource _ambientAudioSource;

    [SerializeField]
    private AudioClip[] _footstepAudioClips;

    /// <summary>
    /// Plays a random footstep audio clip.
    /// </summary>
    /// <param name="groundMaterialName">The name of the ground material.</param>
    /// <example>"dirt" for earthy sounds.</example>
    /// <seealso cref="ILocomotionAnimationHandler.OnFootstep(string)"/>
    public void OnFootstep(string groundMaterialName)
    {
        if (_footstepAudioClips.Length > 0)
        {
            var index = Random.Range(0, _footstepAudioClips.Length);
            _ambientAudioSource.PlayOneShot(_footstepAudioClips[index]);
        }
    }
}
