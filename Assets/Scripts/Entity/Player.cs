using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : EntityHuman {
    public AudioClip SummonSound;
    public AudioClip GameOverSound;

    public string PlayerName;

    public static Player LocalPlayerInstance;

    protected override void Init()
	{
        OnDead += (entity,target) =>
         {
             if (isLocalPlayer)
             {
                 SoundSource.clip = GameOverSound;
                 SoundSource.Play();
             }
         };

        if(isLocalPlayer)
        {
            LocalPlayerInstance = this;
        }

        if(isClient)
        {
            SoundSource.clip = SummonSound;
            SoundSource.Play();
        }

        if (isServer)
        {
            RegisterSkill(Skills.PlayerAttackA);
            RegisterSkill(Skills.PlayerAttackB);
            RegisterSkill(Skills.PlayerAttackRange);
        }
        base.Init();
    }

    private void OnDestroy()
    {
        if (!isLocalPlayer) return;
        GameObject.Find("Joystick").SetActive(false);
        GameObject.Find("Attack").SetActive(false);
        LocalPlayerInstance = null;
    }

    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUI.TextArea(new Rect(0, 0, 200, 20), string.Format("HP:{0}/{1}  Energy:{2}/{3}  Kill:{4}", CurrentHP, MAX_HP, CurrentEnergy, MAX_ENERGY, Scoreboard.Instance.GetPlayerKillNumber(this)));
            GUI.TextArea(new Rect(0,20,100,20),string.Format("Time:{0}",MultiplayGameManager.Instance.Time));
        }
    }
}
