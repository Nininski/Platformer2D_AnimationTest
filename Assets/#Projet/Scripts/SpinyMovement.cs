using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SpinyMovement : MonoBehaviour
{
    
    
    [SerializeField] private float speed = 1f;
    [SerializeField] bool goRight = true; 
    SpriteRenderer spriteSpiny;

    

    void Start()
    {
        spriteSpiny  = GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        
        Vector3 mvt = speed * (goRight? 1f : -1f) * Time.deltaTime * transform.right;
        transform.Translate(mvt);
        //transform.position -= speed * Time.deltaTime * transform.right;



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            InverseSpeed();
            spriteSpiny.flipX = !spriteSpiny.flipX;
        }

    }

    private void InverseSpeed()
    {
        goRight = !goRight;
    }


}
