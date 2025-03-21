using UnityEngine;

public class BasicPhysics : MonoBehaviour
{
    private void OnDisable()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        PhysicsTutorial.Instance.SimulationType = eSimulationType.Basic;
    }
}
