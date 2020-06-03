using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy1 : MonoBehaviour
{

    //public Camera cam;
    [SerializeField] GameObject _target;
    public NavMeshAgent agent;


    private void Start()
    {
        agent.SetDestination(_target.transform.position);

    }
    // Update is called once per frame
    void Update()
    {
        //PointAndClick();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TheHQ")
        {
            SpawnManager.enemiesPool.Add(this.gameObject);
            SpawnManager.enemiesSpawned.Remove(this.gameObject);

                Debug.LogWarning("Pool: " + SpawnManager.enemiesPool.Count);
                Debug.LogWarning("Spawned: " + SpawnManager.enemiesSpawned.Count);

            this.gameObject.SetActive(false);
        }
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
