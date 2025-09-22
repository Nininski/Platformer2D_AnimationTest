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

        Vector3 mvt = speed * (goRight ? 1f : -1f) * Time.deltaTime * transform.right;
        transform.Translate(mvt);
        Vector3 origin = transform.position + 0.4f * Vector3.up * 0.4f + Vector3.right * 0.4f * (goRight ? 1f : -1f);
        Vector3 direction = Vector3.right * (goRight ? 1f : -1f);

        Debug.DrawRay(origin, direction, Color.cyan);
        RaycastHit2D sideHit = Physics2D.Raycast(origin, direction, 0.02f); // si on ne touche rien, le hit sera null + 0.2f correspond à la distance

        origin = transform.position + Vector3.right * 0.4f * (goRight ? 1f : -1f);
        direction = Vector3.down;
        Debug.DrawRay(origin,direction, Color.red); // donne le point de départ de notre ray
        RaycastHit2D belowHit = Physics2D.Raycast(origin, direction,  1.02f);

        
        if (sideHit.collider != null)
        {
            InverseSpeed();
        }
        
        if (belowHit.collider == null)
        {
            InverseSpeed();
        } 
        

    }


    private void InverseSpeed()
    {
        goRight = !goRight;
        spriteSpiny.flipX = !spriteSpiny.flipX;
    }


}
