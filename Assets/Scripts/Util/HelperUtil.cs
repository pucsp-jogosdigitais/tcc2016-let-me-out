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
        /// Realiza busca de GameObject Ativo/Inativo. Método original disponível em: http://answers.unity3d.com/questions/890636/find-an-inactive-game-object.html
        /// </summary>
        /// <param name="parent">GameObject Pai</param>
        /// <param name="name">Nome do GameObject Procurado</param>
        /// <returns></returns>
        public static GameObject FindObject(GameObject parent, string name)
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
    }
}
