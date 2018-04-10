using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class  Weapon : MonoBehaviour {

	public float ATK = 5.0f;
	public float RandomFactor=1.0f;
	public float RandomRange=2.0f;

    /// <summary>
    /// 攻击目标实体
    /// </summary>
    /// <param name="entity">攻击目标.</param>
    void DamageEntity(Entity entity)
    {
        var animator = GetComponentInParent<Animator>();
        var player = GetComponentInParent<Entity>();
        if (entity.Team == player.Team)
            return;

        Skill skill = player.GetSkill(animator.GetCurrentAnimatorStateInfo(0).shortNameHash);
        if (skill == null)
            return;

        Debug.Log(string.Format("{0} Attack {1}(Use {2})", player.name, entity.name, name));
        if (skill.RemainTime <= 0.0f)
        {
            skill.Attack(this, player, entity);
            AttachBuff(entity);//武器buff
        }
    }

	void OnTriggerEnter(Collider collider)
	{
        if (NetworkServer.active)
        {
            var entity = collider.GetComponentInParent<Entity>();

            if (entity == null)
                throw new UnityException("No Found Entity");
            else if (entity == this.GetComponentInParent<Entity>())
                return;

            if (collider.tag == "Body")
                DamageEntity(entity);
        }
	}

	public virtual void AttachBuff (Entity entity){}
}
