using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    public int selectedCharacterIndex = 0;
    public PlayerNetworkBehaviour player;

    [Space(10)]
    public GameObject characterA;
    public GameObject characterB;
    public void Start()
    {
        switch (selectedCharacterIndex)
        {
            case 0:
                CharacterSelectedA();
                break;
            case 1:
                CharacterSelectedB();
                break;
        }

    }
    public void CharacterSelectedA()
    {
        characterA.SetActive(true);
        characterB.SetActive(false);

        player.animator = characterA.GetComponent<Animator>();
    }
    public void CharacterSelectedB()
    {
        characterA.SetActive(false);
        characterB.SetActive(true);

        player.animator = characterB.GetComponent<Animator>();
    }
}
