using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] [Range(0f, 50f)] private float walkSpeed;
    [SerializeField] [Range(0f, 100f)] private float throwSpeed;
    [SerializeField] private int totalRecords = 1;
    [SerializeField] private int catchFramesTotal = 5;
    [SerializeField] private float recordCoyoteSeconds = 0.1f;
    
    [Header("Dependencies")] 
    [SerializeField] private GameObject record;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float walkAnimSpeed = 0.5f;
    [SerializeField] private Sprite throwUp;
    [SerializeField] private Sprite throwDown;
    [SerializeField] private Sprite throwLeft;
    [SerializeField] private Sprite throwRight;
    [SerializeField] private GameObject hurtObject;
    [SerializeField] private GameObject throwRecordSound;
    [SerializeField] private GameObject catchRecordSound;
    [SerializeField] private GameObject dieSound;
    [SerializeField] private GameObject footstep;
    [SerializeField] private float sprintMultiplierTotal = 1.5f;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject dashParticles;
    
    [SerializeField] private Image recordStackImage;
    [SerializeField] private Sprite stack1;
    [SerializeField] private Sprite stack2;
    [SerializeField] private Sprite stack3;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    private Transform _cursor;

    private Vector3 _movementInput;
    private int _currentRecordCount;
    private int _currentCatchFrames;
    private GameObject _coyoteRecord;
    private Direction _currentDirection = Direction.Down;
    private bool _throwingAnim;
    private Conductor _conductor;
    private bool _initialized;
    private bool _dead;
    private float _currentSprintMultiplier = 1f;
    
    private void Start()
    {
        _cursor = GameObject.FindWithTag("Cursor").transform;

        _currentRecordCount = totalRecords;
    }

    private void Update()
    {
        // Get conductor for footsteps
        if (!_initialized)
        {
            _initialized = true;
            _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
            _conductor.beat.AddListener(PlayFootstep);
        }
        
        // Get input and move
        Vector2 movementInputNormalized =
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _movementInput = new Vector3(movementInputNormalized.x, 0, movementInputNormalized.y);

        // Record throwing
        if (Input.GetButtonDown("Fire1") && _currentRecordCount > 0)
        {
            Vector2 throwDir2d = (_cursor.position - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            Vector3 throwDir = new Vector3(throwDir2d.x, 0f, throwDir2d.y);
            GameObject recordInstance = Instantiate(
                record,
                transform.position + throwDir * 1f,
                Quaternion.identity);
            recordInstance.GetComponent<Rigidbody>().AddForce(throwDir * throwSpeed * 100);
            recordInstance.GetComponent<Record>().SetRecordTexture(_currentRecordCount);
            _currentRecordCount--;

            Instantiate(throwRecordSound);
            
            StartCoroutine(PlayThrowAnim());
        }

        if (_currentRecordCount == 0)
        {
            recordStackImage.enabled = false;
        }
        else if (_currentRecordCount == 1)
        {
            recordStackImage.enabled = true;
            recordStackImage.sprite = stack1;
        }
        else if (_currentRecordCount == 2)
        {
            recordStackImage.sprite = stack2;
        }
        else if (_currentRecordCount == 3)
        {
            recordStackImage.sprite = stack3;
        }

        if (Input.GetButtonDown("Fire2") && _currentCatchFrames <= 0 && _coyoteRecord == null)
        {
            _currentCatchFrames = catchFramesTotal;
        }
        else if (Input.GetButtonDown("Fire2") && _coyoteRecord != null)
        {
            Instantiate(catchRecordSound);
            Destroy(_coyoteRecord);
            _coyoteRecord = null;
            _currentRecordCount++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            KillPlayer();
        }

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(Sprint());
        }
        else
        {
            _currentSprintMultiplier -= 0.01f;
            if (_currentSprintMultiplier < 1)
            {
                _currentSprintMultiplier = 1.0f;
            }
        }

        // Update direction and animate
        if (!_throwingAnim)
        {
            float deadZone = 0.15f;
            bool switchedDir = false;

            if (movementInputNormalized.y > 1 - deadZone)
            {
                _currentDirection = Direction.Up;
                switchedDir = true;
            }
            else if (movementInputNormalized.y < deadZone - 1)
            {
                _currentDirection = Direction.Down;
                switchedDir = true;

            }
            else if (movementInputNormalized.x > 1 - deadZone)
            {
                _currentDirection = Direction.Right;
                switchedDir = true;
            }
            else if (movementInputNormalized.x < deadZone - 1)
            {
                _currentDirection = Direction.Left;
                switchedDir = true;
            }

            if (switchedDir)
            {
                string triggerName = "Down";
                switch (_currentDirection)
                {
                    case Direction.Up:
                        triggerName = "Up";
                        break;
                    case Direction.Down:
                        triggerName = "Down";
                        break;
                    case Direction.Left:
                        triggerName = "Left";
                        break;
                    case Direction.Right:
                        triggerName = "Right";
                        break;
                }
            
                animator.SetTrigger(triggerName);
            }

            animator.speed = movementInputNormalized == Vector2.zero ? 0 : walkAnimSpeed;
        }
    }

    private void FixedUpdate()
    {
        transform.position += _movementInput / 100 * walkSpeed * _currentSprintMultiplier;

        if (_currentCatchFrames > 0)
        {
            _currentCatchFrames--;
        }
        else
        {
            _currentCatchFrames = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Record") && other.gameObject.GetComponent<Record>().Bounces > 0)
        {
            if (_currentCatchFrames > 0 || other.gameObject.GetComponent<Record>().OnGround)
            {
                Instantiate(catchRecordSound);
                Destroy(other.gameObject);
                _currentRecordCount++;
            }
            else
            {
                StartCoroutine(CollideWithRecord(other));
            }
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            KillPlayer();
        }
    }

    private IEnumerator CollideWithRecord(Collider other)
    {
        _coyoteRecord = other.gameObject;
        yield return new WaitForSeconds(recordCoyoteSeconds);
        if (_coyoteRecord != null)
        {
            KillPlayer();
            Destroy(_coyoteRecord);
            _coyoteRecord = null;
        }
    }

    private IEnumerator PlayThrowAnim()
    {
        _throwingAnim = true;
        animator.enabled = false;

        animator.speed = 0;
        
        switch (_currentDirection)
        {
            case Direction.Up:
                spriteRenderer.sprite = throwUp;
                break;
            case Direction.Down:
                spriteRenderer.sprite = throwDown;
                break;
            case Direction.Left:
                spriteRenderer.sprite = throwLeft;
                break;
            case Direction.Right:
                spriteRenderer.sprite = throwRight;
                break;
        }
        
        yield return new WaitForSeconds(0.2f);

        animator.enabled = true;
        _throwingAnim = false;
    }

    private void KillPlayer()
    {
        if (!_dead)
        {
            _dead = true;
            Instantiate(dieSound);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("LivesTimeManager").GetComponent<LivesTimeManager>().LoseLife();
            GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>().ResetScene();
            Instantiate(hurtObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void PlayFootstep()
    {
        if (Mathf.Abs(_movementInput.x) > 0.1f || Mathf.Abs(_movementInput.z) > 0.1f)
        {
            GameObject footstepObj = Instantiate(footstep, transform.position, Quaternion.identity);
            footstepObj.GetComponent<AudioSource>().PlayOneShot(footstepObj.GetComponent<AudioSource>().clip, 2f);
        }
    }

    private IEnumerator Sprint()
    {
        Camera.main.GetComponent<ScreenShake>().Shake(0.05f, 0.5f);
        GameObject particleObj = Instantiate(dashParticles, transform.position + Vector3.back * 0.6f + Vector3.down * 0.7f, Quaternion.identity);
        particleObj.transform.parent = transform;
        
        for (int i = 0; i < 20; i++)
        {
            _currentSprintMultiplier += 0.2f;
            _currentSprintMultiplier = Mathf.Min(sprintMultiplierTotal, _currentSprintMultiplier);
            yield return new WaitForSeconds(0.0075f);
        }
    }
}