using NUnit.Framework;
using UnityEngine;

public class DogBarkTests
{
    [Test]
    public void Dog_Barks_When_Between_Points()
    {
        // Create GameObject with the DogBark component
        var dogObj = new GameObject("Dog");
        var dog = dogObj.AddComponent<DogBark>();

        // Create pointA and pointB
        var pointA = new GameObject("A").transform;
        var pointB = new GameObject("B").transform;

        pointA.position = new Vector3(0, 0, 0);
        pointB.position = new Vector3(10, 0, 0);

        dog.pointA = pointA;
        dog.pointB = pointB;

        // Add AudioSource
        var audio = dogObj.AddComponent<AudioSource>();
        dog.barkSound = audio;

        // Place dog between the points
        dogObj.transform.position = new Vector3(5, 0, 0);

        // Ensure sound is not already playing
        audio.Stop();

        // Manually call Update()
        dog.Update();

        // Assert that Play() was triggered
        Assert.IsTrue(audio.isPlaying);
    }
}
