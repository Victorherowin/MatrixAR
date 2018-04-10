using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterPlayer : NetworkBehaviour {

    Player m_player;

    // Use this for initialization
    public override void OnStartServer () {
        m_player = GetComponent<Player>();
        m_player.OnDead += (entity,target) =>
          {
              MultiplayGameManager.Instance.SurvivalNumber--;
              Scoreboard.Instance.SubmitDeadPlayer(entity, target);
          };
        PlayerManager.Instance.RegisterPlayer(m_player);
        base.OnStartServer();

    }

    private void OnDestroy()
    {
        if (!isServer) return;
        PlayerManager.Instance.UnregisterPlayer(m_player);
    }
}
