using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites;

    public Sprite ChangeSprite(int i)
    {
        return sprites[i];
    }
}
