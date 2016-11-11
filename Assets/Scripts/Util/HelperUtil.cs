using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public static class HelperUtil
    {
        /// <summary>
        /// Realiza busca de GameObject ativo/inativo. Método original disponível em: http://answers.unity3d.com/questions/890636/find-an-inactive-game-object.html
        /// </summary>
        /// <param name="parent">GameObject pai</param>
        /// <param name="name">Nome do GameObject procurado</param>
        /// <returns></returns>
        public static GameObject FindGameObject(GameObject parent, string name)
        {
            Component[] cmps = parent.GetComponentsInChildren(typeof(Transform), true);
            List<Transform> trs = new List<Transform>();       

            foreach(Component c in cmps)
            {
                Transform t = c as Transform;

                trs.Add(t);
            }

            foreach (Transform t in trs)
            {
                if (t.name == name)
                {
                    return t.gameObject;
                }
            }
            return null;
        }

        /// <summary>
        /// Realiza busca de GameObjects ativos/inativos pelo nome da tag.
        /// </summary>
        /// <param name="parent">GameObject pai</param>
        /// <param name="name">Nome da tag procurada</param>
        /// <returns></returns>
        public static IList<GameObject> FindGameObjectsWithTag(GameObject parent, string tagName)
        {
            Component[] cmps = parent.GetComponentsInChildren(typeof(Transform), true);
            List<Transform> trs = new List<Transform>();
            List<GameObject> gameObjects = new List<GameObject>();

            foreach (Component c in cmps)
            {
                Transform t = c as Transform;

                trs.Add(t);
            }

            foreach (Transform t in trs)
            {
                if (t.tag.Contains(tagName))
                {
                    gameObjects.Add(t.gameObject);
                }
            }
            return gameObjects;
        }
    }
}
