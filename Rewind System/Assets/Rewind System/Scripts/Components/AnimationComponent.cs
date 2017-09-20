using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationComponent : MonoBehaviour {

    [SerializeField] private List<AnimationState> m_recordedAnim; // List of animation states
    private AnimationState m_lastAnim; // The last animation state
    private Animator m_animator; // Reference to the objects animator
    private int m_animCounter; // Position in the list when rewinding
    public int m_frameCount = 60; // Amount of frames recorded

    // Use this for initialization
    void Start () {
        m_recordedAnim = new List<AnimationState>(); // Create a new list
        m_animator = gameObject.GetComponent<Animator>(); // Get a reference to the animator
    }

    // Not all of these function ever get used but will be needed for further development

    // Reset the components data
    public void ResetData()
    {
        m_recordedAnim.Clear();
    }

    // Add a new animation state
    public void AddAnim(AnimationState _newAnim)
    {
        m_recordedAnim.Add(_newAnim);
    }

    // Get a specific animation state using a index value
    public AnimationState GetAnim(int _index)
    {
        return m_recordedAnim[_index];
    }

    // Get the lists count value
    public int GetAnimListCount()
    {
        return m_recordedAnim.Count;
    }

    // Remove the lists first value
    public void RemoveFirstAnimValue()
    {
        m_recordedAnim.RemoveAt(0);
    }

    // Record objects animation
    public void RecordAnim()
    {
        m_animator.StopPlayback(); // Stop any playback of the current animation
        m_animator.StartRecording(m_frameCount); // Record the animator. 
        m_animator.speed = 1.0f; // Set the animation speed to 1 (1 being normal speed)
    }

    // Rewind objects animation
    public void RewindAnim()
    {
        m_animator.StopRecording(); // Stop the animator recording
        m_animator.StartPlayback(); // Start the playback of the recording
        m_animator.speed = -1.0f; // Set the animation speed to -1 (-1 allowing the animation to seem like its being reversed)
    }

}
