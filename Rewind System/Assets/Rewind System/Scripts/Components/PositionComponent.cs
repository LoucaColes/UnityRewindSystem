using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionComponent : MonoBehaviour {

    [SerializeField] private List<Vector3> m_recordedPos; //Stores the recorded data
    [SerializeField] private int m_recordLimit; // How much data is recorded
    private Vector3 m_lastPos; // Last position value that was recorded
    private float m_posTimer; // Timer
    [SerializeField] private float m_timeInterval; // The frames that each data is recorded at e.g. one position every 0.5 frames
    [SerializeField] private float m_rateOfChange; // How fast the transition from position to position
    private int m_posCounter; // Position within the list when rewinding
    private bool m_posCounterSet; // Dirty flag so counter only gets set once
    public float m_valueBoost; // Used to boost the rate of change

    // Use this for initialization
    void Start()
    {
        m_recordedPos = new List<Vector3>(); //Create the list
        m_posTimer = 0.0f; // Set timer to 0
        m_posCounterSet = false; // Set dirty flag to false
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
        m_recordedPos.Clear();
    }

    // Add a position value to the list
    public void AddPos(Vector3 _newRot)
    {
        m_recordedPos.Add(_newRot);
    }

    // Get a position value from the list at a specific index
    public Vector3 GetPos(int _index)
    {
        return m_recordedPos[_index];
    }

    // Get the count value of the list
    public int GetPosListCount()
    {
        return m_recordedPos.Count;
    }

    // Remove the first value in the list
    public void RemoveFirstPosValue()
    {
        m_recordedPos.RemoveAt(0);
    }

    //record objects position
    public void RecordPos()
    {
        // If the timer has reached the right frame count record a value
        // If time interval is set to 0 it will record every frame
        if (m_posTimer > m_timeInterval)
        {
            // If the position is not the same as the last one add it to the list
            // And store the new position as the last
            if (gameObject.transform.position != m_lastPos)
            {
                AddPos(gameObject.transform.position);
                m_lastPos = gameObject.transform.position;
            }
            m_posTimer = 0; // Reset the timer
        }

        m_posTimer += Time.deltaTime; // Increase the timer value

        // If the count is greater than the record limit remove the first value
        if (GetPosListCount() > m_recordLimit)
        {
            RemoveFirstPosValue();
        }
    }

    //rewind objects rotation
    public void RewindPos()
    {
        // If there were no positions to rewind exit function
        if (GetPosListCount() <= 0)
        {
            return;
        }

        // If counter not set then set it
        if (!m_posCounterSet && GetPosListCount() > 0)
        {
            m_posCounter = GetPosListCount() - 1;
            m_posCounterSet = true; // Set the dirty flag to true
        }

        // If the current position value is the same as the one in the list at the counters point then decrease counter
        if (transform.position == GetPos(m_posCounter) && m_posCounter > 0)
        {
            m_posCounter--;
        }

        // Transition of the objects position
        transform.position = Vector3.MoveTowards(transform.position, GetPos(m_posCounter), Time.deltaTime * (m_rateOfChange * m_valueBoost));
    }

    // Reset the counters dirty flag
    public void ResetDirtyFlags()
    {
        m_posCounterSet = false;
    }
}
