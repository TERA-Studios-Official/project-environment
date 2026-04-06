using UnityEngine;


public class UIManager : MonoBehaviour
{
    
        public GameObject registreerPagina;
        public GameObject welkomPagina;
        public void openPagina(GameObject obj)
        {
            obj.SetActive(true);
        }
        public void closePagina(GameObject obj)
        {
            obj.SetActive(false);
        }


}
