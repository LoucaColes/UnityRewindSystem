using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour {

    public float moveValue; // How much something moves
    public bool m_in2D; // Allows to switch between types of dimensions
    private Rigidbody m_rigidbody; // 3D Rigidbody reference
    private Rigidbody2D m_rigidbody2D; // 2D Rigidbody reference
    public RewindManager rewindManager; // Reference to the rewind manager script

	// Use this for initialization
	void Start () {
        // Get the rigidbody references
        if (!m_in2D)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
        else
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        // If the rewind manager is recording then set isKinematic to false
        // And allow gravity to be used and allow object to move
        if (rewindManager.GetMode() == RewindManager.Mode.Record)
        {
            if (!m_in2D)
            {
                m_rigidbody.isKinematic = false;
                m_rigidbody.useGravity = true;
            }
            else
            {
                m_rigidbody2D.isKinematic = false;
                m_rigidbody2D.gravityScale = 1.0f;
            }

            Movement();
        }
        
        // If the rewind manager is rewinding then set isKinematic to true
        // And turn off gravity and don't allow movement
        else if (rewindManager.GetMode() == RewindManager.Mode.Rewind)
        {
            if (!m_in2D)
            {
                m_rigidbody.isKinematic = true;
                m_rigidbody.useGravity = false;
            }
            else
            {
                m_rigidbody2D.isKinematic = true;
                m_rigidbody2D.gravityScale = 0.0f;
            }
        }
	}

    // Simple movement by adding to the rigidbodys velocity
    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!m_in2D)
            {
                m_rigidbody.velocity += new Vector3(0.0f, 0.0f, moveValue);
            }
            else
            {
                m_rigidbody2D.velocity += new Vector2(0.0f, moveValue);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!m_in2D)
            {
                m_rigidbody.velocity += new Vector3(0.0f, 0.0f, -moveValue);
            }
            else
            {
                m_rigidbody2D.velocity += new Vector2(0.0f, -moveValue);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!m_in2D)
            {
                m_rigidbody.velocity += new Vector3(-moveValue, 0.0f, 0.0f);
            }
            else
            {
                m_rigidbody2D.velocity += new Vector2(-moveValue, 0.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!m_in2D)
            {
                m_rigidbody.velocity += new Vector3(moveValue, 0.0f, 0.0f);
            }
            else
            {
                m_rigidbody2D.velocity += new Vector2(moveValue, 0.0f);
            }
        }
    }
}
