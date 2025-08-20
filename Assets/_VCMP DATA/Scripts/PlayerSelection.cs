using Fusion;
using UnityEngine;

public class PlayerSelection : NetworkBehaviour
{
    [Networked] public int CharacterId { get; set; }
    [Networked] public int WeaponId { get; set; }

    public GameObject characterA;
    public GameObject characterB;
    public GameObject[] weaponsA;
    public GameObject[] weaponsB;

    public override void Spawned()
    {
        // Apply correct model + weapon when player spawns
        ApplySelection();
    }

    public void SetSelection(int charId, int weaponId)
    {
        if (Object.HasInputAuthority) // only local player can set their choice
        {
            RPC_SetSelection(charId, weaponId);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SetSelection(int charId, int weaponId)
    {
        CharacterId = charId;
        WeaponId = weaponId;

        // Apply immediately on the server
        ApplySelection();
    }

    private void ApplySelection()
    {
        // Hide all first
        characterA.SetActive(false);
        characterB.SetActive(false);

        foreach (var w in weaponsA) w.SetActive(false);
        foreach (var w in weaponsB) w.SetActive(false);

        // Show correct character
        if (CharacterId == 0)
        {
            characterA.SetActive(true);
            if (WeaponId < weaponsA.Length)
                weaponsA[WeaponId].SetActive(true);
        }
        else if (CharacterId == 1)
        {
            characterB.SetActive(true);
            if (WeaponId < weaponsB.Length)
                weaponsB[WeaponId].SetActive(true);
        }
    }
}
