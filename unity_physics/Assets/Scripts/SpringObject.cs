using UnityEngine;

public class SpringObject : MonoBehaviour
{
    [SerializeField] LineRenderer debugLine;

    private SpringJoint springJoint;

    private void Start()
    {
        debugLine.positionCount = 2;
        springJoint = GetComponent<SpringJoint>();
    }

    private void FixedUpdate()
    {
        UpdateDebugLine();
    }

    private void UpdateDebugLine()
    {
        if (springJoint != null && debugLine != null)
        {
            Vector3 anchorPosition = springJoint.transform.TransformPoint(springJoint.anchor);
            Vector3 connectedAnchorPosition = springJoint.connectedAnchor != null ?
                springJoint.connectedBody.transform.TransformPoint(springJoint.connectedAnchor) : anchorPosition;

            debugLine.SetPosition(0, anchorPosition);
            debugLine.SetPosition(1, connectedAnchorPosition);
        }
    }
}
