using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _isPlaying = true;

    public bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }

    public void Dead()
    {
        _animator.SetTrigger("Death");
        if (TryGetComponent(out ThirdPersonController TPC))
        {
            TPC.enabled = false;
        }
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}
