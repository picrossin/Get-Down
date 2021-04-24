using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] [Range(0f, 50f)] private float walkSpeed;
    [SerializeField] [Range(0f, 100f)] private float throwSpeed;
    [SerializeField] private int totalRecords = 1;
    [SerializeField] private int catchFramesTotal = 5;
    [SerializeField] private float recordCoyoteSeconds = 0.1f;
    
    [Header("Dependencies")] 
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject record;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider _mainCollider;
    private Transform _cursor;

    private Vector3 _movementInput;
    private int _currentRecordCount;
    private int _currentCatchFrames;
    private GameObject _coyoteRecord;
    
    private void Start()
    {
        _spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        _mainCollider = GetComponent<BoxCollider>();
        _cursor = GameObject.FindWithTag("Cursor").transform;

        _currentRecordCount = totalRecords;

        // Form collider for enemies and records and such
        Bounds spriteBounds = _spriteRenderer.bounds;
        _mainCollider.center = spriteBounds.center;
        _mainCollider.size = new Vector3(spriteBounds.extents.x * 2, 5f, spriteBounds.extents.y * 2);
    }

    private void Update()
    {
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
                transform.position + throwDir * 1.5f,
                Quaternion.identity);
            recordInstance.GetComponent<Rigidbody>().AddForce(throwDir * throwSpeed * 100);
            _currentRecordCount--;
        }

        if (Input.GetButtonDown("Fire2") && _currentCatchFrames <= 0)
        {
            _currentCatchFrames = catchFramesTotal;
        }
        else if (Input.GetButtonDown("Fire2") && _coyoteRecord != null)
        {
            Destroy(_coyoteRecord);
            _currentRecordCount++;
        }
    }

    private void FixedUpdate()
    {
        transform.position += _movementInput / 100 * walkSpeed;

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
        if (other.CompareTag("Record"))
        {
            if (_currentCatchFrames > 0)
            {
                Destroy(other.gameObject);
                _currentRecordCount++;
            }
            else
            {
                StartCoroutine(CollideWithRecord(other));
            }
        }
    }

    private IEnumerator CollideWithRecord(Collider other)
    {
        if (_currentCatchFrames > 0)
        {
            Destroy(other.gameObject);
            _currentRecordCount++;
        }
        else
        {
            _coyoteRecord = other.gameObject;
            yield return new WaitForSeconds(recordCoyoteSeconds);
            _coyoteRecord = null;
        }
    }
}