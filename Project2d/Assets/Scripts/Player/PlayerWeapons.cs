using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons 
{
    public float Damage { get; private set; }
    public float DelayBeforeAttack { get; private set; }
    public float DelayAfterAttack { get; private set; }

    public static PlayerWeapons Sword { get; private set; }  = new PlayerWeapons { DelayBeforeAttack=0.1f, DelayAfterAttack = 1f, Damage = 5f };
}
