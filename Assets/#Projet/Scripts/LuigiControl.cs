using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class LuigiControl : MonoBehaviour
{

    private InputAction xAxis;
    private InputAction jump;
    public InputActionAsset actions;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 300f;
    private Rigidbody2D player;
    private SpriteRenderer spriteLuigi;
    private Animator animator;
    private bool isJumping = false;
    private bool isCrouching = false;
    private BoxCollider2D boxLuigi;



    void Awake()
    {
        xAxis = actions.FindActionMap("LuigiMap").FindAction("XAxis");
        player = GetComponent<Rigidbody2D>();
        spriteLuigi = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxLuigi = GetComponent<BoxCollider2D>();
    }


    void OnEnable()
    {
        actions.FindActionMap("LuigiMap").Enable();
        actions.FindActionMap("LuigiMap").FindAction("Jump").performed += onJump; // = listener
        actions.FindActionMap("LuigiMap").FindAction("Crouch").performed += Crouch;
        actions.FindActionMap("LuigiMap").FindAction("Crouch").canceled += onRise; 
    }


    void OnDisable()
    {
        actions.FindActionMap("LuigiMap").Disable();
        actions.FindActionMap("LuigiMap").FindAction("Jump").performed -= onJump;
        actions.FindActionMap("LuigiMap").FindAction("Crouch").performed -= Crouch;
        actions.FindActionMap("LuigiMap").FindAction("Crouch").canceled += onRise;
    }


    // Update is called once per frame
    void Update()
    {
        MoveX();

        if (isJumping)
        {
            if (player.linearVelocityY < 0f)
            {
                isJumping = false;
                animator.SetBool("onJump", false);
            }
        }



    }

    private void MoveX()
    {
        float xMove = xAxis.ReadValue<float>();
        spriteLuigi.flipX = xMove < 0;

        if (isCrouching) return; // permet de faire en sorte que quand il est en Crouch, il ne peut pas marcher à gauche ou droite (reste bloqué en Crouch)

        transform.Translate((xMove) * speed * Time.deltaTime, 0f, 0f);
        animator.SetFloat("Speed", Mathf.Abs(xMove)); // pour passer un paramètre d'animation
                                                      //transform.position += speed * Time.deltaTime * xMove * transform.right;


    }

    private void onJump(InputAction.CallbackContext context)
    {
        player.AddForce(Vector3.up * jumpForce);
        animator.SetBool("onJump", true);
        isJumping = true;
    }

    private void Crouch(InputAction.CallbackContext context)
    {

        isCrouching = true;
        animator.SetBool("onCrouch", true);
    }

    private void onRise(InputAction.CallbackContext context)
    {
        isCrouching = false;
        animator.SetBool("onCrouch", false);
    }



}
