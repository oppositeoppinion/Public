using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    public UnityEvent ElevatorEvent = new UnityEvent();
    [SerializeField] private float _timeForMovement;
    [SerializeField] private Vector3 _veryHighPoiint;
    [SerializeField] private Ease _ease = Ease.Linear;
    [SerializeField] private float _timeBeforeEndSound=0;
    [SerializeField] private float _timeBeforeEndGame=0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        ElevatorEvent.Invoke();
        GoTween();
        }
    }
    private IEnumerator EndsoundDelay()
    {
        SceneSounds.Instance.StopAllSounds();
        yield return new WaitForSeconds(_timeBeforeEndSound);
        SceneSounds.Instance.PlayShortSounds(SceneSounds.Instance._gameCompleteMusic, 0f);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(_timeBeforeEndGame);
        GameOver();
    }
    [ContextMenu("try_Elevator")]
    private void GoTween()
    {
        transform.DOMove(transform.position + _veryHighPoiint , _timeForMovement).SetEase(_ease);
        StartCoroutine(EndsoundDelay());
        StartCoroutine(EndGame());
    }
    
    private void GameOver()
    {
        SceneManager.LoadScene("01_SceneTitle");
    }
}
