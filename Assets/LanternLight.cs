using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

[RequireComponent(typeof(Light))]

public class LanternLight : MonoBehaviour {

    private FirstPersonController m_Controller;
    private Light m_Light;
    private PlayerChar m_PlayerChar;

    private float m_OilDrainWalk;
    private float m_OilDrainRun;

    [SerializeField]
    private GameObject m_MyController;
    private GameObject MyController
    {
        get { return m_MyController; }
    }

    // Use this for initialization
    void Start ()
    {
        m_Controller = MyController.GetComponent<FirstPersonController>();
        m_Light = GetComponent<Light>();
        m_PlayerChar = MyController.GetComponent<PlayerChar>();

        m_OilDrainWalk = 0.04f * Time.fixedDeltaTime;
        m_OilDrainRun = 0.1f * Time.fixedDeltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate ()
    {
        if (m_PlayerChar.PlayerOil > 0.2f)
        {
            if (m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= m_OilDrainWalk;

            else if (!m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= m_OilDrainRun;

            m_Light.intensity = m_PlayerChar.PlayerOil;
        }
        else
        {
            m_PlayerChar.PlayerOil = 0.2f;
            m_Light.intensity = m_PlayerChar.PlayerOil;
        }
    }
}
