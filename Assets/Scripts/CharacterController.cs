using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    private float speed = 4;
    public LayerMask collisionsLayers;
    public FieldOfView fov;

    private Vector3 targetPosition;
    private Animator animator;
    private new Rigidbody2D rigidbody;
    private Vector2 direction;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsMovementActive())
        {
            GestionarMovimiento();
            GestionarOrientacion();

            fov.SetOrigin(transform.position);
        }
    }

    void GestionarMovimiento() 
    {
        if(Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            targetPosition.z = transform.position.z;

            direction = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
            direction.Normalize();

            Vector2 velocity = direction * speed;

            rigidbody.velocity = velocity;

            animator.SetBool("isMoving", true);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }

    void GestionarOrientacion()
    {
        transform.localScale = new Vector2(direction.x > 0 ? 1 : -1, transform.localScale.y);
    }

    bool IsMovementActive()
    {
        return !GameObject.FindWithTag("Task");
    }
}