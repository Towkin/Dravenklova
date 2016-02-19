using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{
    private Transform[] m_Points;
    public Transform[] Points
    {
        get { return m_Points; }
        set { m_Points = value; }
    }
    private int m_DestIndex = 0;
    public int DestinationIndex
    {
        get { return m_DestIndex; }
        private set { m_DestIndex = value; }
    }
    private NavMeshAgent m_Agent;
    public NavMeshAgent Agent
    {
        get { return m_Agent; }
    }






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


        


        if (Points == null || Points.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        m_Agent.destination = Points[DestinationIndex].position;

        Debug.Log("New destination: " + m_Agent.destination.ToString());

        // Choose the next point in the array as the destination,
        // cycling to the start if nessessary.
        DestinationIndex = (DestinationIndex + Random.Range(1, Points.Length-1)) % Points.Length;
        

    }

    // Update is called once per frame
    void Update()
    {
        // If looking at player and distance < maxDistance follow the player.


        // else if

        


        // Move towards the player...


        // Choose the next destination point when the agent gets
        // close to the current one.
        if (m_Agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

    }
    
    
}
