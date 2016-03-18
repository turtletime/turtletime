using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheloniiUnity
{
    class ModelCollection<M> : ICollection<M> where M : Model
    {
        HashSet<M> models = new HashSet<M>();

        public int Count { get { return models.Count; } }

        public bool IsReadOnly { get { return false; } }

        public virtual void Add(M item)
        {
            models.Add(item);
        }

        public virtual void AddRange(ICollection<M> items)
        {
            foreach (M item in items)
            {
                Add(item);
            }
        }

        public virtual void Clear()
        {
            models.Clear();
        }

        public bool Contains(M item)
        {
            return models.Contains(item);
        }

        public void CopyTo(M[] array, int arrayIndex)
        {
            models.CopyTo(array, arrayIndex);
        }

        public IEnumerator<M> GetEnumerator()
        {
            return models.GetEnumerator();
        }

        public virtual bool Remove(M item)
        {
            return models.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return models.GetEnumerator();
        }
    }

    class ModelWithViewCollection<M, V> : ModelCollection<M> where M : Model where V : View<M>, new()
    {
        Dictionary<M, GameObject> modelToView = new Dictionary<M, GameObject>();

        public override void Add(M item)
        {
            GameObject view = new GameObject();
            view.AddComponent<V>().Model = item;
            modelToView.Add(item, view);
            base.Add(item);
        }

        public override void Clear()
        {
            foreach (GameObject view in modelToView.Values)
            {
                GameObject.Destroy(view);
            }
            modelToView.Clear();
            base.Clear();
        }

        public override bool Remove(M item)
        {
            GameObject.Destroy(modelToView[item]);
            return modelToView.Remove(item) && base.Remove(item);
        }
    }
}
