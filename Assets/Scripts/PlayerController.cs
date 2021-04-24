using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(0f, 50f)] private float speed;
    [SerializeField] private GameObject sprite;
    
    private SpriteRenderer _spriteRenderer;

    private Vector3 movementInput;
    
    private void Start()
    {
        _spriteRenderer = sprite.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Get input and move
        Vector2 movementInputNormalized =
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        movementInput = new Vector3(movementInputNormalized.x, 0, movementInputNormalized.y);
        
        // Form collider for enemies and records and such
    }

    private void FixedUpdate()
    {
        transform.position += movementInput / 100 * speed;
    }
}