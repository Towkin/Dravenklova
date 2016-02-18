using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{

    // Want to make the Enemy follow the target(Player)...
    [SerializeField]
    private bool m_Chasing; // Is he chasing the player?
    [SerializeField]
    private GameObject m_Target; // The target we want to follow(player)
    public float m_Distance = 20f;


     


    [SerializeField]
    private Transform[] m_Points;
    private int m_DestPoint = 0;
    private NavMeshAgent m_Agent;







    void Awake()
    {

       
        
    }
    // Use this for initialization
    void Start()
    {

      



        m_Agent = GetComponent<NavMeshAgent>();

        // Disabling auto-barking allows for continous movement
        // between points, (ie, the agent dosnt slow down as it approches a destination point).

        //m_Agent.autoBraking = false;
       
        GotoNextPoint();

    }


    void GotoNextPoint()
    {
        //Returns if no points have been set up


        


        if (m_Points.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        m_Agent.destination = m_Points[m_DestPoint].position;


        // Choose the next point in the array as the destination,
        // cycling to the start if nessessary.
        m_DestPoint = (m_DestPoint + 1) % m_Points.Length;

    }

    // Update is called once per frame
    void Update()
    {
        // If looking at player and distance < maxDistance follow the player.


        // else if


        if (Vector3.Distance(m_Target.transform.position, transform.position) <= m_Distance)
        {

            Debug.Log("wat up");

        }


        // Move towards the player...


        // Choose the next destination point when the agent gets
        // close to the current one.
        if (m_Agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

    }
    
    
}
