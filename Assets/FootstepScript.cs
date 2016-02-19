using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using FMOD.Studio;
using System.Collections;


public class FootstepScript : MonoBehaviour {

    private ParameterInstance m_FootstepsWalking;
    private ParameterInstance m_FootstepsRunning;
    private EventInstance m_FootstepState;

    [SerializeField]
    [FMODUnity.EventRef]
    private string m_FootstepEvent;

    [SerializeField]
    private FirstPersonController m_Controller;
    private Rigidbody m_ControllerBody;


    void Start () {
        m_ControllerBody = m_Controller.GetComponent<Rigidbody>();

        m_FootstepState = FMODUnity.RuntimeManager.CreateInstance(m_FootstepEvent);
        m_FootstepState.start();
        

        m_FootstepState.getParameterByIndex(0, out m_FootstepsWalking);
        m_FootstepState.getParameterByIndex(1, out m_FootstepsRunning);
        
	}
	void OnDestroy()
    {
        m_FootstepState.release();
    }

	void Update () {

        FMOD.ATTRIBUTES_3D Attributes3D = new FMOD.ATTRIBUTES_3D();
        Attributes3D.position.x = transform.position.x;
        Attributes3D.position.y = transform.position.y;
        Attributes3D.position.z = transform.position.z;

        Attributes3D.forward.x = transform.forward.x;
        Attributes3D.forward.y = transform.forward.y;
        Attributes3D.forward.z = transform.forward.z;

        Attributes3D.velocity.x = m_ControllerBody.velocity.x;
        Attributes3D.velocity.y = m_ControllerBody.velocity.y;
        Attributes3D.velocity.z = m_ControllerBody.velocity.z;

        m_FootstepState.set3DAttributes(Attributes3D);


        float WalkingCurrent, WalkingTarget, RunningCurrent, RunningTarget;
        m_FootstepsWalking.getValue(out WalkingCurrent);
        m_FootstepsRunning.getValue(out RunningCurrent);
        WalkingTarget = m_Controller.PlayerMoving ? 1f : 0f;
        RunningTarget = WalkingTarget == 1f ? (m_Controller.PlayerWalking ? 0f : 1f) : 0f;

        m_FootstepsWalking.setValue(Mathf.Lerp(WalkingCurrent, WalkingTarget, 0.05f));
        m_FootstepsRunning.setValue(Mathf.Lerp(RunningCurrent, RunningTarget, 0.05f));
        

        

        
	}
}
