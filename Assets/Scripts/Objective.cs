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

    public Objective(int id, string nama, float target, string akhiran)
    {
        this.id = id;
        this.nama = nama;
        this.target = target;
        this.akhiran = akhiran;
        isDone = false;
    }

    public Objective(string nama) : this(-1, nama, 0.0f, "")
    {

    }

    public Objective()
    {
        isDone = true;
    }
}
