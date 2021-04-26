using System;
using System.Collections;
using UnityEngine;

public class Record : MonoBehaviour
{
    [SerializeField] private int totalBounce = 4;
    [SerializeField] private Texture2D record1;
    [SerializeField] private Texture2D record2;
    [SerializeField] private Texture2D record3;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject pingSound;
    [SerializeField] private GameObject fallSound;
    [SerializeField] private float deadzone = 0.1f;
    [SerializeField] private int framesBetweenTrail = 3;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject bumpParticles;
    
    private int _bounces = 0;
    public int Bounces => _bounces;

    private bool _onGround;
    public bool OnGround => _onGround;

    private int _trailIndex;
    private bool _inDeadZone;

    private void FixedUpdate()
    {
        _trailIndex++;
        if (_trailIndex >= framesBetweenTrail && !_onGround)
        {
            _trailIndex = 0;
            Instantiate(trail, transform.position + Vector3.down * 0.1f, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3 && !_inDeadZone)
        {
            _bounces++;
            Instantiate(bumpParticles, transform.position, Quaternion.identity);
            if (_bounces >= totalBounce)
            {
                _onGround = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                Instantiate(fallSound);
                GetComponent<AudioSource>().Stop();
            }
            else
            {
                Instantiate(pingSound);
            }
            
            StartCoroutine(CountDeadzone());
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

    private IEnumerator CountDeadzone()
    {
        _inDeadZone = true;
        yield return new WaitForSeconds(deadzone);
        _inDeadZone = false;
    }
}
