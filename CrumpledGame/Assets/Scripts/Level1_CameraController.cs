using UnityEngine;
using UnityEngine.InputSystem;

public class Level1_CameraController : MonoBehaviour
{
    Vector3 direction;
    float x;
    float y;
    float speed = 10;
    void Start()
    {
        y= transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnMove(InputValue data)
    {
        direction = new Vector3(data.Get<Vector2>().x, data.Get<Vector2>().y, 0);
    }
}
