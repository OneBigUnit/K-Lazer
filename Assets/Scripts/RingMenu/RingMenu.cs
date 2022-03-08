using System;
using UnityEngine;


public class RingMenu : MonoBehaviour
{
    public Ring data;
    public RingCakePiece ringCakePiecePrefab;
    public float gapWidthDegree = 1f;
    public Action<string> callback;
    protected RingCakePiece[] pieces;
    protected RingMenu parent;
    public string path;

    private void Start()
    {
        var stepLength = 360f / data.elements.Length;
        var iconDist = Vector3.Distance(ringCakePiecePrefab.icon.transform.position, ringCakePiecePrefab.cakePiece.transform.position);

        // Position it
        pieces = new RingCakePiece[data.elements.Length];

        for (int i = 0; i < data.elements.Length; i++)
        {
            pieces[i] = Instantiate(ringCakePiecePrefab, transform);
            // Set root element
            pieces[i].transform.localPosition = Vector3.zero;
            pieces[i].transform.localRotation = Quaternion.identity;

            // Set cake piece
            pieces[i].cakePiece.fillAmount = 1f / data.elements.Length - gapWidthDegree / 360f;
            pieces[i].cakePiece.transform.localPosition = Vector3.zero;
            pieces[i].cakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + gapWidthDegree / 2f + i * stepLength);
            pieces[i].cakePiece.color = new Color(1f, 1f, 1f, 0.5f);

            // Set icon
            pieces[i].icon.transform.localPosition = pieces[i].cakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            pieces[i].icon.sprite = data.elements[i].icon;

        }
    }

    private void Update()
    {
        var stepLength = 360f / data.elements.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);
        for (int i = 0; i < data.elements.Length; i++)
        {
            if (i == activeElement)
                pieces[i].cakePiece.color = new Color(1f, 1f, 1f, 0.75f);
            else
                pieces[i].cakePiece.color = new Color(1f, 1f, 1f, 0.5f);
        }


        if (Input.GetMouseButtonDown(0))
        {
            string newPath = path + "/" + data.elements[activeElement].elementName;
            if (data.elements[activeElement].nextRing != null)
            {
                var newSubRing = Instantiate(gameObject, transform.parent).GetComponent<RingMenu>();
                newSubRing.parent = this;
                for (var j = 0; j < newSubRing.transform.childCount; j++)
                    Destroy(newSubRing.transform.GetChild(j).gameObject);
                newSubRing.data = data.elements[activeElement].nextRing;
                newSubRing.path = newPath;
                newSubRing.callback = callback;
            }
            else
            {
                callback?.Invoke(newPath);
            }
            gameObject.SetActive(false);
        }
    }

    private float NormalizeAngle(float a)
    {
        return (a + 360f) % 360f;
    }
}
