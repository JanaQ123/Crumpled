using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_L3 : MonoBehaviour
{
    float moveSpeed = 5f;
    float playerPosX;
    Vector3 direction;
    Vector3 playerPos;    
    void Update()
    {
       PlayerMove();
    }
    void PlayerMove()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        playerPosX = transform.position.x;
        playerPosX = Mathf.Clamp(playerPosX, -9.3f, 9.3f);
        transform.position = new Vector3(playerPosX, transform.position.y, transform.position.z);
    }
    public void OnMove(InputValue value)
    {
        direction = new Vector3(value.Get<Vector2>().x, 0f, 0f);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Bird"))
        {
            SceneManager.LoadScene("Level 3");
        }
    }
}
