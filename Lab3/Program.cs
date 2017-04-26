using System;
using System.Collections;
using System.Collections.Generic;
using MyCollections;

namespace Lab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MySortedList<int> list = new MySortedList<int>();
            list.ItemAddedEvent += (source, arg) => Console.WriteLine(arg.message);
            list.ItemRemovedEvent += (source, arg) => Console.WriteLine(arg.message);
            list.ArrayClearedEvent += (source, arg) => Console.WriteLine(arg.message);

            list.Add(2);
            list.Add(1);
            list.Add(4);
            list.Add(6);
            list.Add(-5);
            list.Add(7);

            lab4(list);
            //lab3(list);
        }

        private static void lab3(MySortedList<int> list)
        {

            foreach (int number in list)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine(list.Contains(2));
            Console.WriteLine(list.Contains(8));
            Console.WriteLine(list.Remove(4));
            Console.WriteLine(list.Remove(100));

            Console.WriteLine(list);

            int[] array = new int[10];

            list.CopyTo(array, 2);
        }

        private static void lab4(MySortedList<int> list)
        {
            foreach (int number in list)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();
            int n = 0;

            try
            {
                n = 30;
                Console.WriteLine("Element #" + n + " = " + list.getElementByIndex(n));
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot get " + n + " element in Collection");
            }

            try
            {
                n = 2;
                Console.WriteLine("Element #" + n + " = " + list.getElementByIndex(n));
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot get " + 2 + " element in Collection");
            }


            try
            {
                n = 30;
                list.deleteElementByIndex(30);
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot delete " + n + " element. No element in list");
            }


            list.setImmutable();
            try
            {
                list.Add(5);
                Console.WriteLine("Successfull adding");
                Console.WriteLine(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Collection is immutable");
            }



            list.setMutable();
            try
            {
                list.Add(5);
                Console.WriteLine("Successfull adding");
                Console.WriteLine(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Collection is immutable");
            }


        }
    }
}