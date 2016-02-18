using UnityEngine;
using System.Collections;

public class TestBoundingBox : MonoBehaviour {

    public GameObject m_OtherBox;
	
	void Update () {
	    if(m_OtherBox != null)
        {
            BoxCollider ThisBoxInfo = gameObject.GetComponent<BoxCollider>();
            BoxCollider OtherBoxInfo = m_OtherBox.GetComponent<BoxCollider>();

            bool IsColliding = BoundingBoxCollision.TestCollision(gameObject.transform, ThisBoxInfo.size, m_OtherBox.transform, OtherBoxInfo.size);

            Debug.Log((IsColliding ? "There was a collision!" : "No collision..."));
        }
	}
}
