using NUnit.Framework;
using UnityEngine;

public class DogBarkEditModeTests
{
    [Test]
    public void Dog_Barks_When_Between_Points()
    {
        // Create the dog object and component
        var dogObj = new GameObject("Dog");
        var dog = dogObj.AddComponent<DogBarkController>();

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

        // Manually trigger the bark check through reflection or a public method
        var updateMethod = typeof(DogBarkController).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        updateMethod?.Invoke(dog, null);

        // Assert that Play() was triggered
        Assert.IsTrue(audio.isPlaying);
    }
}
