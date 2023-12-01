using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    

    [SerializeField] private PlayerAnimController _playerAnimController;  
    [SerializeField] private PlayerAttack _playerAttackScript;  
    [SerializeField] private Rigidbody2D _playerRigidBody;  
    [SerializeField] private Transform _leftDetector;  //detectors position holders for _isonground
    [SerializeField] private Transform _righDetector;  
    [SerializeField] private float _normalMoveSpeed = 5f;
    [SerializeField] private float _gravityScale = 3f;
    [SerializeField] private float _jumpPower=12f;
    [SerializeField] private float _ongroundDetectorLength=0.01f;
    [SerializeField] private float _inAirDetectorLength=0.05f;
    [SerializeField] private float _maximumBuggedTimeInAir=0.29f;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashtime; 
    [SerializeField] private bool _debugJumpAllowed;

    private float _horizontalInput;
    private float _dashingDirection; //+1 or -1
    private bool _dashing;
    private bool _isJumpuble;
    private bool _isJumping;
    private bool _jumpedRecentelly;
    private bool _isOnGround;
    private bool _facingRight;
    private byte _standingGroundsCounter;

    //DBG section

    private void Start()
    {
        _playerRigidBody.gravityScale = _gravityScale;
    }
    private void Update()
    {
        #region InputSection

        _horizontalInput = 0f;
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButton("Fire1"))
        {
            _playerAttackScript.Attack(_facingRight);
        }
        if (Input.GetKey(KeyCode.D))
        {
           _horizontalInput+=1f;
            _facingRight = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _horizontalInput-= 1f;
            _facingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpuble = IsJumpAvaliable();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_horizontalInput != 0 && !_isJumping) Dashing(_horizontalInput); 
        }
        #endregion

    }

    private void FixedUpdate()
    {
        if (_isOnGround)
        {
        float horizontalSpeed = _horizontalInput * _normalMoveSpeed;
            if (_dashing) 
            {
                
                _playerRigidBody.velocity = new Vector2(_dashSpeed * _dashingDirection, _playerRigidBody.velocity.y);
            } 
            else _playerRigidBody.velocity = new Vector2(horizontalSpeed, _playerRigidBody.velocity.y);

        }
        

        if (_isJumpuble)
        {
            SceneSounds.Instance.PlayShortSounds(SceneSounds.Instance._jumpSound,0f);
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _jumpPower);
            _isJumpuble = false;
            _isJumping = true;
            _jumpedRecentelly = true;
            _playerAnimController.IsplayerJumping(true) ;
            StartCoroutine(JumpedRecentellyReset());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _standingGroundsCounter++;
        _isOnGround = true;
        _debugJumpAllowed = true;
        _isJumping = false;
        _playerAnimController.IsplayerJumping(false);
        //Debug.Log("onground "+ collision.gameObject.name);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _standingGroundsCounter--;
        if(_standingGroundsCounter<1)
        {
            _isOnGround = false;
            StartCoroutine(DebugJumpAllowedReset());
            //StartCoroutine(TempTestTimeInAir());  //dbg... maxtime = 0.25
        }
    }
    private IEnumerator JumpedRecentellyReset()
    {
        
        float jumptime = Time.time;
        while (Time.time-jumptime<_maximumBuggedTimeInAir)
        {
            yield return null;
        }
        _jumpedRecentelly = false;
    }
    private IEnumerator DebugJumpAllowedReset()
    {
        float starttime = Time.time;
        while (Time.time - starttime < _maximumBuggedTimeInAir)
        {
            yield return null;
        }
        if(!_isOnGround) _debugJumpAllowed = false;
    }
    private IEnumerator DashInProgress()
    {
        yield return new WaitForSeconds(_dashtime);
        _dashing = false;
        _playerAnimController.DashingEnd();
        _dashingDirection = 0;
    }
    #region DEBUG
    private IEnumerator DebugTestTimeInAir()
    {
        float startime = Time.time;
        while (_isOnGround == false)
        {
            yield return null;
        }
        Debug.Log(Time.time - startime);
    }

    [ContextMenu("DebugPlayerToStartingPoint")]
    private void DebugResetPosition()
    {
        transform.position = new Vector2(4f, 1f);
        _playerRigidBody.velocity = Vector2.zero;
    }
    #endregion
    public void Die()
    {
        PlayerKiller.Instance.GameOver();
    }
    public static bool NearObjectCheck(Vector2 startPosition, Vector2 direction, float detectionLength, RigidType expectedType)
    {
        var endPosition = startPosition + direction.normalized * detectionLength;
        RaycastHit2D raycastHit = Physics2D.Linecast(startPosition, endPosition);
        Debug.DrawLine(startPosition, endPosition, color: Color.cyan, 4f);
        //Debug.Log(raycastHit.collider.gameObject.name); //dbg
        if (raycastHit.collider is not null && raycastHit.collider.gameObject.TryGetComponent(out RigidIdentifier register))
        {
            if (register.Type == expectedType) return true;
        }
        return false;
    }

    private bool IsJumpAvaliable()
    {

        if (NearObjectCheck(_leftDetector.position, Vector2.down, _ongroundDetectorLength, RigidType.Ground)) return true;
        //if (LeftJumpDetectorCheck(_ongroundDetectorLength)) return true;

        if (NearObjectCheck(_righDetector.position, Vector2.down, _ongroundDetectorLength, RigidType.Ground)) return true;
        //if (RightJumpDetectorCheck(_ongroundDetectorLength)) return true;

        if (!_jumpedRecentelly && _debugJumpAllowed) 
        {
            if (NearObjectCheck(_leftDetector.position, Vector2.down, _inAirDetectorLength, RigidType.Ground)) return true;
            if (NearObjectCheck(_righDetector.position, Vector2.down, _inAirDetectorLength, RigidType.Ground)) return true;
            else return false;
        }

        else return false;
    }
    private void Dashing(float direction)
    {
        _dashing = true;
        _dashingDirection = direction;
        _playerAnimController.DashingStart();
        SceneSounds.Instance.PlayShortSounds(SceneSounds.Instance._dushSound, 0f);
        StartCoroutine(DashInProgress());
    }
    
    

    
}
