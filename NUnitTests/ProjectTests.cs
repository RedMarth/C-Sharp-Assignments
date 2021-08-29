using CST8333ProjectByJonathanCordingley.Controllers;
using CST8333ProjectByJonathanCordingley.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTests
{
    /*This class is used to hold NUnit test cases.
     * By Jonathan Cordingley
     */
    /// <summary>
    /// This class is used to hold NUnit test cases.
    /// By Jonathan Cordingley
    /// </summary>
    [TestFixture]
    public class ProjectTests
    {

        /*This method is an NUnit test case which tests that the CovidController.sort(string sortOrder, List<Covid> list) method
         * works as expected when sorting by Covid.id.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is an NUnit test case which tests that the CovidController.sort(string sortOrder, List<Covid> list) method
        /// works as expected when sorting by Covid.id.
        /// By Jonathan Cordingley
        /// </summary>
        [Test]
        public void testSortID()

        {
            List<Covid> CovidListTest = new List<Covid> { };
            CovidController ControllerTest = new CovidController();

            CovidListTest.Clear();
            CovidListTest.Add(new Covid(2, 2, "2", "2", new System.DateTime(2222, 1, 11), 2, 2, 2, 2, 2, 2.0));
            CovidListTest.Add(new Covid(1, 1, "1", "1", new System.DateTime(1111, 1, 11), 1, 1, 1, 1, 1, 1.0));
            CovidListTest.Add(new Covid(4, 4, "4", "4", new System.DateTime(4444, 1, 11), 4, 4, 4, 4, 4, 4.0));
            CovidListTest.Add(new Covid(3, 3, "3", "3", new System.DateTime(3333, 1, 11), 3, 3, 3, 3, 3, 3.0));

            ControllerTest.sort("", CovidListTest);

            Assert.AreEqual(1, CovidListTest[0].id);
            Assert.AreEqual(2, CovidListTest[1].id);
            Assert.AreEqual(3, CovidListTest[2].id);
            Assert.AreEqual(4, CovidListTest[3].id);
        }

        /*This method is an NUnit test case which tests that the CovidController.sort(string sortOrder, List<Covid> list) method
        * works as expected when sorting by Covid.prname.
        * By Jonathan Cordingley
        */
        /// <summary>
        /// This method is an NUnit test case which tests that the CovidController.sort(string sortOrder, List<Covid> list) method
        /// works as expected when sorting by Covid.prname.
        /// By Jonathan Cordingley
        /// </summary>
        [Test]
        public void testSortPrname()

        {
            List<Covid> CovidListTest = new List<Covid> { };
            CovidController ControllerTest = new CovidController();

            CovidListTest.Clear();
            CovidListTest.Add(new Covid(2, 2, "2", "2", new System.DateTime(2222, 1, 11), 2, 2, 2, 2, 2, 2.0));
            CovidListTest.Add(new Covid(1, 1, "1", "1", new System.DateTime(1111, 1, 11), 1, 1, 1, 1, 1, 1.0));
            CovidListTest.Add(new Covid(4, 4, "4", "4", new System.DateTime(4444, 1, 11), 4, 4, 4, 4, 4, 4.0));
            CovidListTest.Add(new Covid(3, 3, "3", "3", new System.DateTime(3333, 1, 11), 3, 3, 3, 3, 3, 3.0));

            ControllerTest.sort("prname", CovidListTest);

            Assert.AreSame("1", CovidListTest[0].prname);
            Assert.AreSame("2", CovidListTest[1].prname);
            Assert.AreSame("3", CovidListTest[2].prname);
            Assert.AreSame("4", CovidListTest[3].prname);
        }

        /*This method is an NUnit test case which tests that the CovidController.sort(string sortOrder, List<Covid> list) method
         * works as expected when sorting by Covid.numtotal.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is an NUnit test case which tests that the CovidController.sort(string sortOrder, List<Covid> list) method
        /// works as expected when sorting by Covid.numtotal.
        /// By Jonathan Cordingley
        /// </summary>
        [Test]
        public void testSortNumtotal()

        {
            List<Covid> CovidListTest = new List<Covid> { };
            CovidController ControllerTest = new CovidController();

            CovidListTest.Clear();
            CovidListTest.Add(new Covid(2, 2, "2", "2", new System.DateTime(2222, 1, 11), 2, 2, 2, 2, 2, 2.0));
            CovidListTest.Add(new Covid(1, 1, "1", "1", new System.DateTime(1111, 1, 11), 1, 1, 1, 1, 1, 1.0));
            CovidListTest.Add(new Covid(4, 4, "4", "4", new System.DateTime(4444, 1, 11), 4, 4, 4, 4, 4, 4.0));
            CovidListTest.Add(new Covid(3, 3, "3", "3", new System.DateTime(3333, 1, 11), 3, 3, 3, 3, 3, 3.0));

            ControllerTest.sort("numtotal", CovidListTest);

            Assert.AreEqual(1, CovidListTest[0].numtotal);
            Assert.AreEqual(2, CovidListTest[1].numtotal);
            Assert.AreEqual(3, CovidListTest[2].numtotal);
            Assert.AreEqual(4, CovidListTest[3].numtotal);
        }
    }
}
