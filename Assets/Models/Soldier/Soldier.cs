using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _shotSounds;

    [SerializeField] private Queue<Participant> _killList;
    [SerializeField] private Participant _currentTarget;
    public int TargetCount = 0;
    bool state = true;

    public Queue<Participant> KillList
    {
        get { return _killList; }
    }

    private void Start()
    {
        _killList = new Queue<Participant>();
        _animator.ResetTrigger("Shoot");
    }

    public bool ShootingDown()
    {
        //return _animator.GetCurrentAnimatorStateInfo(0).IsName("Firing Rifle");
        return state;
    }

    public bool HasAnyTargets()
    {
        return TargetCount > 0;
    }

    public IEnumerator KillAllTarget()
    {
        float seconds = Random.Range(0.3f, 1.5f);
        yield return new WaitForSeconds(seconds);
        while (HasAnyTargets() || _currentTarget)
        {
            StartCoroutine(nameof(KillTarget));
            yield return new WaitUntil(ShootingDown);
        }
    }

    private IEnumerator KillTarget()
    {
        state = false;
        if (!_currentTarget) SetNewTarget();
        _animator.SetBool("Aim", true);
        yield return new WaitForEndOfFrame();
        _animator.SetTrigger("Shoot");
        yield return new WaitWhile(HasAnyTargets);
        _animator.SetBool("Aim", false);
    }

    private void Shot()
    {
        Debug.Log($"Убил {_currentTarget.name}");
        _currentTarget.Dead();
        TargetCount--;
        SetNewTarget();
        int index = Random.Range(0, _shotSounds.Length);
        _audioSource.clip = _shotSounds[index];
        _audioSource.Play();
    }

    public void SetNewTarget()
    {
        if (KillList.Count > 0)
        {
            _currentTarget = KillList.Dequeue();
        }
        else _currentTarget = null;
    }

    public void ready()
    {
        state = true;
    }
}
