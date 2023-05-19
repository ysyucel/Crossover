using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaPiece : MonoBehaviour
{
    [SerializeField] GameObject labelText;
    // Start is called before the first frame update
    void Start()
    {
        TextMesh textObject = GameObject.Find("object2").GetComponent<TextMesh>();
        textObject.text = "test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
