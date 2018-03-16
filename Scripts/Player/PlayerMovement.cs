using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	Vector3 movement;                   // The vector to store the direction of the player's movement.     
	Animator anim;                      // Reference to the animator component.     
	//Rigidbody playerRigidbody;          // Reference to the player's rigidbody.     
	//int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.     
	//float camRayLength = 100f;          // The length of the ray from the camera into the scene. 
    public float speed = 35;
    public Camera mainCamera;
    private Vector3 offset;
    private Unit playerUnit;
    bool walking = false;
    // Use this for initialization
    void Awake () 
	{
        playerUnit = GetComponent<Unit>();
		// Create a layer mask for the floor layer.         
		//floorMask = LayerMask.GetMask ("Floor");         

		// Set up references.               
		//playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Animating(0,v);
    }

    private void Update()
    {
        walking = false;
        speed = 35;

        if (playerUnit.getCombatStatus() == Unit.CombatStatus.COMBAT_STATUS_ATTACKING)
            return;

        if (ConfigController.firstDialog)
            return;

        if (playerUnit.getCombatStatus() == Unit.CombatStatus.COMBAT_STATUS_DEFENDING)
            speed /= 2;



        if (ConfigController.inCinematic)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            walking = true;
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            walking = true;
            transform.position += -transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.E))
        {
            walking = true;
            transform.position += transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            walking = true;
            transform.position += -transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            walking = true;
            transform.RotateAround(transform.position, Vector3.up, -200 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            walking = true;
            transform.RotateAround(transform.position, Vector3.up, 200 * Time.deltaTime);
        }
    }

    void Move(float h, float v)
	{
        float x = Mathf.Cos(transform.rotation.eulerAngles.y * 2 * Mathf.PI / 180);
        float z = Mathf.Sin(transform.rotation.eulerAngles.y * 2 * Mathf.PI / 180);

        Debug.Log("El rotation y vale:" + transform.rotation.eulerAngles.y);
        Debug.Log("EL x vale: " + x);
        Debug.Log("EL z vale: " + z);

        movement.Set (x, 0f, z);
        //movement = movement.normalized;
        //playerRigidbody.MovePosition (transform.position + movement);
        transform.position +=  transform.forward  * 3;
    }

	void Animating (float h, float v)
	{
        Unit me = gameObject.GetComponent<Unit>();
        if (walking)
            me.setCurrentAnimation("run");
        if (!walking && me.getCombatStatus() != Unit.CombatStatus.COMBAT_STATUS_ATTACKING)
            me.setCurrentAnimation("idle");
    }
}

