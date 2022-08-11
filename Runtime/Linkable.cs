using UnityEngine;

[AddComponentMenu("Scene Linker/Add Linkable Component")]
public class Linkable : MonoBehaviour
{
    [SerializeField] private bool justTeleported = false;

    public void SetJustTeleported(bool teleported)
    {
        justTeleported = teleported;
    }

    public bool HasJustBeenTeleported()
    {
        return justTeleported;
    }
}