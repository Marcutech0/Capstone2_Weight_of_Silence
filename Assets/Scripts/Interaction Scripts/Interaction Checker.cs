using UnityEngine;

public class InteractionChecker : MonoBehaviour
{
    public NewsScreenInteract _NewsScreen;
    public FlyerWallInteraction _FlyerWall;

    private void OnTriggerEnter(Collider _Other)
    {
        if (_Other.CompareTag("NPC") && _NewsScreen._HasInteracted && _FlyerWall._HasInteracted) 
        {
            _FlyerWall._NPC.SetActive(true);
        }
    }
}
