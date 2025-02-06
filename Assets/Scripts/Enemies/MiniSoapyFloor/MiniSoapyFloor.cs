using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.MiniSoapyFloor
{
    public class MiniSoapyFloor : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnDamageDealt()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.Play();
        }
    }
}
