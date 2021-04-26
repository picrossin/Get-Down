using System;
using UnityEngine;

public class Record : MonoBehaviour
{
    [SerializeField] private int totalBounce = 4;
    [SerializeField] private Texture2D record1;
    [SerializeField] private Texture2D record2;
    [SerializeField] private Texture2D record3;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject pingSound;
    
    private int _bounces = 0;
    public int Bounces => _bounces;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            _bounces++;
            Instantiate(pingSound);
        }
    }

    public void SetRecordTexture(int record)
    {
        Texture2D toSet = record1;
        if (record == 2)
        {
            toSet = record2;
        }
        else if (record == 3)
        {
            toSet = record3;
        }
        
        meshRenderer.material.SetTexture("_BaseMap", toSet);
        meshRenderer.material.SetTexture("_EmissionMap", toSet);
    }
}
