using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RewindManager : MonoBehaviour {

    public List<GameObject> m_rewindableGameObjects; // List of all rewindable objects
    public float m_timeToRewind; // Time that system rewinds for
    public int m_recordLimit; // How much data is recorded
    public enum Mode { Rewind, Record, Reset }; // Systems modes
    public Mode mode = Mode.Record; // Systems current mode
    private float m_stopwatch; // Stopwatch to make sure only x seconds are rewound
    public Text m_stateText; // Reference to a text object to display the current mode of the system
    public KeyCode m_testKey = KeyCode.Alpha1; // Key to test the system if no other trigger available

    // Use this for initialization
    void Start () {
        m_stopwatch = 0; // Set the stopwatch to 0

        // For all rewindable objects set their record limit
        foreach (GameObject rewindable in m_rewindableGameObjects)
        {
            rewindable.GetComponent<Rewindable>().SetRecordLimit(m_recordLimit);
        }

        // Set the mode text and color
        m_stateText.text = "State: Record";
        m_stateText.color = Color.red;
    }
	
	// Update is called once per frame
	void Update () {

        // If test key pressed set mode to rewind
        if (Input.GetKeyDown(m_testKey))
        {
            mode = Mode.Rewind;
        }

        // If in rewind mode start rewind system
        if (mode == Mode.Rewind)
        {
            // Set the mode text to rewind and change its color
            m_stateText.text = "State: Rewind";
            m_stateText.color = Color.green;

            // Add time to the stopwatch
            m_stopwatch += Time.deltaTime;

            // If system has rewound past the time set the mode to reset
            if (m_stopwatch > m_timeToRewind)
            {
                print("Time Up");
                mode = Mode.Reset;
            }
            else // Otherwise continue rewinding
            {
                Rewind();
            }
        }
        // If the mode is equal to record then start the record system
        else if (mode == Mode.Record)
        {
            // For all rewindable objects set the record limit
            foreach (GameObject rewindable in m_rewindableGameObjects)
            {
                rewindable.GetComponent<Rewindable>().SetRecordLimit(m_recordLimit);
            }

            // Set the state text to record and change the color
            m_stateText.text = "State: Record";
            m_stateText.color = Color.red;

            // Call record function
            Record();
        }
        // If mode is in reset then reset all data
        else if (mode == Mode.Reset)
        {
            // Set state text to reset and change the color
            m_stateText.text = "State: Reset";
            m_stateText.color = Color.white;
            ResetData(); // Call the reset data function
        }
	}

    // Rewind System
    void Rewind()
    {
        // Loop through each rewindable object and rewind specific traits
        for (int i = 0; i < m_rewindableGameObjects.Count; i++)
        {
            m_rewindableGameObjects[i].GetComponent<Rewind>().RewindData();
        }
    }

    // Record System
    void Record()
    {
        // Loop through each rewindable object and record specific traits
        foreach (GameObject rewindable in m_rewindableGameObjects)
        {
            rewindable.GetComponent<Record>().RecordData();
        }
    }

    // Reset rewind managers data
    void ResetData()
    {
        m_stopwatch = 0; // Set stopwatch to 0

        // Loop through all objects and reset their data and dirty flags
        foreach (GameObject rewindable in m_rewindableGameObjects)
        {
            rewindable.GetComponent<Rewindable>().ResetData();
            rewindable.GetComponent<Rewind>().ResetDirtyFlags();
        }

        // Set mode to record
        mode = Mode.Record;
    }

    // Set the managers current mode
    public void SetMode(Mode _newMode)
    {
        mode = _newMode;
    }

    // Get the managers current mode
    public Mode GetMode()
    {
        return mode;
    }
}
