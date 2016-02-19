using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_WalkSpeed = 2f;
    public float WalkSpeed
    {
        get { return m_WalkSpeed; }
        private set { m_WalkSpeed = value; }
    }
    [SerializeField]
    private float m_WalkAcceleration = 2f;
    public float WalkAcceleration
    {
        get { return m_WalkAcceleration; }
        private set { m_WalkAcceleration = value; }
    }
    [SerializeField]
    private float m_WalkRotSpeed = 180f;
    public float WalkRotSpeed
    {
        get { return m_WalkRotSpeed; }
        private set { m_WalkRotSpeed = value; }
    }
    [SerializeField]
    private float m_RunSpeed = 4f;
    public float RunSpeed
    {
        get { return m_RunSpeed; }
        private set { m_RunSpeed = value; }
    }
    [SerializeField]
    private float m_RunAcceleration = 8f;
    public float RunAcceleration
    {
        get { return m_RunAcceleration; }
        private set { m_RunAcceleration = value; }
    }
    [SerializeField]
    private float m_RunRotSpeed = 720f;
    public float RunRotSpeed
    {
        get { return m_RunRotSpeed; }
        private set { m_RunRotSpeed = value; }
    }

    [SerializeField]
    private float m_ViewConeAngle = 80f;
    public float ViewConeAngle
    {
        get { return m_ViewConeAngle; }
    }
    [SerializeField]
    private float m_ViewDistance = 50f;
    public float ViewDistance
    {
        get { return m_ViewDistance; }
    }
    [SerializeField]
    private float m_DamageMin = 10f;
    public float DamageMin
    {
        get { return m_DamageMin; }
    }
    [SerializeField]
    private float m_DamageMax = 20f;
    public float DamageMax
    {
        get { return m_DamageMax; }
    }

    public float Damage
    {
        get { return Random.Range(m_DamageMin, m_DamageMax); }
    }


    private GameObject m_Player;
    private GameObject Player
    {
        get { return m_Player; }
    }
    private PlayerChar m_PlayerInfo;
    private PlayerChar PlayerInfo
    {
        get { return m_PlayerInfo; }
    }

    private Vector3 m_LastKnownPlayerPosition = new Vector3();
    public Vector3 LastKnownPlayerPosition
    {
        get { return m_LastKnownPlayerPosition; }
        set { m_LastKnownPlayerPosition = value; }
    }
    
    private NavMeshAgent m_Navigator;
    private NavMeshAgent Navigator
    {
        get
        {
            if(m_Navigator == null)
            {
                Debug.LogError("No NavMeshAgent-component!");
            }
            return m_Navigator;
        }
    }
    private float m_Health = 1f;
    private float m_MaxHealth = 1f;
    public float Health
    {
        get { return m_Health; }
        set
        {
            m_Health = Mathf.Clamp(value, 0f, m_MaxHealth);
            if(m_Health == 0f)
            {
                IsDead = true;
                Destroy(GetComponent<CapsuleCollider>());
            }
        }
    }


    private float m_AttackTimer = 0f;
    private float AttackTimer
    {
        get { return m_AttackTimer; }
        set { m_AttackTimer = value; }
    }
    private float m_AttackTimerMax = 1.5f;
    private float AttackTimerMax
    {
        get { return m_AttackTimerMax; }
    }

    private bool m_CanAttack = true;
    public bool CanAttack
    {
        get
        {
            if (!m_CanAttack)
            {
                m_CanAttack = (AttackTimer = Mathf.Max(AttackTimer - Time.deltaTime, 0f)) <= 0f;
            }
            return m_CanAttack;
        }
        private set { m_CanAttack = value; }
    }

    private bool m_IsAttacking = false;
    public bool IsAttacking
    {
        get { return m_IsAttacking; }
        private set { m_IsAttacking = value; }
    }
    private bool m_IsDead = false;
    public bool IsDead
    {
        get { return m_IsDead; }
        private set
        {
            m_IsDead = value;
            if (m_IsDead)
            {
                m_Navigator.speed = 0.0f;
            }
        }
    }
    private bool m_WasHit = false;
    public bool WasHit
    {
        get { return m_WasHit; }
        private set { m_WasHit = value; }
    }
    private bool m_SeePlayer = false;
    public bool SeePlayer
    {
        get { return m_SeePlayer; }
        private set { m_SeePlayer = value; }
    }
    
    private bool m_Hunting = false;
    public bool Hunting
    {
        get { return m_Hunting; }
        private set { m_Hunting = value; }
    }
    private bool m_BeginHunt = false;
    public bool BeginHunt
    {
        get { return m_BeginHunt; }
        private set { m_BeginHunt = value; }
    }

    //private float Timer = 2f;

	void Start ()
    {
        m_Navigator = GetComponent<NavMeshAgent>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerInfo = Player.GetComponent<PlayerChar>();
        //m_Animations = GetComponent<Animator>();
	}
	
	
	void Update ()
    {
        IsAttacking = false;
        WasHit = false;
        BeginHunt = false;

        if (!IsDead)
        {
            if(SeePlayer = FindPlayer())
            {
                if(!Hunting)
                {
                    BeginHunt = true;
                }
                LastKnownPlayerPosition = Player.transform.position;
                Hunting = true;
            }
            
            
            if(Hunting)
            {
                Navigator.speed = RunSpeed;
                Navigator.angularSpeed = RunRotSpeed;
                Navigator.acceleration = RunAcceleration;
                Navigator.destination = LastKnownPlayerPosition;

                if(CanAttack && Vector3.Distance(Player.transform.position, transform.position) < 2f)
                {
                    IsAttacking = true;
                    PlayerInfo.PlayerHealth -= Damage;
                }

                if(!SeePlayer && Navigator.remainingDistance < 1f)
                {
                    Hunting = false;
                }
            }
            else
            {
                Navigator.speed = WalkSpeed;
                Navigator.angularSpeed = WalkRotSpeed;
                Navigator.acceleration = WalkAcceleration;
            }
        }
    }

    private bool FindPlayer()
    {
        bool SeePlayer = false;
        Vector3 PlayerDirection = (Player.transform.position - transform.position).normalized;
        
        if (Vector3.Angle(transform.forward, PlayerDirection) < ViewConeAngle)
        {
            RaycastHit Hit = new RaycastHit();
            if (Physics.Raycast(transform.position, PlayerDirection, out Hit, ViewDistance) && Hit.collider.tag == "Player")
            {
                SeePlayer = true;
            }
        }
        
        return SeePlayer;        
    }
}
