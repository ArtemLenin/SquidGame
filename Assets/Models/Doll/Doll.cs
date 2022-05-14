using System.Collections;
using UnityEngine;

public enum State
{
    Deactivated,
    Singing,
    Scaning
}

public class Doll : MonoBehaviour
{
    private State _state;

    public AudioSource Sound;
    public Transform Head;
    public AudioClip[] Sounds;
    public Animator Animator;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Animator.SetTrigger("Start Game");
        StartCoroutine(nameof(GreenLight));
        Animator.ResetTrigger("Sing");
    }

    private bool s()
    {
        return Sound.isPlaying;
    }

    private IEnumerator GreenLight()
    {
        Animator.SetTrigger("Sing");
        Sound.clip = Sounds[0];
        Sound.Play();
        yield return new WaitWhile(s);
        StartCoroutine(nameof(RedLight));
    }

    private IEnumerator RedLight()
    {
        Animator.SetTrigger("Scan");
        Sound.clip = Sounds[1];
        Sound.Play();
        yield return new WaitWhile(s);
        StartCoroutine(nameof(GreenLight));
    }

    private void Update()
    {
        if (_state == State.Scaning)
        {
            Participant[] participants = FindObjectsOfType<Participant>();
            foreach (Participant participant in participants)
            {
                Rigidbody rb = participant.GetComponent<Rigidbody>();
                if (rb.velocity.magnitude > 0.1)
                {
                    Destroy(rb.gameObject);
                }
            }
        }
    }

    public void ChangeState(State state)
    {
        _state = state;
    }
}
