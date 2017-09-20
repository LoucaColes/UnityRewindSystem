using UnityEngine;
using System.Collections;

public class Record : MonoBehaviour {

    private Rewindable m_rewindable; // Reference to the rewindable script
    [SerializeField] private float m_timeInterval; // Time between recording data

	// Use this for initialization
	void Start () {
        m_rewindable = GetComponent<Rewindable>(); // Get the reference to the rewindable script
    }

    // Record the gameobjects data
    public void RecordData()
    {
        // Check each component is going to be rewound then call that components
        // Record function as well as passing down the time interval float
        if (m_rewindable.GetRotBool())
        {
            m_rewindable.GetRotComp().SetTimeInterval(m_timeInterval);
            m_rewindable.GetRotComp().RecordRot();
        }
        if (m_rewindable.GetMatBool())
        {
            m_rewindable.GetRotComp().SetTimeInterval(m_timeInterval);
            m_rewindable.GetMatComp().RecordMat();
        }
        if (m_rewindable.GetPosBool())
        {
            m_rewindable.GetRotComp().SetTimeInterval(m_timeInterval);
            m_rewindable.GetPosComp().RecordPos();
        }
        if (m_rewindable.GetAnimBool())
        {
            //m_rewindable.GetRotComp().SetTimeInterval(m_timeInterval);
            m_rewindable.GetAnimComp().RecordAnim();
        }
        if(m_rewindable.GetAudBool())
        {
            m_rewindable.GetAudComp().PlayNormalAudio();
        }
    }
}
