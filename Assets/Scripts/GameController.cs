using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] objectsPrefab;
    [SerializeField] ModeController modeController;

    public List<GameObject> objects;

    [SerializeField] GameObject _blade;
    public float dist;
    private void Update()
    {
        switch (modeController.mode)
        {
            case ModeController.Mode.Brick:
                Brick();
                break;
            case ModeController.Mode.Slice:
                Slice();
                break;
            case ModeController.Mode.Destroy:
                Cell();
                break;
        }
    }

    void Brick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<RayFire.RayfireShatter>())
                {
                    RayFire.RayfireShatter shatter = hit.collider.gameObject.GetComponent<RayFire.RayfireShatter>();
                    shatter.transform.GetChild(0).transform.position = hit.point;
                    shatter.size = .1f;
                    
                    shatter.centerPosition = shatter.transform.GetChild(0).transform.localPosition;
                    shatter.Fragment();
                    AddPhysicsToFragments(shatter.fragmentsAll);

                    Destroy(shatter.gameObject);
                }
            }
        }
    }
    void Slice()
    {
       
        if (Input.GetMouseButton(0) && _blade != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist));
            _blade.transform.position = mousePos;
        }
        
    }
    void Cell()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<RayFire.RayfireShatter>())
                {
                    RayFire.RayfireShatter shatter = hit.collider.gameObject.GetComponent<RayFire.RayfireShatter>();
                    shatter.size = 0;
                    shatter.Fragment();
                    AddPhysicsToFragments(shatter.fragmentsAll);
                    Destroy(shatter.gameObject);
                }
            }
        }
    }

    public void SpawnObject()
    {
        foreach (var i in objects)
        {
            Destroy(i);
        }
        GameObject obj = null;
        switch (modeController.mode)
        {
            case ModeController.Mode.Slice:
                obj = Instantiate(objectsPrefab[0], Vector3.zero, Quaternion.identity);
                _blade = obj.transform.GetChild(1).gameObject;
                break;
            case ModeController.Mode.Brick:
                obj = Instantiate(objectsPrefab[1], Vector3.zero, Quaternion.identity);
                break;
            case ModeController.Mode.Destroy:
                obj = Instantiate(objectsPrefab[2], Vector3.zero, Quaternion.identity);
                break;
        }

        objects.Add(obj);
    }

    public void ResetObject()
    {
        int id = 0;
        for (int i = 0; i < modeController.toggles.Length; i++)
        {
            if (modeController.toggles[i].isOn)
                id = i;
            else
                modeController.toggles[i].isOn = false;
        }
        foreach (var i in objects)
        {
            Destroy(i);
        }
        GameObject obj = Instantiate(objectsPrefab[id], Vector3.zero, Quaternion.identity);
        objects.Add(obj);

        if (id == 0)
            _blade = obj.transform.GetChild(1).gameObject;
    }

    void AddPhysicsToFragments(List<GameObject> fragments)
    {
        foreach (var i in fragments)
        {
            i.AddComponent<MeshCollider>().convex = true;
            i.AddComponent<Rigidbody>();
        }
    }
}
