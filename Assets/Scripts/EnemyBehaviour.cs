using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent m_Navigator;
    private NavMeshAgent Navigator
    {
        get
        {
            if(m_Navigator == null)
            {
                Debug.LogError("No NavMeshAgent-component!");
            }
            return Navigator;
        }
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

    private float Timer = 2f;

	void Start ()
    {
        m_Navigator = GetComponent<NavMeshAgent>();
	}
	
	
	void Update ()
    {
        WasHit = false;

        Timer -= Time.deltaTime;
        if(Timer < 0)
        {
            Debug.Log("Hej");
            Timer = 5f;
            WasHit = true;
        }
    }
}
