using UnityEngine;

public class SimulationObject : MonoBehaviour
{
    [SerializeField] bool mouseClickableObject;
    private Vector3 originPos;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        if (mouseClickableObject)
        {
            gameObject.layer = LayerMask.NameToLayer("ClickableObject");
        }
        originPos = transform.position;
    }

    private void OnDisable()
    {
        SetOriginPosition();
    }

    private void SetOriginPosition()
    {
        transform.position = originPos;
    }

    public void Move(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void ApplyForce(Vector3 force)
    {
        rigidBody.AddForce(force, ForceMode.Impulse);
    }

    public void ApplyTorque(Vector3 force)
    {
        rigidBody.AddTorque(force, ForceMode.Impulse);
    }

    public void ApplyExplosion(float force, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
    }

    public void Inertia(Vector3 force)
    {
        rigidBody.linearVelocity = force * 0.1f;
        rigidBody.angularVelocity = force * 0.1f;
    }

    public void ResetState()
    {
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.linearVelocity = Vector3.zero;
    }

}
