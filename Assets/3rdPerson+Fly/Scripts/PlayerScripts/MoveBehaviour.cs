using UnityEngine;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviour : GenericBehaviour
{
	public float walkSpeed = 0.15f;                 // Default walk speed.
	public float runSpeed = 1.0f;                   // Default run speed.
	public float sprintSpeed = 1.5f;                // Default sprint speed.
	public float speedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.
	public string jumpButton = "Jump";              // Default jump button.
	public float jumpHeight = 0.00f;                 // Default jump height.
	public float jumpIntertialForce = 10f;          // Default horizontal inertial force when jumping.

	private float speed, speedSeeker;               // Moving speed.
	private int jumpBool;                           // Animator variable related to jumping.
	private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
	private int squatStopBool;						// 是否下蹲结束
	private int squatStartBool;						// 开始下蹲
	public bool GameMode = true;					// 跳一跳游戏状态
	public float gamePower;						// 蓄力大小，最大为1.5，最小为0.5
	public float maxPower = 1.5f;
	public bool jump;                              // Boolean to determine whether or not the player started a jump.
	private bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.
	private AnimatorStateInfo stateInfo;
	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		jumpBool = Animator.StringToHash("Jump");
		groundedBool = Animator.StringToHash("Grounded");
		squatStopBool = Animator.StringToHash("SquatStop");
		squatStartBool = Animator.StringToHash("SquatStart");
		behaviourManager.GetAnim.SetBool(groundedBool, true);
		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour(this);
		behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
		speedSeeker = runSpeed;
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		// Get jump input.
		if (!jump && Input.GetButtonDown(jumpButton) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			jump = true;
		}
	}

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
		// Call the basic movement manager.
		MovementManagement(behaviourManager.GetH, behaviourManager.GetV);

		// Call the jump manager.
		JumpManagement();
	}

	// Execute the idle and walk/run jump movements.
	void JumpManagement()
	{
		// Start a new jump.
		if (jump && !behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.IsGrounded())
		{
			// Set jump related parameters.
			
			// Is a locomotion jump?
			if (behaviourManager.GetAnim.GetFloat(speedFloat) > 0.001) //助跑跳
			{
				behaviourManager.LockTempBehaviour(this.behaviourCode);
				behaviourManager.GetAnim.SetBool(jumpBool, true);
				// Temporarily change player friction to pass through obstacles.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0f;
				// Remove vertical velocity to avoid "super jumps" on slope ends.
				RemoveVerticalVelocity();
				// Set jump vertical impulse velocity.
				float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
				print(Physics.gravity.y);
				velocity = Mathf.Sqrt(velocity);
				behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
			}
			if(behaviourManager.GetAnim.GetFloat(speedFloat) <= 0.001){ //原地跳
				//print(111);
				behaviourManager.LockTempBehaviour(this.behaviourCode);
				stateInfo = behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0);
				bool stopSquat = false; //是否下蹲结束的临时变量
				if(GameMode){
					if(Input.GetKey(KeyCode.Space)){
						gamePower += 0.05f;
					}else{
						stopSquat = true;
						if(gamePower <= 0.5f){
							
							jump = false;
							stopSquat = false;
							gamePower = 0.0f;
						}
						gamePower = Mathf.Min(gamePower,maxPower);
					}
				}else{
					if(stateInfo.normalizedTime >= 1.0 && stateInfo.IsName("Squat")){
						stopSquat = true;
					}
				}
				if(stopSquat){ //站着跳的
					behaviourManager.LockTempBehaviour(this.behaviourCode);
					behaviourManager.GetAnim.SetBool(squatStopBool,true);
					behaviourManager.GetAnim.SetBool(squatStartBool,false);
					behaviourManager.GetAnim.SetBool(jumpBool,true);
					// Temporarily change player friction to pass through obstacles.
					GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
					GetComponent<CapsuleCollider>().material.staticFriction = 0f;
					// Remove vertical velocity to avoid "super jumps" on slope ends.
					RemoveVerticalVelocity();
					// Set jump vertical impulse velocity.
					float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
					velocity = Mathf.Sqrt(velocity);
					if(GameMode)behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity * gamePower, ForceMode.VelocityChange);
					else {
						behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity * jumpHeight, ForceMode.VelocityChange);
					}
				} //判断动画播放完成
				else{
					if(jump){
						behaviourManager.GetAnim.SetBool(squatStopBool,false); //判断是否要跳，如果跳起则启动jump动画
						behaviourManager.GetAnim.SetBool(squatStartBool,true);
					}else{
						behaviourManager.GetAnim.SetBool(squatStopBool,false); //如果没跳起来（蓄力太短）则不启动动画
						behaviourManager.GetAnim.SetBool(squatStartBool,false);
					}
				}
			}
		}
		// Is already jumping?
		else if (behaviourManager.GetAnim.GetBool(jumpBool))
		{
			// Keep forward movement while in the air.
			if (!behaviourManager.IsGrounded() && !isColliding && behaviourManager.GetTempLockStatus())
			{
				if(!GameMode)behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce * Physics.gravity.magnitude * sprintSpeed, ForceMode.Acceleration);
				else behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce * Physics.gravity.magnitude * gamePower, ForceMode.Acceleration);
			}
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				jump = false;
				behaviourManager.GetAnim.SetBool(jumpBool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
				if(GameMode) gamePower = 0.0f;
			}
		}
	}

	// Deal with the basic player movement
	void MovementManagement(float horizontal, float vertical)
	{
		// On ground, obey gravity.
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;

		// Avoid takeoff when reached a slope end.
		else if (!behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.GetRigidBody.velocity.y > 0)
		{
			RemoveVerticalVelocity();
		}

		// Call function that deals with player orientation.
		Rotating(horizontal, vertical);

		// Set proper speed.
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		// This is for PC only, gamepads control speed via analog stick.
		speedSeeker += Input.GetAxis("Mouse ScrollWheel");
		speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
		speed *= speedSeeker;
		if (behaviourManager.IsSprinting())
		{
			speed = sprintSpeed;
		}

		if(!GameMode)behaviourManager.GetAnim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
	}

	// Remove vertical rigidbody velocity.
	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = behaviourManager.GetRigidBody.velocity;
		horizontalVelocity.y = 0;
		behaviourManager.GetRigidBody.velocity = horizontalVelocity;
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)
	{
		// Get camera forward direction, without vertical component.
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		// Lerp current direction to calculated target direction.
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

	// Collision detection.
	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
		// Slide on vertical obstacles
		if (behaviourManager.IsCurrentBehaviour(this.GetBehaviourCode()) && collision.GetContact(0).normal.y <= 0.1f)
		{
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		isColliding = false;
		GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
		GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
	}
}
