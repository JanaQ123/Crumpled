using UnityEngine;

public class LV1_Leash : MonoBehaviour
{
    public Transform leashStart;  // owner hand
    public Transform leashEnd;    // dog collar

    [Header("Appearance")]
    public Color leashColor = new Color(0.4f, 0.2f, 0.05f); // brown
    public float leashWidth = 0.153f;

    [Header("Sag / Curve")]
    [Range(2, 20)]
    public int segments = 12;          // more = smoother curve
    [Range(0f, 1f)]
    public float sagAmount = 0.3f;     // how much it droops down

    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = leashColor;
        lr.endColor = leashColor;
        lr.startWidth = leashWidth;
        lr.endWidth = leashWidth;
        lr.positionCount = segments;
    }

    void Update()
    {
        DrawCurvedLeash();
    }

    void DrawCurvedLeash()
    {
        Vector3 start = leashStart.position;
        Vector3 end = leashEnd.position;

        // Control point: midpoint pulled downward by sagAmount
        Vector3 mid = (start + end) / 2f;
        mid.y -= sagAmount;

        lr.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);

            // Quadratic Bezier: B(t) = (1-t)²·P0 + 2(1-t)t·P1 + t²·P2
            Vector3 point = Mathf.Pow(1 - t, 2) * start
                          + 2 * (1 - t) * t * mid
                          + Mathf.Pow(t, 2) * end;

            lr.SetPosition(i, point);
        }
    }
}
