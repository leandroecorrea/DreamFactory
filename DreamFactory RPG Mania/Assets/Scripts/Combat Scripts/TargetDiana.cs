using UnityEngine;

public class TargetDiana : MonoBehaviour
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
