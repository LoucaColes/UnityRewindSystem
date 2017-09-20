using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationComponent : MonoBehaviour {

    [SerializeField] private List<Quaternion> m_recordedRot; //Stores the recorded data
    private int m_recordLimit; // How much data is recorded
    private Quaternion m_lastRot; // Last rotation value that was recorded
    private float m_rotTimer; // Timer
    [SerializeField] private float m_timeInterval; // The frames that each data is recorded at e.g. one rotation every 0.5 frames
    [SerializeField] private float m_rateOfChange; // How fast the transition from rotation to rotation
    private int m_rotCounter; // Position within the list when rewinding
    private bool m_rotCounterSet; // Dirty flag so counter only gets set once
    public float m_valueBoost; // Used to boost the rate of change

    // Use this for initialization
    void Start () {
        m_recordedRot = new List<Quaternion>(); // Create the list
        m_rotTimer = 0.0f; // Set timer to 0
        m_rotCounterSet = false; // Set dirty flag to false
    }

    // Set the record limit for this component
    public void SetRecordLimit(int _newLimit)
    {
        m_recordLimit = _newLimit;
    }

    // Set the rate of change for this component
    public void SetRateOfChange(float _newROC)
    {
        m_rateOfChange = _newROC;
    }

    // Set the time interval for this component
    public void SetTimeInterval(float _newTimeInterval)
    {
        m_timeInterval = _newTimeInterval;
    }

    // Clears the lists data
    public void ResetData()
    {
        m_recordedRot.Clear();
    }

    // Add a rotation value to the list
    public void AddRot(Quaternion _newRot)
    {
        m_recordedRot.Add(_newRot);
    }

    // Get a rotation value from the list at a specific index
    public Quaternion GetRot(int _index)
    {
        return m_recordedRot[_index];
    }

    // Get the count value of the list
    public int GetRotListCount()
    {
        return m_recordedRot.Count;
    }

    // Remove the first value in the list
    public void RemoveFirstRotValue()
    {
        m_recordedRot.RemoveAt(0);
    }

    // Record objects rotation
    public void RecordRot()
    {
        // If the timer has reached the right frame count record a value
        // If time interval is set to 0 it will record every frame
        if (m_rotTimer > m_timeInterval)
        {
            // If the rotation is not the same as the last one add it to the list
            // And store the new rotation as the last
            if (gameObject.transform.rotation != m_lastRot)
            {
                AddRot(gameObject.transform.rotation);
                m_lastRot = gameObject.transform.rotation;
            }
            m_rotTimer = 0; // Reset the timer
        }

        m_rotTimer += Time.deltaTime; // Increase timer value

        // If the count is greater than the record limit remove the first value
        if (GetRotListCount() > m_recordLimit)
        {
            RemoveFirstRotValue();
        }
    }

    //rewind objects rotation
    public void RewindRot()
    {
        // If there were no rotations to rewind exit function
        if (GetRotListCount() <= 0)
        {
            return;
        }

        // If counter not set then set it
        if (!m_rotCounterSet && GetRotListCount() > 0)
        {
            m_rotCounter = GetRotListCount() - 1;
            m_rotCounterSet = true; // Set the dirty flag to true
        }

        // If the current rotation value is the same as the one in the list at the counters point then decrease counter
        if (transform.rotation == GetRot(m_rotCounter) && m_rotCounter > 0)
        {
            m_rotCounter--;
        }

        // Transition of the objects rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, GetRot(m_rotCounter), Time.deltaTime * (m_rateOfChange * m_valueBoost));
    }

    // Reset the counters dirty flag
    public void ResetDirtyFlags()
    {
        m_rotCounterSet = false;
    }
}
