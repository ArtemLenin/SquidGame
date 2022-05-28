using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    Soldier[] soldiers;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start()
    {
        soldiers = FindObjectsOfType<Soldier>();
    }

    public void ExcludePlayer(Participant participant)
    {
        Soldier killer = GetRandomShooter();
        if (!killer.KillList.Contains(participant))
        {
            killer.KillList.Enqueue(participant);
            killer.TargetCount++;
            participant.IsPlaying = false;
        }
    }

    private Soldier GetRandomShooter()
    {
        Soldier[] soldiers = FindObjectsOfType<Soldier>();
        int index = Random.Range(0, soldiers.Length);
        return soldiers[index];
    }

    public bool AnyShootersHasTarget()
    {
        foreach (Soldier soldier in soldiers)
        {
            if (soldier.HasAnyTargets())
                return true;
        }
        return false;
    }

    public void KillLoosers()
    {
        foreach (Soldier soldier in soldiers)
        {
            StartCoroutine(soldier.KillAllTarget());
        }
    }
}
