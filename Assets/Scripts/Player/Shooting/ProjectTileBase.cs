using UnityEngine;
using UnityEngine.Events;

public class ProjectTileBase : MonoBehaviour
{
    public GameObject owner {get; set;}
    public UnityAction onShoot;

    public void Shoot(WeaponController controller)
    {
        owner = controller.owner;
        if(onShoot != null)
        {
            onShoot.Invoke();
        }
    }
}
