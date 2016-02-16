using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDigger : MonoBehaviour {

    private Stack<GameObject> m_PrefabBranch = new Stack<GameObject>();
    public Stack<GameObject> PrefabBranch
    {
        get { return m_PrefabBranch; }
    }

    [SerializeField]
    private GameObject m_FirstPrefab;
    public GameObject FirstPrefab
    {
        get { return m_FirstPrefab; }
    }

    [SerializeField]
    private GameObject[] m_PrefabArray;
    public GameObject[] PrefabArray
    {
        get { return m_PrefabArray; }
    }
    
    void Start () {
	    
	}
	
	void Update () {
	    
	}

    private bool BuildNextPiece()
    {
        ConnectionPoint[] NextConnections = GetConnections(PrefabBranch.Peek());
        if(NextConnections != null)
        {
            foreach (ConnectionPoint Connection in NextConnections)
            {
                Transform BuildFromTransform = Connection.transform;
                
                
            }
            

            return true;
        }
        return false;
    }

    private ConnectionPoint[] GetConnections(GameObject aPrefab)
    {
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

    private GameObject TestPrefabs(Transform aFromTransform, GameObject[] aPrefabs)
    {
        int RandomAdd = Random.Range(0, aPrefabs.Length);
        GameObject[] ExistingPrefabBlockers = GameObject.FindGameObjectsWithTag("PrefabBlocker");

        GameObject ReturnPrefab = null;

        for (int i = 0; i < aPrefabs.Length; i++)
        {
            GameObject TestObject = aPrefabs[(i + RandomAdd) % aPrefabs.Length];
            BoxCollider[] TestColliders = TestObject.GetComponentsInChildren<BoxCollider>();


            ConnectionPoint[] TestConnections = GetConnections(TestObject);

            foreach(ConnectionPoint Connection in TestConnections)
            {
                bool NoCollisions = true;
                foreach (BoxCollider TestCollider in TestColliders)
                {
                    Bounds TestBounds = TestCollider.bounds;
                    

                    foreach (GameObject OtherBlockers in ExistingPrefabBlockers)
                    {
                        BoxCollider OtherCollider = OtherBlockers.GetComponent<BoxCollider>();
                        if (TestCollider.bounds.Intersects(OtherCollider.bounds))
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
