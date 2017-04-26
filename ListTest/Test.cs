using NUnit.Framework;
using System;
using System.Linq;
using MyCollections;
namespace ListTest
{
	[TestFixture]
	public class Test
	{
		private static MySortedList<int> myset;

		[TestFixtureSetUp]
		public static void init()
		{
			myset = new MySortedList<int>();
		}

		[Test]
		public void AddAndGetFirstItemTest()
		{
			myset.Add(1);
			int expected = 1;
			int actual = myset.getElementByIndex(0);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void AddAndGetElementOutOfCountRange()
		{
			myset.Add(1);
			myset.Add(2);
			int actual = myset.getElementByIndex(myset.size() + 2);
		}

	    [Test]
	    public void clearCollectionAndGetSize()
	    {
	        myset.Clear();
	        int expected = 0;
	        int actual = myset.size();
	        Assert.AreEqual(expected, actual);
	    }

	    [Test]
	    public void testIfCollectionContainsElement()
	    {
	        myset.Add(2);
	        Assert.IsTrue(myset.Contains(2));
	    }

	    [Test]
	    public void TestIfCollectionContainsElementThatWasNotPut()
	    {
	        myset.Add(1);
	        Assert.IsFalse(myset.Contains(20));
	    }

	    [Test]
	    public void testRemovingObjectFromCollection()
	    {
	        int element = 30;
	        myset.Add(element);
	        myset.Add(element+1);
	        myset.Add(element+2);

	        myset.Remove(element);
	        Assert.IsFalse(myset.Contains(element));
	    }

	    [Test]
	    public void testDeleteByIndexWhichIsInRange()
	    {
	        myset.Add(1);
	        myset.Add(2);
	        myset.Add(3);
	        myset.Add(4);

	        myset.deleteElementByIndex(0);
	        Assert.IsFalse(myset.Contains(1));
	    }

	    [Test]
	    [ExpectedException(typeof(IndexOutOfRangeException))]
	    public void testDeleteByIndexWhichIsOutOfRange()
	    {
	        myset.Add(1);
	        myset.deleteElementByIndex(30);
	    }
	}
}

