using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scene Linker/Add Link Component")]
public class Link : MonoBehaviour
{
    public Linker handledBy;
    [SerializeField] private string detectableTag = "Player";
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
        var destinationScene = handledBy.destinationScene.sceneToHandle;
        if (destinationScene == null)
        {
            throw new NullReferenceException("Destination Scene Handler has no scene selected.");
        }

        if (!other.gameObject.CompareTag(detectableTag)) return;
        var player = other.gameObject;

        var linkableComponent = player.GetComponent<Linkable>();
        if (linkableComponent == null)
        {
            throw new NullReferenceException("The GameObject to handle must have a 'Linkable' component attached.");
        }

        if (linkableComponent.HasJustBeenTeleported()) return;

        SceneManager.LoadScene(destinationScene.name);
        
        linkableComponent.SetJustTeleported(true);
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