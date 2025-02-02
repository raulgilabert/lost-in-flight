using UnityEngine;

namespace SFX
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SfxTrigger : MonoBehaviour
    {
        private AudioSource _audioSource;

        [SerializeField] private float maxPitchOffset;
        
        protected void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        protected void Play()
        {
            _audioSource.pitch = Random.Range(1f - maxPitchOffset, 1f + maxPitchOffset);
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}