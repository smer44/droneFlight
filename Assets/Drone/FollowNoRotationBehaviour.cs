using UnityEngine;

public class NoRotationBEhaviour : MonoBehaviour
{
    public GameObject followerObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles  =  new Vector3 (0,followerObject.transform.eulerAngles.y,0) ; //Quaternion.identity;
        transform.position = followerObject.transform.position;
    }
}
