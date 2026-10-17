using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public float weight;
    public Texture2D icon;
    public string description;
}
