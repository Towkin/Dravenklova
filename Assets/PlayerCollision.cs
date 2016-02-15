using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    PlayerChar m_PlayerChar;

	// Use this for initialization
	void Start ()
    {
        m_PlayerChar = GetComponent<PlayerChar>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter (Collision Coll)
    {
        if (Coll.gameObject.tag == "Food")
        {
            m_PlayerChar.PlayerHealth += 1f;
        }
        if (Coll.gameObject.tag == "Oil")
        {
            m_PlayerChar.PlayerOil += 2f;
        }
        if (Coll.gameObject.tag == "Bolt")
        {
            m_PlayerChar.PlayerAmmo += 1;
        }
    }
}
