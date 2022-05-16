using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _shotSounds;

    private Queue<Participant> _killList;
    private Participant _currentTarget;

    public Queue<Participant> KillList
    {
        get { return _killList; }
    }

    private void Start()
    {
        _killList = new Queue<Participant>();
        _animator.ResetTrigger("Shoot");
    }

    public IEnumerator KillTarget()
    {
        if (_currentTarget)
        {
            _animator.SetBool("Aim", true);
            yield return new WaitForEndOfFrame();
            _animator.SetTrigger("Shoot");
            _animator.SetBool("Aim", false);
        }
    }

    private void Shot()
    {
        _currentTarget.Dead();
        int index = Random.Range(0, _shotSounds.Length);
        _audioSource.clip = _shotSounds[index];
        _audioSource.Play();
        GetNewTarget();
    }

    public void GetNewTarget()
    {
        if (_killList.Count > 0)
        {
            _currentTarget = _killList.Dequeue();
        }
    }
}
