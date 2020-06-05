using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy1 : MonoBehaviour
{

    //public Camera cam;
    public GameObject _target;
    public NavMeshAgent agent;
    [SerializeField] int _health = 1, _warFund = 10;

    private void OnEnable()
    {
        //Invoke("Hide", 30);
        if (_target == null)
        {
            _target = GameObject.Find("TheHQ");
        }
        //agent.SetDestination(_target.transform.position);

    }

    private void Start()
    {
        agent.SetDestination(_target.transform.position);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //PointAndClick();

    //}



    void OnDying()
    {
        

    }

    void Hide()
    {
        CancelInvoke();
        this.gameObject.SetActive(false);

    }

    //void PointAndClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            agent.SetDestination(hit.point);
    //        }
    //    }
    //}

}
