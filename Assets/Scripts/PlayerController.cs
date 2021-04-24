using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(0f, 50f)] private float speed;
    
    private void Update()
    {
        Vector2 movementInputNormalized =
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 movementInput = new Vector3(movementInputNormalized.x, 0, movementInputNormalized.y);

        transform.position += movementInput / 100 * speed;
    }
}