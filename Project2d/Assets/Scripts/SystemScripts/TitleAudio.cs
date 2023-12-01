using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _secondsWithoutSounds;
    [Range(0f,1f)]
    [SerializeField] private float _volumeMax;
    [Range(0.0002f, 0.001f)]
    [SerializeField] private float _volumeIncreaseValue= 0.0002f;   
    void Start()
    {
        _audioSource.volume = 0f;
        StartCoroutine(WithoutSounds());
    }

    // Update is called once per frame
    private IEnumerator WithoutSounds()
    {
        yield return new WaitForSeconds(_secondsWithoutSounds);
        StartCoroutine(SoundIncrease());
    }

    private IEnumerator SoundIncrease()
    {

        for (; _audioSource.volume < _volumeMax;)
        {
            _audioSource.volume += _volumeIncreaseValue;
            yield return null;
        }
    }
}
