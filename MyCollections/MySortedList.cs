using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace MyCollections
{
    public class MessageEventArgs : EventArgs
    {
        public string message;
    }

    public delegate void MessageEventHandler(object source, MessageEventArgs arg);

    /// <summary>
    /// Dynamic array with starting index
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    public class MySortedList<T>: ICollection, ICollection<T> where T: IComparable
    {
        /// <summary>
        /// Array for elements
        /// </summary>
        private T[] elements;

        /// <summary>
        /// Boolean for mutable or immutable collection
        /// </summary>
        private bool immutable;

        /// <summary>
        /// Number of items in array
        /// </summary>
        private int count;

        /// <summary>
        /// Identefier for resize array
        /// </summary>
        private int indForDelete;

        public event MessageEventHandler ItemAddedEvent;
        public event MessageEventHandler ItemRemovedEvent;
        public event MessageEventHandler ArrayClearedEvent;
        /// <summary>
        /// Creates new array with starting index = 0 and length = 0.
        /// </summary>
        public MySortedList()
        {
            immutable = false;
            count = 0;
            indForDelete = 0;
            elements = new T[10];
        }

        /// <summary>
        /// Method, which return Enumerator of collection
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void CopyTo(Array array, int index)
        {}

        /// <summary>
        /// Number of elements in collection
        /// </summary>
        int ICollection.Count
        {
            get { return count; }
        }

        public object SyncRoot { get; }
        public bool IsSynchronized { get; }

        /// <summary>
        /// Creating and return new Enumerator for Collection
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            //return elements.Take(count).GetEnumerator();
            return new SortedSetEnumerator(elements, count);
        }

        /// <summary>
        /// Adding element for collection
        /// </summary>
        /// <param name="item">Element which will be added to collection</param>
        public void Add(T item)
        {
            checkForModify();
            if (count == elements.Length)
            {
                T[] temp = new T[count * 2];
                for (int i = 0; i < count; i++)
                {
                    temp[i] = elements[i];
                }
                elements = temp;
            }

            elements[count] = item;
            ItemAdded();
            Array.Sort(elements,0,count);
            ++count;
        }

        /// <summary>
        /// Clear old array and creating new array with size 10
        /// </summary>
        public void Clear()
        {
            checkForModify();
            elements = new T[10];
            count = 0;
            Cleared();
        }

        /// <summary>
        /// Checking if element is contain in collection
        /// </summary>
        /// <param name="item">Element</param>
        public bool Contains(T item)
        {
            return elements.Contains(item);
        }

        /// <summary>
        /// Copying elements from input index to new array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="arrayIndex">Starting index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int j = 0;
            for (int i = arrayIndex; i < count; i++)
            {
                array[j] = elements[i];
                j++;
            }
        }

        /// <summary>
        /// Removing element from collection
        /// </summary>
        /// <param name="item">Input element</param>
        public bool Remove(T item)
        {
            checkForModify();
            int newCount = count;
            bool ind = false;
            T[] temp = elements;

            if (indForDelete == 5)
            {
                elements = new T[elements.Length - 5];
            }
            else
            {
                elements = new T[elements.Length];
            }

            int j = 0;
            for (int i = 0; i < count; i++)
            {
                if (temp[i].Equals(item))
                {
                    --newCount;
                    ++indForDelete;
                    ind = true;
                    ItemRemoved();
                }
                else
                {
                    elements[j] = temp[i];
                    j++;
                }

            }

            count = newCount;

            return ind;
        }

        /// <summary>
        /// Count of elements
        /// </summary>
        int ICollection<T>.Count
        {
            get { return count; }
        }

        public int size() => count;

        public bool IsReadOnly { get; }



        /// <summary>
        /// Class for Enumerator
        /// </summary>
        private class SortedSetEnumerator : IEnumerator<T>
        {
            private int position = -1;
            private T[] elements;
            private T current;
            private int count;

            public SortedSetEnumerator(T[] elements, int count)
            {
                this.elements = elements;
                this.count = count;
            }

            public void Dispose()
            {}

            public bool MoveNext()
            {
                position++;
                return position < count;
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public T Current {
                get { return elements[position]; }
            }
        }

        /// <summary>
        /// Override toString() method
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            string result = "[";
            foreach (T element in this)
            {
                result += element + ",";
            }

            result = result.Substring(0, result.Length - 1);
            result += "]";
            return result;
        }


        //Lab4
        /// <summary>
        /// Return element from collection by Index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>element</returns>
        public T getElementByIndex(int index)
        {
            checkIndex(index);
            T element = elements[index];
            return element;
        }

        //Lab4
        /// <summary>
        /// Delete element from collection by Index
        /// </summary>
        /// <param name="index">index</param>
        public void deleteElementByIndex(int index)
        {
            checkIndex(index);
            T element = elements[index];
            Remove(element);
        }

        //Lab4
        /// <summary>
        /// Tool method for checking if index more than count of elements
        /// </summary>
        /// <param name="index">index</param>
        /// <exception cref="IndexOutOfRangeException">exception</exception>
        private void checkIndex(int index)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException("No element in this Collection");
            }
        }

        //Lab4
        /// <summary>
        /// Checking if collection is IMMUTABLE
        /// </summary>
        /// <returns>immutable</returns>
        public bool isImmutable()
        {
            return immutable;
        }

        //Lab4
        /// <summary>
        /// setting collection mutable
        /// </summary>
        public void setMutable()
        {
            immutable = false;
        }

        //Lab4
        /// <summary>
        /// Setting collection immutable
        /// </summary>
        public void setImmutable()
        {
            immutable = true;
        }

        //Lab4
        /// <summary>
        /// Tool method for immutable checking
        /// </summary>
        /// <exception cref="ReadOnlyCollection">exception</exception>
        private void checkForModify()
        {
            if(immutable)
                throw new ReadOnlyCollection();
        }

        /// <summary>
        /// Item added
        /// </summary>
        private void ItemAdded()
        {
            MessageEventArgs m = new MessageEventArgs();
            if (ItemAddedEvent != null)
            {
                m.message = "New item added! Count = " + (count+1);
                ItemAddedEvent(this, m);
            }
        }

        /// <summary>
        /// Item removed
        /// </summary>
        private void ItemRemoved() {
            MessageEventArgs m = new MessageEventArgs();
            if (ItemRemovedEvent != null)
            {
                m.message = "Item removed! Count = " + (count-1);
                ItemRemovedEvent(this, m);
            }
        }

        /// <summary>
        /// Array cleared
        /// </summary>
        private void Cleared()
        {
            MessageEventArgs m = new MessageEventArgs();
            if (ArrayClearedEvent != null)
            {
                m.message = "Array Cleared!";
                ArrayClearedEvent(this, m);
            }
        }
    }
}