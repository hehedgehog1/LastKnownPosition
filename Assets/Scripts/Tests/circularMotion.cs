using NUnit.Framework;
using UnityEngine;

public class CircularMotionEditModeTests
{
    [Test]
    public void CircularMotion_MovesBetweenRadiusPoints()
    {
        // Create GameObject and component
        var obj = new GameObject("Mover");
        var mover = obj.AddComponent<CircularMotion>();

        // Create radius points and player center
        var center = new GameObject("Center").transform;
        var pointA = new GameObject("A").transform;
        var pointB = new GameObject("B").transform;

        center.position = Vector3.zero;
        pointA.position = new Vector3(5, 0, 0);
        pointB.position = new Vector3(0, 0, 5);

        mover.playerCenter = center;
        mover.radiusPointA = pointA;
        mover.radiusPointB = pointB;

        // Call the testable method at a known time
        mover.UpdateCircularMotion(0f);

        // The object should be at radiusA + center
        Assert.AreEqual(pointA.position, mover.transform.position);

        // Call again at t = 1 (PingPong gives 1)
        mover.UpdateCircularMotion(1f);

        // Now it should be at radiusB + center
        Assert.AreEqual(pointB.position, mover.transform.position);
    }
}