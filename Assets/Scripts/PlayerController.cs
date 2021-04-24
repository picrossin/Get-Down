using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] [Range(0f, 50f)] private float walkSpeed;
    [SerializeField] [Range(0f, 100f)] private float throwSpeed;

    [Header("Dependencies")] 
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject record;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider _mainCollider;
    private Transform _cursor;

    private Vector3 movementInput;

    private void Start()
    {
        _spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        _mainCollider = GetComponent<BoxCollider>();
        _cursor = GameObject.FindWithTag("Cursor").transform;

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
        movementInput = new Vector3(movementInputNormalized.x, 0, movementInputNormalized.y);

        // Record throwing
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 throwDir2d = (_cursor.position - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            Vector3 throwDir = new Vector3(throwDir2d.x, 0f, throwDir2d.y);
            GameObject recordInstance = Instantiate(
                record,
                transform.position + throwDir * 1.5f,
                Quaternion.identity);
            recordInstance.GetComponent<Rigidbody>().AddForce(throwDir * throwSpeed * 100);
        }
    }

    private void FixedUpdate()
    {
        transform.position += movementInput / 100 * walkSpeed;
    }
}