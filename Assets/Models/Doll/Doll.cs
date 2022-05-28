using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Doll : MonoBehaviour
{
    private enum State
    {
        Deactivated,
        Activated,
        Singing,
        Scaning
    }

    public AudioClip[] Sounds;

    [SerializeField] private AudioSource _sound;
    [SerializeField] private Animator _animator;

    private State _state;

    private void Start()
    {
        Invoke(nameof(StartGame), 3f);
    }

    public void StartGame()
    {
        _animator.SetTrigger("Start Game");
        StartCoroutine(nameof(GreenLight));
        _animator.ResetTrigger("Sing");
    }

    private bool IsPlaying()
    {
        return _sound.isPlaying;
    }

    private IEnumerator GreenLight()
    {
        _animator.SetTrigger("Sing");
        _sound.clip = Sounds[0];
        _sound.Play();
        yield return new WaitWhile(IsPlaying);
        StartCoroutine(nameof(RedLight));
    }

    private IEnumerator RedLight()
    {
        _animator.SetTrigger("Scan");
        _sound.clip = Sounds[1];
        _sound.Play();
        yield return new WaitWhile(IsPlaying);
        GameManager.Instance.KillLoosers();
        
        yield return new WaitWhile(GameManager.Instance.AnyShootersHasTarget);

        StartCoroutine(nameof(GreenLight));
    }

    private void Update()
    {
        if (_state == State.Scaning)
        {
            List<Participant> participants = FindObjectsOfType<Participant>().ToList();
            foreach (Participant participant in participants)
            {
                if (participant.TryGetComponent(out CharacterController controller))
                {
                    if (controller.velocity.magnitude > 0.1 && participant.IsPlaying)
                    {
                        GameManager.Instance.ExcludePlayer(participant);
                    }
                }
            }
        }
    }

    private void ChangeState(State state)
    {
        _state = state;
    }
}
