using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void ExcludePlayer(Participant participant)
    {
        Soldier killer = GetRandomShooter();
        killer.KillList.Enqueue(participant);
    }

    private Soldier GetRandomShooter()
    {
        Soldier[] soldiers = FindObjectsOfType<Soldier>();
        int index = Random.Range(0, soldiers.Length);
        return soldiers[index];
    }

    public void KillLoosers()
    {
        Soldier[] soldiers = FindObjectsOfType<Soldier>();
        foreach (Soldier soldier in soldiers)
        {
            soldier.GetNewTarget();
            StartCoroutine(soldier.KillTarget());
        }
    }
}
