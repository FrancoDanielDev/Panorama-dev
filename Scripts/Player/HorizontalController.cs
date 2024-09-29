using UnityEngine;

public class HorizontalController : PlayerControllers
{
    private Vector3 _movementVector;

    public HorizontalController(Player player) : base(player)
    {
    }

    public override void Start()
    {
        Input.ResetInputAxes();
        player.transform.right = Vector3.right * Mathf.Sign(player.transform.right.x);
    }

    public override void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        _movementVector = Vector3.right * (Input.GetAxis("Horizontal") * player.Speed * Time.deltaTime);

        if (_movementVector.magnitude != 0)
        {
            player.transform.right = _movementVector;
        }
       
        player.Body.MovePosition(player.Body.position + _movementVector);

        player.animator.SetBool("Walking", Input.GetAxis("Horizontal") != 0);
    }

    private void Jump()
    {
        if (!player.isGrounded)
        {
            // Fisica de caida regulada.
            if (player.Body.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                player.Body.velocity += Physics.gravity.y * player.LowJumpMultiplier * Time.deltaTime * Vector3.up;
            }
            else
            {
                player.Body.velocity += Physics.gravity.y * player.FallMultiplier * Time.deltaTime * Vector3.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // Salta.
            player.Body.velocity = Vector3.up * player.JumpForce;
            AudioManager.instance.Play("Jump");
        }
    }
}
