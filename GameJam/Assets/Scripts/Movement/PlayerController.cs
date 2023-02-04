using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 8f;
    [SerializeField] private SpriteRenderer spRenderer;
    
    //UTILIDAD
    private Vector2 axis = Vector2.zero;
    private Transform playerTrans;
    private byte zero = 0, one = 1;

    private void Awake()
    {
        playerTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        InputDetection();
        Move();
    }

    #region Methods

    void Move()
    {
        //Modifica la velocidad del cuerpo rigido del jugador para moverlo respectivamente
        rb.velocity = new Vector2(axis.x * speed, rb.velocity.y);
    }

    void InputDetection()
    {
        //Se asigna el valor de respectivo eje dependiendo del input en los ejes X y Y del jugador.
        axis.x = Input.GetAxisRaw("Horizontal");
        axis.y = Input.GetAxisRaw("Vertical");
        
        Flip(axis.x);
    }

    void Flip(float dir)
    {
        //Varia la direccion del sprite dependiendo de hacia a donde este mirando
        if (dir < zero)
        {
            spRenderer.flipX = true;
        }
        else if(dir > zero)
        {
            spRenderer.flipX = false;
        }
    }

    #endregion
}
