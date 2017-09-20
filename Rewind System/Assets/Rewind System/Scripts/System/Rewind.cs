using UnityEngine;
using System.Collections;

public class Rewind : MonoBehaviour {

    private Rewindable m_rewindable; // Reference to the Rewindable script
    [SerializeField] private float m_rateOfChange; // Rate of change in terms of transitions

    // Use this for initialization
    void Start () {
        // Get a reference to the rewindable script
        m_rewindable = GetComponent<Rewindable>();
    }

    // Pass the rate of change to each attached component
    void PassRateOfChange()
    {
        if (m_rewindable.GetMatBool())
        {
            m_rewindable.GetMatComp().SetRateOfChange(m_rateOfChange);
        }
        if (m_rewindable.GetRotBool())
        {
            m_rewindable.GetRotComp().SetRateOfChange(m_rateOfChange);
        }
        if (m_rewindable.GetPosBool())
        {
            m_rewindable.GetPosComp().SetRateOfChange(m_rateOfChange);
        }
    }

    // Rewind attached components data
    public void RewindData()
    {
        PassRateOfChange(); // Incase a component got missed pass the rate of change again
        if (m_rewindable.GetPosBool())
        {
            m_rewindable.GetPosComp().RewindPos();
        }
        if (m_rewindable.GetRotBool())
        {
            m_rewindable.GetRotComp().RewindRot();
        }
        if (m_rewindable.GetMatBool())
        {
            m_rewindable.GetMatComp().RewindMat();
        }
        if (m_rewindable.GetAnimBool())
        {
            m_rewindable.GetAnimComp().RewindAnim();
        }
        if (m_rewindable.GetAudBool())
        {
            m_rewindable.GetAudComp().PlayReverseAudio();
        }
    }

    // Reset any dirty flags that the attached components have
    public void ResetDirtyFlags()
    {
        if (m_rewindable.GetPosBool())
        {
            m_rewindable.GetPosComp().ResetDirtyFlags();
        }
        if (m_rewindable.GetRotBool())
        {
            m_rewindable.GetRotComp().ResetDirtyFlags();
        }
        if (m_rewindable.GetMatBool())
        {
            m_rewindable.GetMatComp().ResetDirtyFlags();
        }
    }
}
