using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    [field: SerializeField] public float TimeScale { get; private set;}
    [SerializeField] private int _timeCounter;
    [SerializeField] private float _timeTrigger;
    [SerializeField] private int _fixedUpdateCounter;
    void Update()
    {
        _timeTrigger += TimeScale;

        if (_timeTrigger >= 1f) 
        {
            
            SendToAll();
            _timeTrigger -= 1f;

            _fixedUpdateCounter = 0;
        }

        _fixedUpdateCounter++;
    }
    public void SendToAll()
    {
        //
    }
}
