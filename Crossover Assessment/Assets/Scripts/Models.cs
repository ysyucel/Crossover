using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Models : MonoBehaviour
{
    
}
[System.Serializable]
public class StudentData
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
    // Add more properties as needed
}

// Wrapper class to handle deserialization of JSON array
[Serializable]
public class ListWrapper<T>
{
    public List<T> data;
}
