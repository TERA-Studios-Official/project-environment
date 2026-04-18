// UIManager.cs
//
// Description:
// Manages the UI.
//
// Date of last amendment:
// 17/04/2026

using UnityEngine;

public class UIManager : MonoBehaviour
{
        public void openPagina(GameObject obj)
        {
            obj.SetActive(true);
        }
        public void closePagina(GameObject obj)
        {
            obj.SetActive(false);
        }


}
