using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spRenderer;
    [SerializeField] private Rigidbody2D rb;
    [Space]
    [SerializeField] private float speed = 8f, jumpForce = 16f, jumpForcePressed = 0.5f, dashForce = 24f, dashCd = 1f;
    [Header("Checkers")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform rightCheck;
    [SerializeField] private Transform leftCheck;
    [Space]
    [SerializeField] private LayerMask groudLayer;
    [Space] 
    [SerializeField] private Animator anim;
    [Space] 
    [SerializeField] private GameObject trail;
    [Space] 
    public UnityEvent TurretModeToggle;
    
    public bool isturret { get; private set; }
    
    //UTILIDAD
    private Vector2 axis = Vector2.zero;
    private byte zero = 0;
    private float groundCollRad = 0.2f, dashTime = 0.2f, originalGravity = 1f;
    private bool isDashing = false, canDash = true;

    private void Awake()
    {
        originalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        InputDetection();
        
        if (!isDashing && !HorizontallyBlocked())
        {
            TurretMode();
            
            if (!isturret)
            {
                Move();
                Jump();
            }
        }
        
        anim.SetFloat("Horizontal", Math.Abs(Input.GetAxisRaw("Horizontal")));
        anim.SetBool("Grounded", IsGrounded());
        anim.SetBool("Turret", isturret);
    }

    #region Methods

    void Move()
    {
        //Modifica la velocidad del cuerpo rigido del jugador para moverlo respectivamente
        rb.velocity = new Vector2(axis.x * speed, rb.velocity.y);
        
        Dash();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > zero)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpForcePressed);
        }
    }

    void Dash()
    {
        //Desplazamiento lateral de caracter instantaneo y veloz para el jugador
        if (Input.GetButtonDown("Dash") && axis.x != zero && canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0;
            anim.SetTrigger("Dash");
            trail.SetActive(true);
            rb.velocity = new Vector2(dashForce * axis.x, 0);
            Invoke("DashReset", dashTime);
            Invoke("DashCd", dashCd);
        }
    }

    void TurretMode()
    {
        if (Input.GetButtonDown("Turret"))
        {
            isturret = !isturret;
            TurretModeToggle.Invoke();
        }
    }
    
    void InputDetection()
    {
        //Se asigna el valor de respectivo eje dependiendo del input en los ejes X y Y del jugador.
        axis.x = Input.GetAxisRaw("Horizontal");
        axis.y = Input.GetAxisRaw("Vertical");

        if (!isturret)
        {
            Flip(axis.x);
        }
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
    
    bool IsGrounded()
    {
        //Revisa la permanencia del jugador 
        return Physics2D.OverlapCircle(groundCheck.position, groundCollRad, groudLayer);
    }

    bool HorizontallyBlocked()
    {
        //Revisa el bloqueo horizontal del jugador al estar en el aire
        if ((Physics2D.OverlapCircle(rightCheck.position, groundCollRad*2, groudLayer) ||
             Physics2D.OverlapCircle(leftCheck.position, groundCollRad*2, groudLayer)) &&
            !IsGrounded())
        {
            return true;
        }

        return false;
    }

    void DashReset()
    {
        //Restablece las caracteristicas iniciales de la gravedad y marca el fin del estado "Dashing"
        isDashing = false;
        rb.gravityScale = originalGravity;
        anim.ResetTrigger("Dash");
        Invoke("TrailNotVisible", 0.5f);
    }

    void DashCd()
    {
        //Determina la disponibilidad de la mecanica "Dash"
        canDash = true;
    }

    void TrailNotVisible()
    {
        trail.SetActive(false);
    }
    
    #endregion

    #region Editor

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundCollRad);
        Gizmos.DrawSphere(rightCheck.position, groundCollRad*2);
        Gizmos.DrawSphere(leftCheck.position, groundCollRad*2);
    }
    
    #endregion
}
