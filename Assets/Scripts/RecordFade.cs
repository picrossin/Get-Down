using UnityEngine;

public class RecordFade : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    private Material _mat;

    private float alpha = 1f;
    
    private void Start()
    {
        _mat = meshRenderer.material;
    }

    private void FixedUpdate()
    {
        Color newColor = _mat.color;
        newColor.a -= 0.05f;
        _mat.SetColor("_BaseColor", newColor);
        
        if (newColor.a <= 0)
            Destroy(gameObject);
    }
}
