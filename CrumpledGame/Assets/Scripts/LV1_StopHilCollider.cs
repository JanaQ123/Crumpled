using UnityEngine;
using UnityEngine.InputSystem;

public class LV1_StopHillCollider : MonoBehaviour

{
    [SerializeField] GameObject parent;
    [SerializeField] GameObject UD;
    [SerializeField] AudioSource grass;
    bool canMove;
    void Start()

    {

        parent.GetComponent<LV1_PlayerParentFollow>().SetRollingMode(true);
        UD.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)

    {

        if (collision.gameObject.tag == "Player")

        {
            grass.Stop();
            parent.GetComponent<LV1_PlayerParentFollow>().SetRollingMode(false);
            UD.SetActive(true);
        }

    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed|| Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            parent.GetComponentInChildren<LV1_PlayerController>().CanMove = true;
            Destroy(this);
        }

    }

    void MustPressButton()
    {
        if (canMove)
        {
        }

 

    }
}