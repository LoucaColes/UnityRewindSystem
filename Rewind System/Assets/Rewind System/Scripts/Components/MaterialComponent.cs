using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialComponent : MonoBehaviour {

    [SerializeField] private List<Material> m_recordedMat; // List of all recorded materials
    [SerializeField] private float m_recordLimit; // Limit of data that is recorded
    private Material m_lastMat; // Last material to be recorded
    private float m_matTimer; // Timer
    [SerializeField] private float m_timeInterval; // The frames that each data is recorded at e.g. one material every 0.5 frames
    private Renderer m_renderer; // Reference to the renderer
    [SerializeField] private float m_rateOfChange; // How fast the transition from material to material
    private int m_matCounter; // Position within the list when rewinding
    private bool m_matCounterSet; // Dirty flag so counter only gets set once

    // Use this for initialization
    void Start () {
        m_recordedMat = new List<Material>(); // Create the list
        m_matTimer = 0.0f; // Set the timer to 0
        m_renderer = gameObject.GetComponent<Renderer>(); // Get access to the renderer
        m_matCounterSet = false; // Set the dirty flag to false
    }

    // Clears the lists data
    public void ResetData()
    {
        m_recordedMat.Clear();
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

    // Add a material value to the list
    public void AddMat(Material _newMat)
    {
        m_recordedMat.Add(_newMat);
    }

    // Get a material value from the list at a specific index
    public Material GetMat(int _index)
    {
        return m_recordedMat[_index];
    }

    // Get the count value of the list
    public int GetMatListCount()
    {
        return m_recordedMat.Count;
    }

    // Remove the first value in the list
    public void RemoveFirstMatValue()
    {
        m_recordedMat.RemoveAt(0);
    }

    // Record objects material
    public void RecordMat()
    {
        // If the timer has reached the right frame count record a value
        // If time interval is set to 0 it will record every frame
        if (m_matTimer > m_timeInterval)
        {
            if (m_renderer.material != m_lastMat)
            {
                AddMat(m_renderer.material);
                m_lastMat = m_renderer.material;
            }
            m_matTimer = 0; // Reset the timer
        }

        m_matTimer += Time.deltaTime; // Increase timer value

        // If the count is greater than the record limit remove the first value
        if (GetMatListCount() > m_recordLimit)
        {
            RemoveFirstMatValue();
        }
    }

    //rewind objects material
    public void RewindMat()
    {
        // If there were no materials to rewind exit function
        if (GetMatListCount() <= 0)
        {
            return;
        }

        // If counter not set then set it
        if (!m_matCounterSet && GetMatListCount() > 0)
        {
            m_matCounter = GetMatListCount() - 1;
            m_matCounterSet = true; // Set the dirty flag to true
        }

        // If the current material value is the same as the one in the list at the counters point then decrease counter
        if (transform.GetComponent<Renderer>().material.color == GetMat(m_matCounter).color && m_matCounter > 0)
        {   
            m_matCounter--;
        }

        // Transition of the objects material
        m_renderer.material.Lerp(m_renderer.material, GetMat(m_matCounter), m_rateOfChange / 10);
    }

    // Reset the componets dirty flag
    public void ResetDirtyFlags()
    {
        m_matCounterSet = false;
    }
}
