using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;
    public float maxLength;
    public float bottomBoundary;

    bool isMouseDown;
    bool isDragBird = true;

    public GameObject birdPrefab;
    public float birdPositionOffset;
    public int totalBird;

    Rigidbody2D bird;
    Collider2D birdCollider;

    public float force;

    public GameObject gameController;
    private GameController controller;

    public GameObject pointPrefab;
    private GameObject[] points;
    public int numberOfPoints;


    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        maxLength = 5f;
        controller = gameController.GetComponent<GameController>();
        CreateBird();
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, center.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            //kep doi tuong voi ban kinh toi da = maxlength

            /*currentPosition = ClampBoundary(currentPosition);*/
            //gioi han ban kinh keo xuong

            SetStrips(currentPosition);

            if (birdCollider)
            {
                birdCollider.enabled = true;
            }
            int length = points.Length;
            for (int i = 0; i < length; i++)
            {
                points[i].transform.position = PointPosition(i * 0.1f);
            }
        }
        else
        {
            ResetStrips();
        }
        if (bird != null)
        {
            if ((bird.transform.position.x < center.position.x) && !isDragBird)
            {
                controller.DisplaySoundDragBird();
                isDragBird = true;
            }
            if ((bird.transform.position.x >= center.position.x) && isDragBird)
                isDragBird = false;
        }
        else
        {
            totalBird--;
            if (totalBird > 0)
            {
                CreateBird();
            }
        }
    }

    void CreateBird()
    {
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        //clone doi tuong birdPrefab
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;

        bird.isKinematic = true;
        
        ResetStrips();
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        controller.DisplaySoundDragBird();
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        bird.isKinematic = false;
        Vector3 birdForce = (currentPosition - center.position) * force * -1;
        bird.velocity = birdForce;
        //van toc

        bird = null;
        birdCollider = null;
        /*Invoke("CreateBird", 2);*/
        //goi lai ten phuong thuc voi thoi gian t

        controller.DisplaySoundShoot();
        controller.ReduceNumberOfPlays();
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        // dat ve vi tri ranh roi
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            //normalized chuan hoa
            /*bird.transform.right = -dir.normalized;*/
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        //Mathf.Clamp(value, min, max) -> value neu min <= value <= max
        return vector;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = currentPosition - center.position;
        Vector2 direction = (Vector2)currentPosition + position.normalized * birdPositionOffset;
        Vector2 currentPointPosition = direction + (position * -1 * force * t) + 0.5f * Physics2D.gravity * t * t;
        return currentPointPosition;

    }

}
