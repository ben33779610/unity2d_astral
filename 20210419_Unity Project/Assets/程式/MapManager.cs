using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapManager : MonoBehaviour

{
    [Header("場景")]
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        

    }
        // Update is called once per frame
        void Update()
        {
        
        SceneManager.LoadScene("教官家");
        }
    
}
