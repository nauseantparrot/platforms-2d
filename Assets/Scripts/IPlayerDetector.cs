using UnityEngine;

public interface IPlayerDetector
{
    void DetectPlayer(GameObject playerGameObject);

    void LostPlayer();
}
