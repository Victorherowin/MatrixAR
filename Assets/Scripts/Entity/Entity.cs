using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 实体类。重写Start/FixedUpdate函数需要在第一行调用父类的Start/FixedUpdate
/// </summary>
public class Entity : NetworkBehaviour {
    public float MAX_HP = 200.0f;
    public int MAX_ENERGY = 5;


    [SyncVar] public float CurrentHP;
    [SyncVar] public bool isSurvival = true;
    [SyncVar] public int CurrentEnergy;
    [SyncVar] public float ATK=2.5f;

    public float InvincibleTime = 1.0f;
    private float m_invincible_remain_time = 0.0f;

    public int Team;

    private static GameObject _buffs_prefab = null;
    private static GameObject _skills_prefab = null;

    private GameObject m_skills;
    private GameObject m_buffs;

    public AudioClip DeadSound;
    private AudioSource m_audio_source;
    protected AudioSource SoundSource{get{return m_audio_source;}}

    void Start()
	{
		if (isServer) {
			if (_buffs_prefab == null)
				_buffs_prefab = (GameObject)Resources.Load ("Prefab/EntityAttribute/Buffs");
			if (_skills_prefab == null)
				_skills_prefab = (GameObject)Resources.Load ("Prefab/EntityAttribute/Skills");

			m_buffs = GameObject.Instantiate (_buffs_prefab, transform);
			m_skills = GameObject.Instantiate (_skills_prefab, transform);
			m_buffs.name = "Buffs";
			m_skills.name = "Skills";

            CurrentHP = MAX_HP;
            CurrentEnergy = MAX_ENERGY;
        }

        if(isClient)
        {
            m_audio_source = gameObject.AddComponent<AudioSource>();
            m_audio_source.loop = false;
        }

        OnDead += (entity,target) =>
          {
              if (!isClient || DeadSound == null) return;
              m_audio_source.clip = DeadSound;
              m_audio_source.Play();
          };

        Init();
	}
    /// <summary>
    /// 子类在第最后一行调用base.Init();
    /// </summary>
	protected virtual void Init()
	{
        //收尾工作
		if (isServer) {
			foreach (var s in tmp_skill_cache) {
				var skill = m_skills.AddComponent (s.GetType ());
				_cache_skills [Animator.StringToHash (s.Name)] = skill as Skill;
			}
			NetworkServer.Spawn (m_skills);
			NetworkServer.Spawn (m_buffs);

			tmp_skill_cache = null;
		}
	}
		
	Dictionary<int,Skill> _cache_skills=new Dictionary<int,Skill>();
	List<Skill> tmp_skill_cache = new List<Skill> ();

	/// <summary>
	/// 要求在Awake内使用
	/// </summary>
	/// <param name="skill">要注册的技能.</param>
	public void RegisterSkill(Skill skill)
	{
		tmp_skill_cache.Add (skill);
	}

	public Skill GetSkill(int hash)
	{
		Skill s;
		try{
			s=_cache_skills[hash];
		}catch{
			s = null;
		}
		return s;
	}

	public T GetSkill<T>()where T:Skill 
	{
		return m_skills.GetComponent<T> ();
	}

	/// <summary>
	/// 给实体添加Buff
	/// </summary>
	/// <param name="index">Buffs中的Buff索引.</param>
	public void AddBuff(Entity entity,Buffs.BuffIndex index)
	{
		CmdAddBuff (entity.GetComponent<NetworkIdentity>(),index);
	}

	private List<Buff> _cache_buffs=new List<Buff>();

	[Command]
	private void CmdAddBuff(NetworkIdentity entity,Buffs.BuffIndex buff_index)
	{
		var buff = Buffs.GetBuff (buff_index);
		var buff_obj = GameObject.Instantiate (_buffs_prefab, m_buffs.transform);

		buff_obj.name = buff.Name;
		_cache_buffs.Add(buff_obj.AddComponent (buff.GetType ())as Buff);

        buff = buff_obj.GetComponent<Buff>();
        buff.Entity = entity.GetComponent<Entity>();
        buff.StartBuff(this);

        NetworkServer.Spawn (buff_obj);
	}

