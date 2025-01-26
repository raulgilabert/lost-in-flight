using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
