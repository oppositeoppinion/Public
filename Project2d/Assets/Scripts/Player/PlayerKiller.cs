using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerKiller : MonoBehaviour
{
    public static PlayerKiller Instance;
    [SerializeField] private Transform _player;
    [SerializeField] private CameraFollowPlayer _cameraFollowPlayer;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Image _faderImage;
    [SerializeField] private float _waitBeforeNextScene=5f;
    [Range(0.001f,2f)]
    [SerializeField] private float _faderRate ;
    private void Start()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else Instance = this;


        _cameraFollowPlayer = Camera.main.GetComponent<CameraFollowPlayer>();
        _boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _boxCollider.enabled = false;
            GameOver();
        }
    }
    private IEnumerator LoadTitle()
    {
        StartCoroutine(Fader());
        yield return new WaitForSeconds(_waitBeforeNextScene); 
        SceneManager.LoadScene("01_SceneTitle");
    }
    private IEnumerator Fader()
    {
        var color = _faderImage.color;
        color.a = 0;
        for (; _faderImage.color.a<255; )
        {
            yield return null;
            color.a += _faderRate*Time.deltaTime;
            _faderImage.color = color;
        }
    }
    public void GameOver()
    {
        _cameraFollowPlayer.enabled = false;
        StartCoroutine(LoadTitle());
    }
}
