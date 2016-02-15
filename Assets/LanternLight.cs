using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LanternLight : MonoBehaviour {

    private Light m_Light;
    private PlayerChar m_PlayerChar;
    
    [SerializeField]
    private GameObject m_MyController;
    private GameObject MyController
    {
        get { return m_MyController; }
    }

    // Use this for initialization
    void Start ()
    {
        m_Light = GetComponent<Light>();
        m_PlayerChar = MyController.GetComponent<PlayerChar>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate ()
    {
        if (m_PlayerChar.PlayerOil > 0.2f)
        {
            m_PlayerChar.PlayerOil -= .04f * Time.fixedDeltaTime;
            m_Light.intensity = m_PlayerChar.PlayerOil;
        }
        else
        {
            m_PlayerChar.PlayerOil = 0.2f;
            m_Light.intensity = m_PlayerChar.PlayerOil;
        }
    }
}
