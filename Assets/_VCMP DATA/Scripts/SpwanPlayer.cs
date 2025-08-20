using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class SpwanPlayer : MonoBehaviour
{
    public static SpwanPlayer Instance { get; private set; }
    public Button spwanBtn;
    public GameObject selectCharacterScreen;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EnableSpwanButton(bool isTrue)
    {
        selectCharacterScreen.gameObject.SetActive(isTrue);
    }

}
