using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Dead()
    {
        _animator.SetTrigger("Death");
        if (TryGetComponent(out ThirdPersonController TPC))
        {
            TPC.enabled = false;
        }
        Destroy(this);
    }
}
