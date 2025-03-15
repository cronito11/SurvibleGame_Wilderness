using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
        //Debug.Log("Player position: " + transform.position);    
    }
}
