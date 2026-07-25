using UnityEngine;

public enum TrashCategory { None, Organik, Anorganik, B3 , Key}

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string ItemName = "New Item";
    public Sprite icon;
    public string description;

    public TrashCategory category = TrashCategory.None;
}
