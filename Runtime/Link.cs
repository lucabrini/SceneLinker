using UnityEngine;
using UnityEngine.SceneManagement;

public class Link : MonoBehaviour
{
    public Linker handledBy;

    private Vector2 _previousLocation;

    private void OnValidate()
    {
        if (handledBy == null) return;
        handledBy.SetDestinationLinkLocation(gameObject.transform.position);
        Debug.Log(handledBy.GetDestinationLinkLocation());
        _previousLocation = handledBy.GetDestinationLinkLocation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        var linkablePlayer = other.gameObject.GetComponent<Linkable>();
        if (linkablePlayer.HasJustBeenTeleported()) return;

        SceneManager.LoadScene(handledBy.destinationScene.sceneToHandle.name);
        var player = other.gameObject;
        linkablePlayer.SetJustTeleported(true);
        player.transform.position = handledBy.destinationLink.GetDestinationLinkLocation();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var linkablePlayer = other.gameObject.gameObject.GetComponent<Linkable>();
        if (linkablePlayer.HasJustBeenTeleported())
        {
            linkablePlayer.SetJustTeleported(false);
        }
    }
}