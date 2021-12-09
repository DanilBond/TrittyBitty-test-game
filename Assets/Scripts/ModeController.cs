using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public enum Mode{
        Slice,
        Brick,
        Destroy
    }
    Mode _mode;
    public Mode mode { get => _mode; set => _mode = value; }

    [SerializeField] CameraController cameraController;
    public Toggle[] toggles;
    
    public void FreeMode()
    {
        cameraController.CanMove = !cameraController.CanMove;
    }

    public void SetMode(int id)
    {
        switch (id)
        {
            case 0:
                mode = Mode.Slice;

                break;
            case 1:
                mode = Mode.Brick;
                break;
            case 2:
                mode = Mode.Destroy;
                break;
        }
        FindObjectOfType<GameController>().SpawnObject();
    }
}
