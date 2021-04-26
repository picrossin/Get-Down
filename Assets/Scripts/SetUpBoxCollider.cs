using UnityEngine;

public class SetUpBoxCollider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float xSizeMultiply = 1f;
    [SerializeField] private float ySizeMultiply = 1f;

    private BoxCollider _mainCollider;

    private void Start()
    {
        _mainCollider = GetComponent<BoxCollider>();

        Bounds spriteBounds = spriteRenderer.bounds;
        _mainCollider.center = Vector3.zero;
        _mainCollider.size = new Vector3(spriteBounds.extents.x * 2 * xSizeMultiply, 5f,
            spriteBounds.extents.y * 2 * ySizeMultiply);
    }
}