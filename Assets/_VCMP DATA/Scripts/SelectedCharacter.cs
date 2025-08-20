using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    [System.Serializable]
    public class wepons
    {
        public int characterId;
        public GameObject[] weapons;
    }
    public PlayerNetworkBehaviour player;

    [Space(10), Header("Characetr Selection")]
    public int selectedCharacterIndex = 0;
    public GameObject characterA;
    public GameObject characterB;

    [Space(10), Header("Weapon Selection")]
    public int weaponIndex = 0;
    public wepons[] weponsList;
    public void Start()
    {
        selectedCharacterIndex = CharacterSelection.Instance.characterId;
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

    private void Update()
    {
        WeaponType();
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

    public void WeaponType()
    {
        player.animator.SetFloat("Weapon Type", weaponIndex);
        for (int i = 0; i < weponsList.Length; i++)
        {
            if (weponsList[i].characterId == selectedCharacterIndex)
            {
                for (int j = 0; j < weponsList[i].weapons.Length; j++)
                {
                    weponsList[i].weapons[j].SetActive(j == weaponIndex);
                }
            }
        }
    }
}
