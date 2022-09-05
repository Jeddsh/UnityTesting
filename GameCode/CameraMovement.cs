
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //room camera
    [SerializeField] private float speed;
    private float currentPosx;
    private Vector3 velocity = Vector3.zero;

    //follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float aboveDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    private float lookAbove;

    private void Update() {
        //room camera movement
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosx,transform.position.y,transform.position.z),ref velocity, speed);

        //Follows player
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y + lookAbove, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        lookAbove = Mathf.Lerp(lookAbove, aboveDistance, Time.deltaTime * cameraSpeed);
    }
    public void MovetoNewRoom(Transform _newRoom) {
        currentPosx = _newRoom.position.x;
    }
}
