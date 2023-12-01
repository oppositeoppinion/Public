using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RigidType
{
    Ground,
    Enemiy,
    Collectable,
    Water,
    Player, 
    Reserv2
}
public class RigidIdentifier : MonoBehaviour
{
    [field: SerializeField] public RigidType Type { get; private set; }
}
