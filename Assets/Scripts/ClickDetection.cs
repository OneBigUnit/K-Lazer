using UnityEngine;
using System.Collections;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] private float maxTimeToClick = 0.3f;
    [SerializeField] private float minTimeToClick = 0.05f;
    [SerializeField] private float longClickThreshold = 0.3f;

    private float minCurrentTime;
    private float maxCurrentTime;
    private float lastDoubleClickTime;

    public IEnumerator DoubleClickedAndHolding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastDoubleClickTime = 0f;
            if (Time.time >= minCurrentTime && Time.time <= maxCurrentTime)
            {
                minCurrentTime = 0;
                maxCurrentTime = 0;

                lastDoubleClickTime = Time.time;
            }
            minCurrentTime = Time.time + minTimeToClick;
            maxCurrentTime = Time.time + maxTimeToClick;
        }
        if (Input.GetMouseButton(0) && (Time.time - lastDoubleClickTime > longClickThreshold) && lastDoubleClickTime != 0)
        {
            Debug.Log("Double Clicked & Holding");
            yield return true;
        } else
        {
            yield return false;
        }
    }
}

public class CoroutineWithData
{
    public Coroutine coroutine { get; private set; }
    public object result;

    private IEnumerator target;

    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }
}
