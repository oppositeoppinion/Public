using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TestStuff : MonoBehaviour
{ }
/*{
    [SerializeField] int iterations;
    private Stopwatch stopwatch = new Stopwatch();
    private float testFloat = -0.03997511f;
    private void Start()
    {
       
    }
    [ContextMenu("1")]
    private void Test1()
    {
        stopwatch.Start();
        for (int i = 0; i < iterations; ++i)
        {
            if (Mathf.Abs(testFloat) > 2f) ;
        }
        stopwatch.Stop();
        var elapsedTime = stopwatch.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"first case {elapsedTime}");
        stopwatch.Reset();
    }
    [ContextMenu("2")]
    private void Test2()
    {
        stopwatch.Start();
        for (int i = 0; i < iterations; ++i)
        {
            if (testFloat>2f ||-testFloat<-2f);
        }
        stopwatch.Stop();
        var elapsedTime = stopwatch.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"second case {elapsedTime}");
        stopwatch.Reset();
    }
    [ContextMenu("3")]
    private void Test3()
    {
        stopwatch.Start();
        for (int i = 0; i < iterations; ++i)
        {
            if (testFloat > 2f) ;
            if (-testFloat < -2f) ;
        }
        stopwatch.Stop();
        var elapsedTime = stopwatch.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"third case {elapsedTime}");
        stopwatch.Reset();
    }
    [ContextMenu("zero")]
    private void TestZero()
    {
        stopwatch.Start();
        for (int i = 0; i < iterations; ++i)
        {
            
        }
        stopwatch.Stop();
        var elapsedTime = stopwatch.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"third case {elapsedTime}");
        stopwatch.Reset();
    }
}
*/