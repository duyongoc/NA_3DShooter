using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTileStandard : MonoBehaviour
{
    public Transform root;
    public Transform tip;

    public float speed = 5;
    Vector3 m_Velocity;

    ProjectTileBase m_ProjectTileBase;
    public GameObject impactExplosion;

    Vector3 m_lastRootPosition;

    const QueryTriggerInteraction k_TriggerInteraction = QueryTriggerInteraction.Collide;
    public List<Collider> m_IgnoredColliders;

    public int index;

    private void OnEnable()
    {
        m_ProjectTileBase = GetComponent<ProjectTileBase>();
        m_ProjectTileBase.onShoot += OnShoot;
    }

    void OnShoot()
    {
        m_lastRootPosition = root.position;
        if (index == 1)
            m_Velocity = transform.forward * speed;
        else
            m_Velocity = (transform.forward + Vector3.down * 0.1f) * speed;

        m_IgnoredColliders = new List<Collider>();
        Collider[] ownerColliders = m_ProjectTileBase.owner.GetComponentsInChildren<Collider>();
        m_IgnoredColliders.AddRange(ownerColliders);

    }

    void Update()
    {
        transform.position += m_Velocity * Time.deltaTime;

        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = Mathf.Infinity;
        bool foundHit = false;

        Vector3 displacementSinceLastFrame = tip.position - m_lastRootPosition;
        RaycastHit[] hits = Physics.SphereCastAll(m_lastRootPosition, 0.01f, displacementSinceLastFrame.normalized, displacementSinceLastFrame.magnitude, -1, k_TriggerInteraction);
        foreach (var hit in hits)
        {
            if (IsHitValid(hit) && hit.distance < closestHit.distance)
            {
                foundHit = true;
                closestHit = hit;
            }
        }
        if (foundHit)
        {
            // Handle case of casting while already inside a collider
            if (closestHit.distance <= 0f)
            {
                closestHit.point = root.position;
                closestHit.normal = -transform.forward;
            }
            OnHit(closestHit.point, closestHit.normal);
        }

        m_lastRootPosition = root.position;

    }

    bool IsHitValid(RaycastHit hit)
    {
        if (m_IgnoredColliders != null && m_IgnoredColliders.Contains(hit.collider))
            return false;
        return true;
    }

    void OnHit(Vector3 point, Vector3 normal)
    {
        GameObject temp = Instantiate(impactExplosion, point + normal*0.1f, Quaternion.identity);
        Destroy(this);
        Destroy(this.gameObject, 5);
    }


}
