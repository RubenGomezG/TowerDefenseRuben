using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camara : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera virtualCamera;

    float horizontal;
    float vertical;
    public Transform target;
    [SerializeField] float zoomMin;
    [SerializeField] float zoomMax;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && virtualCamera.m_Lens.OrthographicSize < zoomMax)
        {
            virtualCamera.m_Lens.OrthographicSize += 0.5f;
            //GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && virtualCamera.m_Lens.OrthographicSize > zoomMin)
        {
            
            virtualCamera.m_Lens.OrthographicSize -= 0.5f;
            //GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 20;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 position = target.transform.position;
        position.x = target.transform.position.x + horizontal/100;
        position.y = target.transform.position.y + vertical/100;
        target.transform.position = position;
        /*if (horizontal < 0)
        {
            empty.transform.position.Set(virtualCamera.transform.position.x - horizontal, virtualCamera.transform.position.y, virtualCamera.transform.position.z);
        }
        if (horizontal > 0)
        {
            empty.transform.position.Set(virtualCamera.transform.position.x + horizontal, virtualCamera.transform.position.y, virtualCamera.transform.position.z);
        }

        if (vertical < 0)
        {
            empty.transform.position.Set(virtualCamera.transform.position.x, virtualCamera.transform.position.y - vertical, virtualCamera.transform.position.z);
        }
        if (vertical > 0)
        {
            empty.transform.position.Set(virtualCamera.transform.position.x, virtualCamera.transform.position.y + vertical, virtualCamera.transform.position.z);
        }*/
    }

    /*IEnumerator Lerp(float start, float end)
    {
        t = 0f;
        while(virtualCamera.m_Lens.OrthographicSize != end)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(start, end, t);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }*/
}
