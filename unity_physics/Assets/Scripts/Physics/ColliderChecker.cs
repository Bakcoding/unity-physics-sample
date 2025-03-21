using UnityEngine;

public class ColliderChecker : MonoBehaviour
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
}
