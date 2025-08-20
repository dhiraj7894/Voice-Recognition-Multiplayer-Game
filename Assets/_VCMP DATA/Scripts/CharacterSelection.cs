using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection Instance { get; private set; }
    public int characterId;
    private void Awake()
    {
        Instance = this;
    }

    public void SetCharacter(int Id)
    {
        characterId = Id;
    }
}
