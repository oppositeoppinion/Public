using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneSounds : MonoBehaviour
{
    public static SceneSounds Instance;
    [field: SerializeField] public AudioClip _swingSound {   get; private set; }
    [field: SerializeField] public AudioClip _jumpSound {   get; private set; }
    [field: SerializeField] public AudioClip _struckSound {   get; private set; }
    [field: SerializeField] public AudioClip _dushSound {   get; private set; }
    [field: SerializeField] public AudioClip _gameCompleteMusic {   get; private set; }
    [field: SerializeField] public AudioClip _AnimalDieSound {   get; private set; }

    [SerializeField] private AudioSource _audioSourceForShorts;
    [SerializeField] private AudioSource _audioSourceForBackground;
    [SerializeField] private AudioClip _onstartAudio;
    [SerializeField] private float _delayBeforeBGM;
    [SerializeField] private float _BGMVolumeMax;
    [Range(0.0002f,0.001f)]
    [SerializeField] private float _BGMVolumeIncreaseValue;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        else Instance = this;
        PlayShortSounds(_onstartAudio,0.5f);
        StartCoroutine(BGM());
    }

    
    public void PlayShortSounds(AudioClip clip, float delay)
    {
        StartCoroutine(OneShotDelay(clip, delay));
    }
    public void StopAllSounds()
    {
        _audioSourceForShorts.Stop();
        _audioSourceForBackground.Stop();
    }
    private IEnumerator OneShotDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        _audioSourceForShorts.PlayOneShot(clip);
    }
    private IEnumerator BGM()
    {
        yield return new WaitForSeconds(_delayBeforeBGM);
        _audioSourceForBackground.volume = 0f;
        _audioSourceForBackground.Play();
        StartCoroutine(SoundIncrease());
    }
    private IEnumerator SoundIncrease()
    {
        
        for (; _audioSourceForBackground.volume < _BGMVolumeMax;)
        {
            _audioSourceForBackground.volume += _BGMVolumeIncreaseValue;
            yield return null;
        }
    }
    
   
}
