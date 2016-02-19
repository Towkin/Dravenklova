using UnityEngine;
using FMOD.Studio;
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
    private Animator m_Animations;
    public Animator Animations
    {
        get { return m_Animations; }
    }
    private float m_FootstepTimer = 0f;
    public float FootstepTimer
    {
        get { return m_FootstepTimer; }
        set { m_FootstepTimer = value; }
    }

    [SerializeField] [FMODUnity.EventRef]
    private string m_FootstepEvent;
    private EventInstance m_FootstepState;
    [SerializeField] [FMODUnity.EventRef]
    private string m_VoiceIdleEvent;
    private EventInstance m_VoiceIdle;
    [SerializeField] [FMODUnity.EventRef]
    private string m_VoiceAttackEvent;
    private EventInstance m_VoiceAttack;
    [SerializeField] [FMODUnity.EventRef]
    private string m_VoiceDeathEvent;
    private EventInstance m_VoiceDeath;
    [SerializeField] [FMODUnity.EventRef]
    private string m_VoiceHitEvent;
    private EventInstance m_VoiceHit;


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

    [SerializeField]
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

    //private bool m_CanAttack = true;
    public bool CanAttack
    {
        get
        {
            return AttackTimer <= 0f;
        }
    }

    private bool m_IsAttacking = false;
    public bool IsAttacking
    {
        get { return m_IsAttacking; }
        private set
        {
            m_IsAttacking = value;
            if (m_IsAttacking)
            {
                m_AttackTimer = m_AttackTimerMax;
                m_VoiceAttack.setTimelinePosition(0);
                m_VoiceAttack.start();
            }
        }
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
                m_VoiceDeath.setTimelinePosition(0);
                m_VoiceDeath.start();
            }
        }
    }
    private bool m_WasHit = false;
    public bool WasHit
    {
        get { return m_WasHit; }
        private set {
            m_WasHit = value;
            if (m_WasHit)
            {
                m_VoiceHit.setTimelinePosition(0);
                m_VoiceHit.start();
            }
        }
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
        private set {
            m_BeginHunt = value;
            if(m_BeginHunt)
            {
                m_VoiceIdle.setTimelinePosition(0);
                m_VoiceIdle.start();
            }
        }
    }

    //private float Timer = 2f;

	void Start ()
    {
        m_Navigator = GetComponent<NavMeshAgent>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerInfo = Player.GetComponent<PlayerChar>();
        m_Animations = GetComponent<Animator>();

        m_FootstepState = FMODUnity.RuntimeManager.CreateInstance(m_FootstepEvent);
        m_VoiceAttack = FMODUnity.RuntimeManager.CreateInstance(m_VoiceAttackEvent);
        m_VoiceDeath = FMODUnity.RuntimeManager.CreateInstance(m_VoiceDeathEvent);
        m_VoiceHit = FMODUnity.RuntimeManager.CreateInstance(m_VoiceHitEvent);
        m_VoiceIdle = FMODUnity.RuntimeManager.CreateInstance(m_VoiceIdleEvent);
    }
	
	
	void Update ()
    {
        IsAttacking = false;
        WasHit = false;
        BeginHunt = false;

        if (!IsDead)
        {
            FootstepTimer -= Time.deltaTime;
            if(FootstepTimer <= 0)
            {
                var CurrentAnimationState = Animations.GetCurrentAnimatorStateInfo(0);
                float FootstepMaxTime = CurrentAnimationState.length / 2;
                FootstepTimer = FootstepMaxTime;
                m_FootstepState.setTimelinePosition(0);
                m_FootstepState.start();
            }

            AttackTimer -= Time.deltaTime;
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
                    Debug.Log("Player health: " + PlayerInfo.PlayerHealth.ToString());
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

            FMOD.ATTRIBUTES_3D Attributes3D = new FMOD.ATTRIBUTES_3D();
            Attributes3D.position.x = transform.position.x;
            Attributes3D.position.y = transform.position.y;
            Attributes3D.position.z = transform.position.z;

            Attributes3D.forward.x = transform.forward.x;
            Attributes3D.forward.y = transform.forward.y;
            Attributes3D.forward.z = transform.forward.z;

            m_FootstepState.set3DAttributes(Attributes3D);
            m_VoiceAttack.set3DAttributes(Attributes3D);
            m_VoiceDeath.set3DAttributes(Attributes3D);
            m_VoiceHit.set3DAttributes(Attributes3D);
            m_VoiceIdle.set3DAttributes(Attributes3D);
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
