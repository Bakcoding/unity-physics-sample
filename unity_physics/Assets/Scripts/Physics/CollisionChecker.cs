using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    float timerStay = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SimulationObject>() != null)
        {
            CanvasDebug.Instance.ShowLog("<color=#0087FF>[Collision]</color> Enter ");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<SimulationObject>() != null)
        {
            timerStay += Time.deltaTime;
            if (timerStay >= 1f)
            {
                timerStay = 0f;
                CanvasDebug.Instance.ShowLog("<color=#0087FF>[Collision]</color> Stay ");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<SimulationObject>() != null)
        {
            timerStay = 1f;
            CanvasDebug.Instance.ShowLog("<color=#0087FF>[Collision]</color> Exit ");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SimulationObject>() != null)
        {
            CanvasDebug.Instance.ShowLog("<color=#FF6400>[Trigger]</color> Enter ");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<SimulationObject>() != null)
        {
            timerStay += Time.deltaTime;
            if (timerStay >= 1f)
            {
                timerStay = 0f;
                CanvasDebug.Instance.ShowLog("<color=#FF6400>[Trigger]</color> Stay ");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SimulationObject>() != null)
        {
            timerStay = 1f;
            CanvasDebug.Instance.ShowLog("<color=#FF6400>[Trigger]</color> Exit ");
        }
    }
}