	/// <summary>
	/// 移除第一个找到的Buff Object
	/// </summary>
	/// <param name="index">Index.</param>
	public void RemoveBuff(Buffs.BuffIndex index) 
	{
		CmdRemoveBuff (Buffs.GetBuff (index).Name);
	}

	/// <summary>
	/// 移除第一个找到的Buff Object
	/// </summary>
	/// <param name="name">buff 名</param>
	public void RemoveBuff(string name) 
	{
		CmdRemoveBuff (name);
	}

    /// <summary>
    /// 移除Buff Object(Server Only)
    /// </summary>
    /// <param name="name">buff</param>

    public void RemoveBuff(Buff buff)
    {
        _cache_buffs.Remove(buff);
        NetworkServer.Destroy(buff.gameObject);
    }

    [Command]
    private void CmdRemoveBuff(string buff_name)
    {
        Buff buff = null;
        GameObject buff_obj = null;

        foreach (Buff b in _cache_buffs)
        {
            if (b.Name == buff_name)
            {
                buff = b;
                break;
            }
        }
		if (buff != null) {
			buff_obj = buff.gameObject;
			_cache_buffs.Remove (buff);
		}

		if (buff_obj != null)
			NetworkServer.Destroy (buff_obj);
	}

	/// <summary>
	/// 是否在无敌时间内
	/// </summary>
	/// <returns><c>true</c> if this instance is invincible; otherwise, <c>false</c>.</returns>
	bool IsInvincible()
	{
		return m_invincible_remain_time <= 0.0f ? false : true;
	}
    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="entity">攻击者</param>
    /// <param name="damage">伤害值</param>
	public void TakeDamage(Entity entity,float damage)
	{
		if (!IsInvincible ()) {
			CurrentHP -= damage;
            if (isSurvival)
                if (CurrentHP <= 0.0f)
                {
                    isSurvival = false;
                    if (OnDead != null)
                    {
                        OnDead(entity,this);
                        RpcDelegateOnDead(entity.GetComponent<NetworkIdentity>());
                    }
                }
            m_invincible_remain_time = InvincibleTime;
		}
	}

    [ClientRpc]
    void RpcDelegateOnDead(NetworkIdentity id)
    {
        OnDead(id.GetComponent<Entity>(),this);
    }

    public void TakeDamageWithoutInvincible(float damage)
    {
        CurrentHP -= damage;
    }
    /// <summary>
    /// 角色死亡时调用(Server/Client)
    /// </summary>
    /// <param name="entity">攻击者</param>
    //  <param name="target">被攻击者</param>
    public delegate void OnDeadEvt(Entity entity,Entity target);
    public event OnDeadEvt OnDead;

    protected virtual void FixedUpdate()
	{
		if (m_invincible_remain_time > 0.0f) {
			m_invincible_remain_time -= Time.fixedDeltaTime;
		}
        if (!isServer) return;

		foreach (Buff buff in _cache_buffs) {
			buff.ApplyBuff (this);
            buff.UpdateRemainTime();

            if (buff.RemainTime <= 0)
            {
                buff.DestroyBuff(this);
                RemoveBuff(buff);
            }

            if (isSurvival)
                if (CurrentHP <= 0.0f)
                {
                    isSurvival = false;
                    if (OnDead != null)
                        OnDead(buff.Entity,this);
                }
        }
    }

    protected virtual void LateUpdate()
    {
        if (isSurvival == false)
            NetworkServer.Destroy(gameObject);
    }
    /*
	void OnGUI()
	{
		if (!isLocalPlayer)
			return;
		
		if (GUI.Button (new Rect(0,300,100,100),"Add TestBuff"))
			AddBuff(Buffs.BuffIndex.TestBuff);
		if (GUI.Button (new Rect(0,400,100,100),"Rmove TestBuff"))
			RemoveBuff(Buffs.BuffIndex.TestBuff);
	}*/
}
