using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * 100);
    }

}
