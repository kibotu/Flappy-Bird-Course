// Bird.cs

using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D myBody;
    private Vector3 startPos;
    private bool dead = false;

    public PipeSet pipes;
    public float counter = 0f;

    public bool isTouching = false;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        startPos = transform.localPosition;
    }

    private void Update()
    {
        if (!dead)
        {
            counter += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                Push();
            }

            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    isTouching = true;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    isTouching = false;
                    Push();
                }
            }
        }
        else
        {
            ResetPos();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision2d)
    {
        dead = true;
    }

    public void Push()
    {
        myBody.AddForce(Vector2.up, ForceMode2D.Impulse);
    }

    public void ResetPos()
    {
        myBody.velocity = Vector3.zero;
        transform.localPosition = startPos;
        dead = false;
        pipes.ResetPos();
        counter = 0;
    }
}