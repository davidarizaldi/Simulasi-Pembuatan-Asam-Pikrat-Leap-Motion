using LiquidVolumeFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixBehaviour : MonoBehaviour
{
    private LiquidVolume lv;
    private Rigidbody rb;
    [SerializeField] private float levelPerML; // FlorenceFlask250 = 0.002544f; GraduatedCylinder100 = 0.010111f;
    [SerializeField] private float underSomeLevel; // FlorenceFlask250 = 0; GraduatedCylinder100 = 0.05f;
    [SerializeField] private float levelPerML2; // FlorenceFlask250 = 0; GraduatedCylinder100 = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lv = transform.parent.GetComponent<LiquidVolume>();
        UpdateColliderPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        AddLevel(other);
        Destroy(other);
        UpdateColliderPos();
    }

    void AddLevel(GameObject other)
    {
        if (lv.level < underSomeLevel)
        {
            if (other.name == "Water Particle" || other.name == "Water Particle(Clone)")
            {
                lv.level += levelPerML2;
            }
            else if (other.name == "Water Particle Medium Variant" || other.name == "Water Particle Medium Variant(Clone)")
            {
                lv.level += levelPerML2 * 10;
            }
        }
        else
        {
            if (other.name == "Water Particle" || other.name == "Water Particle(Clone)")
            {
                lv.level += levelPerML;
            }
            else if (other.name == "Water Particle Medium Variant" || other.name == "Water Particle Medium Variant(Clone)")
            {
                lv.level += levelPerML * 10;
            }
        }
    }

    void UpdateColliderPos()
    {
        Vector3 pos = new Vector3(transform.position.x, lv.liquidSurfaceYPosition - transform.localScale.y * 0.5f, transform.position.z);
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
