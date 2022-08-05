using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform playerPosition;
    public Transform cameraPosition;

    private void Update()
    {
        // Make camera follow player
        transform.position = new Vector3(playerPosition.position.x, cameraPosition.position.y, playerPosition.position.z);
    }
}
