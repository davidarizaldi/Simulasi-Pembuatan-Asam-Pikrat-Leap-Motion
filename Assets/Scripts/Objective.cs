using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective
{
    public int id;
    public string nama;
    public string namaID;
    public float target;
    public string akhiran;
    public bool isDone;

    public Objective(int id, string nama, string namaID, float target, string akhiran, bool isDone = false)
    {
        this.id = id;
        this.nama = nama;
        this.namaID = namaID;
        this.target = target;
        this.akhiran = akhiran;
        this.isDone = isDone;
    }

    public Objective(string nama, string namaID) : this(-1, nama, namaID, 0.0f, "")
    {

    }

    public Objective(string nama, string namaID, bool isDone) : this(-1, nama, namaID, 0.0f, "", isDone)
    {
        
    }

    public Objective()
    {
        isDone = true;
    }
}
