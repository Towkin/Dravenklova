using UnityEngine;
using UnityEditor;
using System.Collections;

public class NavmeshTest : MonoBehaviour {

	// Use this for initialization
	void Awake() {
        NavMeshBuilder.BuildNavMesh();
        Debug.Log("RUNNING");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
