using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_L3 : MonoBehaviour
{
    float moveSpeed = 5f;
    float playerPosX;
    float playerPosZ;
    Vector3 direction;
    Vector3 camOffset;
    Vector3 playerPos;
    Rigidbody rb;
    
    void Start()
    {
        camOffset = Camera.main.transform.position - transform.position;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
       PlayerMove();
    }
    void PlayerMove()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        playerPos = Camera.main.transform.position + camOffset;
        playerPosX = transform.position.x;
        playerPosZ = playerPos.z;
        playerPosX = Mathf.Clamp(playerPosX, -9.3f, 9.3f);
        transform.position = new Vector3(playerPosX, 0f, playerPosZ);
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
