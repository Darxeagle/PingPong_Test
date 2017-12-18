using Assets.Common.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Rigidbody2D))]
public class BallMediator : MonoBehaviour
{
    [SerializeField] private Image Image;

    public EventWrap<GameObject> CollisionEvent { get; private set; }
    public Rigidbody2D Rigidbody2D { get { return gameObject.GetComponent<Rigidbody2D>(); } }

    public BallMediator()
    {
        CollisionEvent = new EventWrap<GameObject>();
    }

    public void UpdateView(float size, Color color)
    {
        Image.transform.localScale = Vector3.one*size;
        Image.color = color;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        CollisionEvent.Dispatch(col.gameObject);
    }
}
