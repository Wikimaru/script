using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float SmoothSpeed = 0.125f;
    public Vector3 offset;
    private bool hasTarget;

    private void Start()
    {
        StartCoroutine(FindTarget());
    }
    private void FixedUpdate()
    {
        if (hasTarget) 
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed*Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }

    }
    private IEnumerator FindTarget() 
    {
        if (GameManager.instance.Check(GameManager.CheckType.player)) 
        {
            Debug.Log("CheckComplate");
            target = GameManager.instance.player.transform;
            hasTarget = true;
        }
        else 
        {
            Debug.Log("CheckFail");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("checkAgain");
            StartCoroutine(FindTarget());
        }
    }
}
