using UnityEngine;

[CreateAssetMenu(
    fileName = "NewDialogue",
    menuName = "Kedas Jagat/Dialogue/Cutscene Dialogue"
)]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueLine
{
    public ActorSO speaker;

    [TextArea(3, 8)]
    public string text;
}