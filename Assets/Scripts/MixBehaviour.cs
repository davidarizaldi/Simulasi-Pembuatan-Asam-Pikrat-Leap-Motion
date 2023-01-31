using LiquidVolumeFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixBehaviour : MonoBehaviour
{
    private LiquidVolume lv;
    private Rigidbody rb;
    private Collider[] childrenColliders;

    [SerializeField] private float levelPerML; // FlorenceFlask250 = 0.002544f; GraduatedCylinder100 = 0.010111f, ErlenMeyer250 = 0.002;
    [SerializeField] private float underSomeLevel; // FlorenceFlask250 = 0; GraduatedCylinder100 = 0.05f;
    [SerializeField] private float levelPerML2; // FlorenceFlask250 = 0; GraduatedCylinder100 = 0.005f;
    [SerializeField] private bool isPrimaryFlask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lv = transform.parent.GetComponent<LiquidVolume>();
        
        childrenColliders = transform.parent.transform.parent.GetComponentsInChildren<Collider>();
        foreach (Collider col in childrenColliders)
        {
            if (col != GetComponent<Collider>())
            {
                Physics.IgnoreCollision(col, GetComponent<Collider>());
            }
        }
        Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.Find("Magnet").transform.GetComponent<Collider>());
        UpdateColliderPos();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water Particle"))
        {
            AddLevel(other);
            Destroy(other);
            UpdateColliderPos();
        }
    }

    void AddLevel(GameObject other)
    {
        if (!isPrimaryFlask)
        {
            if (lv.level < underSomeLevel)
            {
                lv.level += levelPerML2;
            }
            else
            {
                lv.level += levelPerML;
            }
        }
        else
        {
            switch (other.name)
            {
                case "Phenol Particle Variant(Clone)":
                    lv.liquidLayers[0].amount += levelPerML;
                    break;
                case "Sulfuric Acid Particle Variant(Clone)":
                    lv.liquidLayers[1].amount += levelPerML;
                    break;
                case "Nitric Acid Particle Variant(Clone)":
                    lv.liquidLayers[2].amount += levelPerML;
                    if (GameManager.practicumStep == 2)
                    {
                        transform.parent.GetComponent<SmokeBehaviour>().NitricAcidAdded();
                        lv.liquidLayers[0].color = new Color(0.647f, 0.165f, 0.165f, 0.502f);
                        lv.liquidLayers[1].color = new Color(0.647f, 0.165f, 0.165f, 0.502f);
                        lv.liquidLayers[2].color = new Color(0.647f, 0.165f, 0.165f, 0.502f);
                    }
                    break;
                case "Water Particle(Clone)":
                    lv.liquidLayers[3].amount += levelPerML;
                    break;
                default:
                    break;
            }
            lv.UpdateLayers();
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLevels();
        }
    }

    void UpdateColliderPos()
    {
        Vector3 pos = new(transform.position.x, lv.liquidSurfaceYPosition - transform.localScale.y * 0.5f + 0.01f, transform.position.z);
        rb.position = pos;
        if (lv.level >= 1f)
        {
            transform.localRotation = Quaternion.Euler(Random.value * 30 - 15, Random.value * 30 - 15, Random.value * 30 - 15);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
