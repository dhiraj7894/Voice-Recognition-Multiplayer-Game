using Fusion;
using UnityEngine;

public class WeaponSwitcher : NetworkBehaviour
{
    [Networked] public int SelectedWeaponIndex { get; set; }

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private PlayerNetworkBehaviour player;
    private int _lastAppliedIndex = -1;

    public override void Spawned()
    {
        ApplyWeapon(SelectedWeaponIndex);
        _lastAppliedIndex = SelectedWeaponIndex;

        InGameManager.Instance.weaponA.onClick.AddListener(() => RequestSwitch(0));
        InGameManager.Instance.weaponB.onClick.AddListener(() => RequestSwitch(1));
    }

    private void Update()
    {
        // Local input to request weapon changes
        if (Object.HasInputAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0)) RequestSwitch(0);
            if (Input.GetKeyDown(KeyCode.Keypad1)) RequestSwitch(1);
            
        }

        // Apply replicated value on all clients
        if (_lastAppliedIndex != SelectedWeaponIndex)
        {
            ApplyWeapon(SelectedWeaponIndex);
            _lastAppliedIndex = SelectedWeaponIndex;
        }
    }

    public void RequestSwitch(int index)
    {
        index = Mathf.Clamp(index, 0, weapons.Length - 1);

        if (Object.HasStateAuthority)
        {
            SelectedWeaponIndex = index;            
        }
        else
        {
            RPC_SetWeapon(index);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SetWeapon(int index)
    {
        SelectedWeaponIndex = index;        
    }

    private void ApplyWeapon(int index)
    {
        if (weapons == null || weapons.Length == 0) return;

        for (int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(i == index);
        player.animator.SetFloat("Weapon Type", index);
    }
}
