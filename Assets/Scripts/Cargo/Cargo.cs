using UnityEngine;

public class Cargo : MonoBehaviour
{
    [SerializeField] private float ropeLength = 2.5f;
    [SerializeField] private LineRenderer rope;

    private DistanceJoint2D joint;   // keep the joint you create in Attach, so LateUpdate can use it

    public void Attach(Rigidbody2D target)
    {
        gameObject.transform.position = target.position + new Vector2(0, -ropeLength);

        joint = gameObject.AddComponent<DistanceJoint2D>();
        joint.connectedBody = target;
        joint.autoConfigureDistance = false;
        joint.distance = ropeLength;
        joint.maxDistanceOnly = true;
        joint.enableCollision = false;
        joint.anchor = new Vector2(0, 0.75f);
        joint.connectedAnchor = new Vector2(0, -1f);

        rope.enabled = true;
        rope.positionCount = 2;
    }

    public float GetRopeLength()
    {
        return ropeLength;
    }



    private void LateUpdate()
    {
        if(joint == null) return;
        rope.SetPosition(0, transform.TransformPoint(joint.anchor));
        rope.SetPosition(1, joint.connectedBody.transform.TransformPoint(joint.connectedAnchor));
    }
}
