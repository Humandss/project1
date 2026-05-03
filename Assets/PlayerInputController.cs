using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerInputController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5.0f;

    private float attackDistance = 3.0f;
    private float attackDamage = 10f;

    
    private CharacterController characterController;
    private Vector3 vel;

    private InputAction moveAction;

    private InputAction attackAction;

    private Enemy enemy;
    

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");
        moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        attackAction = new InputAction("Attack", binding: "<Keyboard>/space");
        attackAction.performed += ctx => attack();
    }

    void OnEnable()
    {
        moveAction.Enable();
        attackAction.Enable();
    }

    void OnDisable()
    {
        attackAction.Disable();
        moveAction.Disable();
    }

    void Start()
    {
        enemy = GameObject.Find("Enemy").transform.GetComponent<Enemy>();
    }

    void attack()
    {
        if (enemy == null) return;   
        enemy.GetDamage(attackDamage);
        Debug.Log("Player attacked enemy, enemy hp = " + enemy.Hp);
    }

    
    // Update is called once per frame
    void Update()
    {
        // handle movement
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        vel = Vector3.zero;
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            vel = moveDirection * speed;
        }

        characterController.Move(vel * Time.deltaTime); 
    }
}
