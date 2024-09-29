using UnityEngine;

public class Ladder : MonoBehaviour, IInteractableObj
{
    [SerializeField] private Transform _bottomWaypoint;
    [SerializeField] private Transform _topWaypoint;

    public void Interact()
    {
        Climb();
    }

    private void Climb()
    {
        Transform startingPoint = null;
        Transform endPoint = null;

        GetWaypoint(out startingPoint, out endPoint);

        //Player.instance.OnEnabled(false);
        Player.instance.transform.position = endPoint.position;
        //StartCoroutine(Player.instance.Climb(startingPoint, endPoint));
    }

    private void GetWaypoint(out Transform startingPoint, out Transform endPoint)
    {
        var bottomDistance = Vector3.Distance(Player.instance.transform.position, _bottomWaypoint.position);
        var topDistance = Vector3.Distance(Player.instance.transform.position, _topWaypoint.position);

        if (bottomDistance > topDistance)
        {
            startingPoint = _topWaypoint;
            endPoint = _bottomWaypoint;
        }
        else
        {
            startingPoint = _bottomWaypoint;
            endPoint = _topWaypoint;
        }
    }
}
