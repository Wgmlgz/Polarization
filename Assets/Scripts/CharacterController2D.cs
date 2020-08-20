using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] bool invertFlip;
	[SerializeField] private float m_JumpForce = 800f;                                 // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .1f;         // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                              // Whether or not a player can steer while jumping;
	[SerializeField] public int maxJumpCount = 2;                                   // Jump count								
	[SerializeField] private LayerMask m_WhatIsGround;                             // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                             // A position marking where to check if the player is grounded.
	[SerializeField] public bool ignoreJump = false;
	[SerializeField] public bool doInvertGravity = false;

	[HideInInspector] const float k_GroundedRadius = .1f;  // Radius of the overlap circle to determine if grounded
	[HideInInspector] private bool m_Grounded;            // Whether or not the player is grounded.
	[HideInInspector] private Rigidbody2D m_Rigidbody2D;
	[HideInInspector] private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	[HideInInspector] private Vector3 m_Velocity = Vector3.zero;
	[HideInInspector] public int jumpsLeft = 0;
	[HideInInspector] bool wasGrounded = false;

	[Header("Dash")]
	public int maxDashes = 0;
	public float dashSpeed;
	public float dashTime;
	[HideInInspector] public int dashesLeft = 0;
	[HideInInspector] bool isDashing = false;
	[HideInInspector] private float dashTimeLeft;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public UnityEvent OnjumpEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	GameObject phantom;

	public void Jump(float force = 0f)
	{
		PlayerPrefs.SetInt("_game_jumps", PlayerPrefs.GetInt("_game_jumps") + 1);
		PlayerPrefs.SetInt("_lvl_jumps_" + SceneManager.GetActiveScene().name, PlayerPrefs.GetInt("_lvl_jumps_" + SceneManager.GetActiveScene().name) + 1);
		Vibration.VibratePop();
		if (ignoreJump == false)
		{
			//Stop falling
			Vector3 curVelocity = m_Rigidbody2D.velocity;
			curVelocity.y = 0;
			m_Rigidbody2D.velocity = curVelocity;

			//add jump force
			m_Rigidbody2D.AddRelativeForce(new Vector2(0f, force == 0f ? m_JumpForce : force));
		}
		
		if (doInvertGravity)
		{
			m_Rigidbody2D.gravityScale = m_Rigidbody2D.gravityScale * -1;
		}


		//effects
		AudioManager.AudioManager.m_instance.PlaySFX("Jump");
		OnjumpEvent.Invoke();
	}
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

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
				{
					OnLandEvent.Invoke();
					Vibration.VibrateNope();
					AudioManager.AudioManager.m_instance.PlaySFX("Land");
				}

				if (colliders[i].tag == "MovingPlatform")
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
		dashTimeLeft -= Time.fixedDeltaTime;
		if(dashTimeLeft < 0)
		{
			isDashing = false;
			GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "part").GetComponent<ParticleSystem>().Stop();
		}

		if (m_Grounded && wasGrounded)
		{
			jumpsLeft = maxJumpCount;
			dashesLeft = maxDashes;
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			if (isDashing)
			{
				move *= dashSpeed;
			}
			Vector3 targetVelocity = new Vector2(move * 10f, isDashing?0f:m_Rigidbody2D.velocity.y);
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
	public void Dash()
	{
		Vibration.VibratePeek();
		if (dashesLeft > 0 && !m_Grounded
			//&& (m_Rigidbody2D.velocity.x > 1f || m_Rigidbody2D.velocity.x < -1f)
			)
		{
			GetChildWithName(GameObject.FindGameObjectWithTag("Phantom"), "part").GetComponent<ParticleSystem>().Play();
			dashesLeft -= 1;
			isDashing = true;
			dashTimeLeft = dashTime;
			AudioManager.AudioManager.m_instance.PlaySFX("Dash");
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
	private void Start()
	{
		if (invertFlip)
		{
			phantom.GetComponent<Phantom>().flip = !phantom.GetComponent<Phantom>().flip;
		}
	}
	GameObject GetChildWithName(GameObject obj, string name)
	{
		Transform trans = obj.transform;
		Transform childTrans = trans.Find(name);
		if (childTrans != null)
		{
			return childTrans.gameObject;
		}
		else
		{
			return null;
		}
	}
}
