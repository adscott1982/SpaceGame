using System.Collections.Generic;
using UnityEngine;

public class SpaceDustManager : MonoBehaviour
{
    public int DustCount;
    public GameObject DustPrefab;
    public float DustMaxSpeed;

    private Camera cameraMain;
    private List<Dust> dustCollection;

	// Use this for initialization
	void Start ()
    {
        this.dustCollection = new List<Dust>();
        this.cameraMain = Camera.main;
        var boundingRectangle = this.cameraMain.OrthographicRectInWorldSpace();
        this.CreateDustParticles(this.DustCount, boundingRectangle);
    }

	// Update is called once per frame
	void Update ()
    {
        var boundingRectangle = this.cameraMain.OrthographicRectInWorldSpace();
        this.WrapDustOutsideBoundary(boundingRectangle);
    }

    private void WrapDustOutsideBoundary(Rect boundingRectangle)
    {
        foreach(var dust in this.dustCollection)
        {
            var newX = dust.transform.position.x;
            var newY = dust.transform.position.y;

            // If the object is greater than or less than the boundary x
            if (dust.transform.position.x > boundingRectangle.xMax || dust.transform.position.x < boundingRectangle.xMin)
            {
                newX = dust.transform.position.x > boundingRectangle.xMax ?
                    dust.transform.position.x - boundingRectangle.width :
                    dust.transform.position.x + boundingRectangle.width;
            }

            if (dust.transform.position.y > boundingRectangle.yMax || dust.transform.position.y < boundingRectangle.yMin)
            {
                newY = dust.transform.position.y > boundingRectangle.yMax ?
                    dust.transform.position.y - boundingRectangle.height :
                    dust.transform.position.y + boundingRectangle.height;
            }

            dust.transform.position = new Vector2(newX, newY);
        }
    }

    private void CreateDustParticles(int count, Rect boundingRectangle)
    {
        for (var i = 0; i < this.DustCount; i++)
        {
            var randomX = Random.Range(boundingRectangle.xMin, boundingRectangle.xMax);
            var randomY = Random.Range(boundingRectangle.yMin, boundingRectangle.yMax);
            var randomDirection = Random.Range(0f, 360f);
            var randomSpeed = Random.Range(0f, this.DustMaxSpeed);
            var randomPosition = new Vector3(randomX, randomY);
            var zeroRotation = new Quaternion();

            var dust = Instantiate(this.DustPrefab, randomPosition, zeroRotation).GetComponent<Dust>();
            dust.Speed = randomDirection.DegreeToVector2() * randomSpeed;

            this.dustCollection.Add(dust);
        }
    }
}
