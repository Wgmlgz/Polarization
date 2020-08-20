using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 800f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .1f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] public int maxJumpCount = 2;                              // Jump count								
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private int jumpsLeft = 0;
	bool wasGrounded = false;




























































































	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	bool m_wasCrouching = false;
	GameObject phantom;

	private void Jump()
	{
		//Stop falling
		Vector3 curVelocity = m_Rigidbody2D.velocity;
		curVelocity.y = 0;
		m_Rigidbody2D.velocity = curVelocity;

		//add jump force
		m_Rigidbody2D.AddRelativeForce(new Vector2(0f, m_JumpForce));

		AudioManager.AudioManager.m_instance.PlaySFX("Jump");
	}
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		this.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
		phantom = GameObject.FindGameObjectWithTag("Phantom");
	}
	private void FixedUpdate()
	{
		wasGrounded = m_Grounded;
		m_Grounded = false;
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		bool onPlatform = false;
		GameObject platform;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject && colliders[i].isTrigger == false)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();

				if(colliders[i].tag == "MovingPlatform")
				{
					onPlatform = true;
					transform.parent = colliders[i].transform;
					phantom.transform.parent = colliders[i].transform;
				}
			}
		}
		if (onPlatform == false)
		{
			transform.parent = null;
			phantom.transform.parent = null;
		}
	}
	public void Move(float move, bool crouch, bool jump)
	{
		if (m_Grounded && wasGrounded)
		{
			jumpsLeft = maxJumpCount;
		}

		if (!crouch)
		{
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (jump && (jumpsLeft != 0))
		{
			--jumpsLeft;

			Jump();

			m_Grounded = false;
		}
	}
	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		phantom.GetComponent<Phantom>().flip = !phantom.GetComponent<Phantom>().flip;
	}
}
