using UnityEngine;
using UnityEngine.UI;

public class ConvexPhysicsUI : MonoBehaviour
{
    [SerializeField] Toggle toggleConvex;
    [SerializeField] GameObject convex;
    [SerializeField] GameObject noneConvex;

    private void Start()
    {
        convex.SetActive(toggleConvex.isOn);
        noneConvex.SetActive(!toggleConvex.isOn);
        toggleConvex.onValueChanged.AddListener(OnChangedConvex);
    }

    private void OnChangedConvex(bool isOn)
    {
        convex.SetActive(isOn);
        noneConvex.SetActive(!isOn);
    }
}
