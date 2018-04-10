using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EntityHuman : Entity {

	private Transform m_weapon_point;
	private Weapon m_current_weapon;
	public Weapon CurrentWeapon{
		set
		{
            SwitchWeapon(value);
        }
		get
		{
			return m_current_weapon;
		}
	}

    protected void SwitchWeapon(Weapon weapon)
    {
        if (isClient)
        {
            m_current_weapon = null;
            CmdSwitchWeapon(weapon.GetComponent<NetworkIdentity>());
        }
    }

    [Command]
    private void CmdSwitchWeapon(NetworkIdentity weapon_id)
    {
        NetworkServer.Destroy(m_current_weapon.gameObject);

        var weapon_prefab = weapon_id.GetComponent<Weapon>();
        var weapon=Instantiate(weapon_prefab);
        weapon.transform.parent = m_weapon_point;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        NetworkServer.Spawn(weapon.gameObject);
        RpcSetCurrentWeapon(weapon.GetComponent<NetworkIdentity>());
    }

    [ClientRpc]
    private void RpcSetCurrentWeapon(NetworkIdentity weapon_id)
    {
        m_current_weapon = weapon_id.GetComponent<Weapon>();
    }


    protected override void Init()
	{
        if (isServer)
        {
            foreach (var t in GetComponentsInChildren<Transform>())
            {
                if (t.name == "WeaponPoint")
                {
                    m_weapon_point = t;
                    break;
                }
            }
        }
        base.Init();
    }
}
