using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        // Make camera follow player
        transform.position = cameraPosition.position;
    }
}
