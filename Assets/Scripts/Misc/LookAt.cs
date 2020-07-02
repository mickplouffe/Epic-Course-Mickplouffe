using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] GameObject target;
    //[SerializeField] bool isResyzing;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
            target = Camera.main.transform.gameObject;

        transform.LookAt(target.transform);

        //if (isResyzing)
        //{

        //}


    }
}
