using UnityEngine;

[CreateAssetMenu(
    fileName = "NewActor",
    menuName = "Kedas Jagat/Dialogue/Actor"
)]
public class ActorSO : ScriptableObject
{
    [Header("Character")]
    public string actorName;

    [Header("Portrait")]
    public Sprite portrait;
}