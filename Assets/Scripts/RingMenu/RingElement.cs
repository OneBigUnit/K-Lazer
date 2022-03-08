using UnityEngine;

[CreateAssetMenu(fileName = "RingElement", menuName = "RingMenu/Element", order = 2)]
public class RingElement : ScriptableObject
{

    public string elementName;
    public Sprite icon;
    public Ring nextRing;

}
