using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class PetPool : MonoBehaviour
    {
        [SerializeField] List<GameObject> petPool;
        public GameObject InitSpawn(Vector3 initPos)
        {
            int index = Random.Range(0, petPool.Count);
            GameObject pet = Instantiate(petPool[index], initPos,Quaternion.identity);
            return pet;
        }
        public GameObject ChooseAxie(int index)
        {
            try
            {
                return petPool[index];
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }

}
