using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDigger : MonoBehaviour {
    [SerializeField]
    private bool m_IgnoreBlockerTests = false;

    [SerializeField]
    private bool m_DebugLogs = false;

    private Stack<GameObject> m_PrefabBranch = new Stack<GameObject>();
    public Stack<GameObject> PrefabBranch
    {
        get { return m_PrefabBranch; }
    }

    [SerializeField]
    private GameObject m_FirstRoomPrefab;
    public GameObject FirstRoomPrefab
    {
        get { return m_FirstRoomPrefab; }
    }

    [SerializeField]
    private GameObject[] m_PrefabArray;
    public GameObject[] PrefabArray
    {
        get { return m_PrefabArray; }
    }

    int Counter = 0;
    
    void Start () {
        PrefabBranch.Push(FirstRoomPrefab);

        while(Counter < 10 && PrefabBranch.Peek() != null)
        {
            PrefabBranch.Push(BuildNextPiece());
            Counter++;
            if(m_DebugLogs)
                Debug.Log("Build room " + Counter.ToString());
        }
	}
	
	void Update () {
	    
	}

    // Builds the next piece on the current branch, and returns the GameObject. If unsuccessful, returns null.
    private GameObject BuildNextPiece()
    {
        Debug.Log("BuildNextPiece");
        GameObject NextPiece = null;

        ConnectionPoint[] NextConnections = GetConnections(PrefabBranch.Peek());
        if(NextConnections != null)
        {
            
            foreach (ConnectionPoint Connection in NextConnections)
            {
                Transform BuildFromTransform = Connection.transform;

                

                NextPiece = TestAndBuildPrefabs(BuildFromTransform, Connection.PreferedPrefabs);
                if(NextPiece == null)
                {
                    NextPiece = TestAndBuildPrefabs(BuildFromTransform, PrefabArray);
                }
                if(NextPiece != null)
                {
                    if (m_DebugLogs)
                        Debug.Log("Found a piece that worked, built a " + NextPiece.name);
                    break;
                }
            }
        }
        return NextPiece;
    }

    // Returns the ConnectionPoints of a prefab in random order. If no ConnectionPoints exist, returns null.
    private ConnectionPoint[] GetConnections(GameObject aPrefab)
    {
        Debug.Log("Connection");
        ConnectionPoint[] ReturnArray = null;

        ConnectionPoint[] Connections = aPrefab.GetComponentsInChildren<ConnectionPoint>();

        if(Connections.Length > 0)
        {
            ReturnArray = new ConnectionPoint[Connections.Length];
            int RandomAdd = Random.Range(0, Connections.Length);

            for(int i = 0; i < ReturnArray.Length; i++)
            {
                ReturnArray[i] = Connections[(i + RandomAdd) % Connections.Length];
            }
        }


        return ReturnArray;
    }

    // Tests an array of prefabs from a ConnectionPoint's transform, and returns the instantiated GameObject if successful, else null.
    private GameObject TestAndBuildPrefabs(Transform aFromTransform, GameObject[] aPrefabs)
    {
        Debug.Log("TestAndBuildPrefab");
        GameObject ReturnPrefab = null;

        // To "scramble" the list, we add a random integer to the index (andmod it by length).
        int IndexAdd = Random.Range(0, aPrefabs.Length);
        // Find all GameObjects who'll potentially block our new prefab.
        GameObject[] ExistingPrefabBlockers = GameObject.FindGameObjectsWithTag("PrefabBlocker");

        // Go through all prefabs until we find one who fit. 
        // Resource-intense - TODO: performance check?
        for (int i = 0; i < aPrefabs.Length; i++)
        {
            GameObject TestObject = aPrefabs[(i + IndexAdd) % aPrefabs.Length];
            BoxCollider[] TestBlockers = TestObject.GetComponentsInChildren<BoxCollider>();
            


            ConnectionPoint[] TestConnections = GetConnections(TestObject);

            foreach(ConnectionPoint Connection in TestConnections)
            {
                bool NoCollisions = true;
                Vector3 ConnectionOffset = Connection.transform.position;
                Quaternion ConnectionRotation = Connection.transform.rotation;

                
                // Go through all BoxColliders in this prefab.
                foreach (BoxCollider TestBlocker in TestBlockers)
                {
                    if (m_IgnoreBlockerTests)
                    {
                        continue;
                    }

                    // If the BoxCollider isn't a PrefabBlocker, we skip to the next.
                    if (TestBlocker.tag != "PrefabBlocker")
                    {
                        continue;
                    }

                    // The "blueprint" transform - note that TestBlocker doesn't exist yet, so its transform are just local coordinates.
                    Transform TestTransform = TestBlocker.transform;

                    // Move the blueprint transform through the other ConnectionPoint's transform, and then offset with our prefab's transform.
                    TestTransform.position += aFromTransform.position - ConnectionOffset;
                    TestTransform.Rotate(ConnectionRotation.eulerAngles);

                    // Go through ALL other potential blockers (tagged "PrefabBlocker").
                    foreach (GameObject OtherBlockers in ExistingPrefabBlockers)
                    {
                        BoxCollider OtherCollider = OtherBlockers.GetComponent<BoxCollider>();

                        // Make sure the Collider actually exists, then see if it blocks this blocker. If it does, we can break from this Connection, and try the next.
                        if (OtherCollider != null && BoundingBoxCollision.TestCollision(TestTransform, TestBlocker.size, OtherCollider.transform, OtherCollider.size))
                        {
                            NoCollisions = false;
                            break;
                        }
                    }
                    if (NoCollisions == false)
                    {
                        break;
                    }
                }
                if(NoCollisions)
                {
                    // Build the thing from this place, then break.
                    ReturnPrefab = Instantiate(TestObject);

                    break;
                }
            }

            // The latest prefab was successfully built, no need to check any more.
            if(ReturnPrefab != null)
            {
                break;
            }
        }

        return ReturnPrefab;
    }
}
