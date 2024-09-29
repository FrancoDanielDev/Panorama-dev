using UnityEngine;

public class VerticalController : PlayerControllers
{
    public VerticalController(Player player) : base(player)
    {
    }

    public override void Start()
    {
        player.Body.velocity = Vector3.zero;
        Input.ResetInputAxes();
    }

    public override void Update()
    {
        // Direccion de movimiento.
        var desiredDirection = new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            z = Input.GetAxis("Vertical")
        };
        if (desiredDirection.magnitude > 1) desiredDirection.Normalize();
        // ========================

        // Direccionar la vista del personaje.
        var onMovement = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        if (onMovement) player.transform.right = desiredDirection;
        // ===================================

        // Evita que se mueva al haber un borde.
        var distanceX = Mathf.Ceil(Mathf.Abs(desiredDirection.x)) * Mathf.Sign(desiredDirection.x);
        var distanceZ = Mathf.Ceil(Mathf.Abs(desiredDirection.z)) * Mathf.Sign(desiredDirection.z);
        var edgeX = CheckEdges.Check(player.transform.position + Vector3.right * distanceX, 1.5f);
        var edgeZ = CheckEdges.Check(player.transform.position + Vector3.forward * distanceZ, 1.5f);
        if (edgeX) desiredDirection.x = 0;
        if (edgeZ) desiredDirection.z = 0;
        // =====================================

        // Movimiento del cuerpo.
        var movementVector = player.Body.position + player.Speed * Time.deltaTime * desiredDirection;
        player.Body.MovePosition(movementVector);
        // ======================

        player.animator.SetBool("Walking", desiredDirection.magnitude > 0);
    }
}

public static class CheckEdges
{
    public static bool Check(Vector3 origin, float distance)
    {
        Debug.DrawLine(origin, origin + Vector3.down * distance, Color.cyan);
        return !Physics.Raycast(origin, Vector3.down, distance, LayerMask.GetMask("Ground"));
    }
}
