using UnityEngine;
using UnityEngine.InputSystem;

public enum eSimulationType
{
    Basic, Spring, Force, Torque, Explosion,
}

public class PhysicsTutorial : MonoBehaviour
{
    private static PhysicsTutorial instance;
    public static PhysicsTutorial Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<PhysicsTutorial>();
                if (instance == null)
                {
                    GameObject go = new GameObject("PhysicsTutorial");
                    instance = go.AddComponent<PhysicsTutorial>();
                }
            }
            return instance;
        }
    }

    [SerializeField] GameObject explosionEffect;
    [SerializeField] LayerMask clickableObject;

    private SimulationObject selectedObject;
    private Vector3 previousPosition;
    private Vector3 inertiaVelocity;
    private Vector3 objectInteractPoint;

    private eSimulationType simulationType;
    public eSimulationType SimulationType { set { simulationType = value; } }

    private float forceAmount;
    private float torqueAmount;
    private float explosionForceAmount;

    public float ForceAmount { set { forceAmount = value; } }
    public float TorqueAmount { set { torqueAmount = value; } }
    public float ExplosionForceAmount { set { explosionForceAmount = value; } }

    public void Explosion(Vector3 targetPos)
    {
        if (explosionEffect != null)
        {
            GameObject go = Instantiate(explosionEffect);
            go.transform.position = targetPos;
            go.SetActive(true);
        }
    }

    private void Update()
    {
        UpdateMouseInteraction();
    }
    
    public void UpdateMouseInteraction()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            DetectObject();
        }
        if (Mouse.current.leftButton.isPressed)
        {
            InteractObject();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ApplyInertia();
            selectedObject = null;
        }
    }

    public void DetectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableObject))
        {
            Transform hitObject = hit.transform;
            
            selectedObject = hitObject.gameObject.GetComponent<SimulationObject>();
            if (selectedObject)
            {
                objectInteractPoint = hit.point;
                selectedObject.ResetState();
            }
        }
    }

    private void InteractObject()
    {
        if (selectedObject == null) return;

        if (simulationType == eSimulationType.Basic)
        {
            MoveObject();
        }
        else if (simulationType == eSimulationType.Force)
        {
            ApplyForce();
        }
        else if (simulationType == eSimulationType.Torque)
        {
            ApplyTorque();
        }
        else if (simulationType == eSimulationType.Explosion)
        {
            ApplyExplosion();
        }
    }

    private void MoveObject()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        float distanceToObject = Mathf.Abs(Camera.main.transform.position.z - selectedObject.transform.position.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, distanceToObject));
        worldPosition.y = Mathf.Clamp(worldPosition.y, 0.5f, 10f);
        worldPosition.x = Mathf.Clamp(worldPosition.x, -7f, 7f);

        inertiaVelocity = (worldPosition - previousPosition) / Time.deltaTime;
        previousPosition = selectedObject.transform.position;

        selectedObject.Move(new Vector3(worldPosition.x, worldPosition.y, selectedObject.transform.position.z));
    }

    private void ApplyForce()
    {
        Vector3 dir = (selectedObject.transform.position - objectInteractPoint).normalized;
        selectedObject.ApplyForce(dir * forceAmount);
    }

    private void ApplyTorque()
    {
        Vector3 dir = (selectedObject.transform.position - objectInteractPoint).normalized;
        selectedObject.ApplyTorque(Vector3.up * torqueAmount);
    }

    private void ApplyExplosion()
    {
        selectedObject.ApplyExplosion(explosionForceAmount, 10f);
    }

    private void ApplyInertia()
    {
        if (selectedObject != null && 
            selectedObject.tag != "SpringObject")
        {
            inertiaVelocity.x = Mathf.Clamp(inertiaVelocity.x, -700f, 700f);
            inertiaVelocity.y = Mathf.Clamp(inertiaVelocity.y, -700f, 700f);

            selectedObject.Inertia(inertiaVelocity);
        }
    }
}
