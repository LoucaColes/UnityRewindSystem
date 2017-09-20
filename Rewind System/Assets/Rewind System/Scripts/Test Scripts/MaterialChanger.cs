using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour {

    public Material[] m_materials; // Array of materials that can be put onto an object
    private Material[] m_renderersMats; // Array of all materials that the renderer has
    public int m_matNum; // Index number for the material you want to change
    private int m_index, m_indexMax; // Index counter and Index limit
    public KeyCode m_nextMatKC, m_prevMatKC; // Keycodes for triggering the changes
    private Renderer m_renderer; // Objects renderer reference

	// Use this for initialization
	void Start () {
        m_renderer = gameObject.GetComponent<Renderer>(); // Get the objects renderer
        m_renderersMats = m_renderer.materials; // Get all the renderers materials and store in array
        m_index = 0; // Set the index to 0
        m_indexMax = m_materials.Length; // Set the index limit to the length of materials array
	}
	
	// Update is called once per frame
	void Update () {
        // Go to next material if keycode pressed and update index
	    if (Input.GetKeyDown(m_nextMatKC))
        {
            MoveIndexUp();
            UpdateMat();
        }
        // Go to previous material if keycode pressed and update index
        if (Input.GetKeyDown(m_prevMatKC))
        {
            MoveIndexDown();
            UpdateMat();
        }
	}

    // Updates the objects material and the renderers material array
    void UpdateMat()
    {
        m_renderersMats[m_matNum] = m_materials[m_index];
        m_renderer.materials = m_renderersMats;
    }

    // Move index value up one or if limit reached set back to 0
    void MoveIndexUp()
    {
        if (m_index < m_indexMax)
        {
            m_index++;
        }
        if (m_index >= m_indexMax)
        {
            m_index = 0;
        }
    }

    // Move index value down one or if reached lower than 0 then set to limit
    void MoveIndexDown()
    {
        if (m_index <= 0)
        {
            m_index = m_indexMax;
        }
        if (m_index > 0)
        {
            m_index--;
        }
    }
}
