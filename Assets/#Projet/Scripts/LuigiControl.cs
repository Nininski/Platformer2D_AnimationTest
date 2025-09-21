using UnityEngine;
using UnityEngine.InputSystem;

public class LuigiControl : MonoBehaviour
{

    private InputAction xAxis;
    private InputAction jump;
    public InputActionAsset actions;

    [SerializeField] private float speed = 1.1f;
    private float jumpForce = 300f;
    Rigidbody2D player;
    SpriteRenderer spriteLuigi;


    void Awake()
    {
        xAxis = actions.FindActionMap("LuigiMap").FindAction("XAxis");
        jump = actions.FindActionMap("LuigiMap").FindAction("Jump");
        player = GetComponent<Rigidbody2D>();
        spriteLuigi  = GetComponent<SpriteRenderer>();
    }
    

    void OnEnable()
    {
        actions.FindActionMap("LuigiMap").Enable();
        jump.performed += ctx => Jump();
    }


    void OnDisable()
    {
        actions.FindActionMap("LuigiMap").Disable();
        jump.performed -= ctx => Jump();  
    }


    // Update is called once per frame
    void Update()
    {
        MoveX();
        
    }

         private void MoveX()
    {
        float xMove = xAxis.ReadValue<float>();
        transform.position += speed * Time.deltaTime * xMove * transform.right;

    }

    private void Jump()
    {
        player.AddForce(Vector3.up * jumpForce);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            
            spriteLuigi.flipX = !spriteLuigi.flipX;
        }

    }
}
