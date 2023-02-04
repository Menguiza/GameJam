using UnityEngine;

public class PointFix : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    private Transform playerPos;
    private Color initial, wanted;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        initial = sprRenderer.color;
        wanted = new Color(initial.r, initial.g, initial.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < playerPos.position.y - 0.5f)
        {
            sprRenderer.color = wanted;
        }
        else
        {
            sprRenderer.color = initial;
        }
    }
}
