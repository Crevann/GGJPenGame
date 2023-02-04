using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaycastInstance : MonoBehaviour
{
    public Camera Cam;
    public GameObject[] Prefabs;
    private int Prefab;
    private Ray RayMouse;
    private GameObject Instance;
    private float windowDpi;
    [SerializeField] NavMeshAgent navMeshAgent;
    //Double-click protection
    private float buttonSaver = 0f;

    void Start()
    {
        if (Screen.dpi < 1) windowDpi = 1;
        if (Screen.dpi < 200) windowDpi = 1;
        else windowDpi = Screen.dpi / 200f;
        Counter(0);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Cam != null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = Cam.ScreenPointToRay(mousePos);
                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    NavMeshPath path = new NavMeshPath();
                    if(!navMeshAgent.CalculatePath(hit.point, path)) return ;
                    if (!Instance)
                        Instance = Instantiate(Prefabs[Prefab], transform);
                    Instance.transform.position = hit.point + hit.normal * 0.01f;
                    Instance.GetComponent<ParticleSystem>().Play();
                    //Destroy(Instance, 1.5f);
                }
            }
            else
            {
                Debug.Log("No camera");
            }          
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0) && buttonSaver >= 0.4f)// left button
        {
            buttonSaver = 0f;
            Counter(-1);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0) && buttonSaver >= 0.4f)// right button
        {
            buttonSaver = 0f;
            Counter(+1);
        }
        buttonSaver += Time.deltaTime;
    }

    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > Prefabs.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }
    }
}
