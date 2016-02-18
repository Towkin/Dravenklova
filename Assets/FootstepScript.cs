using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using FMOD.Studio;
using System.Collections;


public class FootstepScript : MonoBehaviour {

    private ParameterInstance m_FootstepsWalking;
    private ParameterInstance m_FootstepsRunning;

    [SerializeField]
    private FirstPersonController m_Controller;



    void Start () {
        //FMODAsset FootstepSounds = GetComponent<FMOD_StudioEventEmitter>().asset;

        // Fook it, get this to work some fun day!
        //EventInstance FootstepSounds = GetComponent<FMOD_StudioEventEmitter>();

        //FootstepSounds.getParameterByIndex(0, out m_FootstepsWalking);
        //FootstepSounds.getParameterByIndex(0, out m_FootstepsRunning);
        
	}
	/*
	void Update () {
        m_FootstepsWalking.setValue(m_Controller.PlayerMoving ? 1 : 0);

        if (m_Controller.PlayerMoving)
        {
            m_FootstepsRunning.setValue(m_Controller.PlayerWalking ? 0 : 1);
        }
        else
        {
            m_FootstepsRunning.setValue(0);
        }
	}*/
}
