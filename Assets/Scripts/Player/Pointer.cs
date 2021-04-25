using UnityEngine;

public class Pointer : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}