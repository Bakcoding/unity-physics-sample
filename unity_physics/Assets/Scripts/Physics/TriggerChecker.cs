using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    float timerStay = 1f;

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
