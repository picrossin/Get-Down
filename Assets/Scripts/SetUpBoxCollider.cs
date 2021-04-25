using UnityEngine;

public class SetUpBoxCollider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private BoxCollider _mainCollider;
    
    private void Start()
    {
        _mainCollider = GetComponent<BoxCollider>();
        
        Bounds spriteBounds = spriteRenderer.bounds;
        _mainCollider.center = Vector3.zero;
        _mainCollider.size = new Vector3(spriteBounds.extents.x * 2, 5f, spriteBounds.extents.y * 2);
    }
}
