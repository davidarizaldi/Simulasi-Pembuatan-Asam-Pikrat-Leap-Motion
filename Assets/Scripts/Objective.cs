using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective
{
    public int id;
    public string nama;
    public float target;
    public string akhiran;
    public bool isDone;

    public Objective(int id, string nama, float target, string akhiran, bool isDone = false)
    {
        this.id = id;
        this.nama = nama;
        this.target = target;
        this.akhiran = akhiran;
        this.isDone = isDone;
    }

    public Objective(string nama) : this(-1, nama, 0.0f, "")
    {

    }

    public Objective(string nama, bool isDone) : this(-1, nama, 0.0f, "", isDone)
    {
        
    }

    public Objective()
    {
        isDone = true;
    }
}
