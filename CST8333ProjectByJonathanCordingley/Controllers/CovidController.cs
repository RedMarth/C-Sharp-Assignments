using CST8333ProjectByJonathanCordingley.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.IO;

namespace CST8333ProjectByJonathanCordingley.Controllers
/* This class is a controller which will parse data from the dataset, and render a view to display the records.
By Jonathan Cordingley 
*/
{/// <summary>
/// This class is a controller which will parse data from the dataset, and render a view to display the records.
/// By Jonathan Cordingley
/// </summary>
    public class CovidController : Controller
    {
        public static List<Covid> CovidList = new List<Covid> { };
        public string pathToLoad = @"D:\Documents\Algonquin\Level 4\CST8333 - Programming Language Research Project\covid19-download.csv";
        public string pathToWrite = @"D:\Documents\Algonquin\Level 4\CST8333 - Programming Language Research Project\new-covid-data.csv";
        public static bool sorted1 = false;
        public static bool sorted2 = false;
        public static string firstSort;




        public IOrderedEnumerable<Covid> ordered = CovidList.OrderBy(record => record.id);


        /* This method checks if the list of records is empty. If so, imports the dataset from the specified path 
         * and adds each record to the list as a Covid object
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method checks if the list of records is empty. If so, imports the dataset from the specified path 
        /// and adds each record to the list as a Covid object
        /// By Jonathan Cordingley 
        /// </summary>
        public void loadDataFromCSV()
        {

            if (!CovidList.Any())
            {
                try
                {
                    /*FileIO - a TextFieldParser object from the Microsoft.VisualBasic.FileIO library is used to import the dataset into the program.
                    By Jonathan Cordingley 
                    */
                    TextFieldParser textFieldParser = new TextFieldParser(pathToLoad);

                    using (textFieldParser)
                    {
                        textFieldParser.TextFieldType = FieldType.Delimited;
                        textFieldParser.SetDelimiters(",");

                        //to skip the row containing the headers, or it will throw an error due to incorrect datatype
                        string[] firstRow = textFieldParser.ReadFields();

                        /*This while loop is used to parse the data from the dataset, and instantiate a Covid object, which is used to represent a record from the dataset.
                        This object is added to the list. The loop will continue as long as the end of the dataset is not reached yet.
                        By Jonathan Cordingley 
                        */
                        while (!textFieldParser.EndOfData)
                            for (int i = 1; i <= 100; i++)
                            {
                                string[] fields = textFieldParser.ReadFields();
                                CovidList.Add(new Covid(i, int.Parse(fields[0]), fields[1], fields[2], DateTime.Parse(fields[3]), int.Parse(fields[5]), int.Parse(fields[6]),
                                                 int.Parse(fields[7]), int.Parse(fields[8]), int.Parse(fields[13]), double.Parse(fields[15])));
                            }
                    }
/*                    CovidList.RemoveAt(0);*/
                }

                /*If any exceptions are found, this will print the name of the exception in the Debug log. 
                  This is used to handle FileNotFoundExceptions in the event that the program cannot load the file from the specified path. 
                  By Jonathan Cordingley
                */
                catch (Exception e)
                {
                    Debug.WriteLine("The exception is : " + e.ToString());
                }
            }
        }
        /*This method deletes all objects from the list which holds the records.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method deletes all objects from the list which holds the records.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="list"></param>
        public void clearList(List<Covid> list)
        {
            list.Clear();
            sorted1 = false;
            sorted2 = false;
            firstSort = "";
        }

        /* This method is used to export the data in the list as a new CSV file. It will use the path specified in the pathToWrite class variable.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is used to export the data in the list as a new CSV file. It will use the path specified in the pathToWrite class variable.
        /// By Jonathan Cordingley
        /// </summary>
        public void writeDataToCSV()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(pathToWrite, FileMode.Create, FileAccess.Write)))
                {
                    writer.WriteLine("id,pruid,prname,prnameFR,date,numconf,numprob,numdeaths,numtotal,numtoday,ratetotal");

                    foreach (Covid record in CovidList)
                    {
                        writer.WriteLine(record.id + "," + record.pruid + "," + record.prname + "," + record.prnameFR + "," + record.date + ","
                            + record.numconf + "," + record.numprob + "," + record.numdeaths + "," + record.numtotal + "," + record.numtoday + "," + record.ratetotal);
                    }

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("The exception is : " + e.ToString());
            }
        }

        /*This method is called when the program is launched. It returns a view which shows the entire list of records, and options for the user.
        If the list is empty, it will be loaded. It sorts the list of records by a field when the user clicks that field.
        By Jonathan Cordingley 
        */
        /// <summary>
        /// This method is called when the program is launched. It returns a view which shows the entire list of records, and options for the user.
        /// If the list is empty, it will be loaded. It sorts the list of records by a field when the user clicks that field.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <returns>Returns a view which shows the entire list of records, and options for the user.</returns>
        public ActionResult Index(string sortOrder)
        {
            loadDataFromCSV();
            CovidList = sort(sortOrder, CovidList);            
            return View(CovidList);
        }

        /*This method is used to sort the list by the parameter the user clicks on in the Index view. The user can sort by a second parameter by clicking on it. 
         * The third click will reset the list to be sorted by id. It is called by the Index method.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is used to sort the list by the parameter the user clicks on in the Index view. The user can sort by a second parameter by clicking on it
        /// The third click will reset the list to be sorted by id.It is called by the Index method.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="list"></param>
        /// 

        public List<Covid> sort(string sortOrder, List<Covid> list)
        {
            ViewBag.idSort = sortOrder == "id" ? "" : "id";
            ViewBag.pruidSort = sortOrder == "pruid" ? "" : "pruid";
            ViewBag.prnameSort = sortOrder == "prname" ? "" : "prname";
            ViewBag.prnameFRSort = sortOrder == "prnameFR" ? "" : "prnameFR";
            ViewBag.dateSort = sortOrder == "date" ? "" : "date";
            ViewBag.numconfSort = sortOrder == "numconf" ? "" : "numconf";
            ViewBag.numprobSort = sortOrder == "numprob" ? "" : "numprob";
            ViewBag.numdeathsSort = sortOrder == "numdeaths" ? "" : "numdeaths";
            ViewBag.numtotalSort = sortOrder == "numtotal" ? "" : "numtotal";
            ViewBag.numtodaySort = sortOrder == "numtoday" ? "" : "numtoday";
            ViewBag.ratetotalSort = sortOrder == "ratetotal" ? "" : "ratetotal";

            var sortedList = new List<Covid>(list);

            //By Jonathan Cordingley
            if (sorted1 == false)
            {
                switch (sortOrder)
                {
                    case "id":
                        sortedList = list.OrderBy(record => record.id).ToList();
                        sorted1 = true;
                        firstSort = "id";
                        break;

                    case "pruid":
                        sortedList = list.OrderBy(record => record.pruid).ToList();
                        sorted1 = true;
                        firstSort = "pruid";
                        break;

                    case "prname":
                        sortedList = list.OrderBy(record => record.prname).ToList();
                        sorted1 = true;
                        firstSort = "prname";
                        break;

                    case "prnameFR":
                        sortedList = list.OrderBy(record => record.prnameFR).ToList();
                        sorted1 = true;
                        firstSort = "prnameFR";
                        break;

                    case "date":
                        sortedList = list.OrderBy(record => record.date).ToList();
                        sorted1 = true;
                        firstSort = "date";
                        break;

                    case "numconf":
                        sortedList = list.OrderBy(record => record.numconf).ToList();
                        sorted1 = true;
                        firstSort = "numconf";
                        break;

                    case "numprob":
                        sortedList = list.OrderBy(record => record.numprob).ToList();
                        sorted1 = true;
                        firstSort = "numprob";
                        break;

                    case "numdeaths":
                        sortedList = list.OrderBy(record => record.numdeaths).ToList();
                        sorted1 = true;
                        firstSort = "numdeaths";
                        break;

                    case "numtotal":
                        sortedList = list.OrderBy(record => record.numtotal).ToList();
                        sorted1 = true;
                        firstSort = "numtotal";
                        break;

                    case "numtoday":
                        sortedList = list.OrderBy(record => record.numtoday).ToList();
                        sorted1 = true;
                        firstSort = "numtoday";
                        break;

                    case "ratetotal":
                        sortedList = list.OrderBy(record => record.ratetotal).ToList();
                        sorted1 = true;
                        firstSort = "ratetotal";
                        break;

                    //default case is sorted as ID but does not change sorted1 to true.
                    //By Jonathan Cordingley
                    default:
                        sortedList = list.OrderBy(record => record.id).ToList();
                        break;
                }
                return sortedList;
            }

            // By Jonathan Cordingley
            else if (sorted1 == true && sorted2 == false)
            {

                switch (firstSort)
                {
                    case "id":

                        switch(sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.id).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;

                        }

                        break;
                
                    case "pruid":

                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.pruid).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;

                        }

                        break;
                        
                    case "prname":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.prname).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;

                        }

                        break;

                    case "prnameFR":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.prnameFR).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                                break;

                    case "date":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.date).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                        break;

                    case "numconf":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.numconf).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                        break;

                    case "numprob":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.numprob).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                        break;
                    case "numdeaths":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.numdeaths).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                        break;
                    case "numtotal":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.numtotal).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                        break;
                    case "numtoday":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.numtoday).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                        break;
                    case "ratetotal":
                        switch (sortOrder)
                        {
                            case "id":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.id).ToList();
                                sorted2 = true;
                                break;

                            case "pruid":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "prname":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.prname).ToList();
                                sorted2 = true;
                                break;

                            case "prnameFR":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.pruid).ToList();
                                sorted2 = true;
                                break;

                            case "date":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.date).ToList();
                                sorted2 = true;
                                break;

                            case "numconf":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.numconf).ToList();
                                sorted2 = true;
                                break;

                            case "numprob":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.numprob).ToList();
                                sorted2 = true;
                                break;

                            case "numdeaths":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.numdeaths).ToList();
                                sorted2 = true;
                                break;

                            case "numtotal":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.numtotal).ToList();
                                sorted2 = true;
                                break;

                            case "numtoday":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.numtoday).ToList();
                                sorted2 = true;
                                break;

                            case "ratetotal":
                                sortedList = list.OrderBy(record => record.ratetotal).ThenBy(record => record.ratetotal).ToList();
                                sorted2 = true;
                                break;
                        }
                        break;
                        
                }

                return sortedList;
            }

            // By Jonathan Cordingley
            else { 

                sorted1 = false;
                sorted2 = false;
                firstSort = "";
                sortedList = list.OrderBy(record => record.id).ToList();
                return sortedList;
            }
        }

        /* This method returns a view which informs the user that the list has been reloaded. It clears the list, and when the user returns to the
        * Index view, the list is reloaded.
        * By Jonathan Cordingley
        */
        /// <summary>
        /// This method returns a view which informs the user that the list has been reloaded. It clears the list, and when the user returns to the
        /// Index view, the list is reloaded.
        /// By Jonathan Cordingley 
        /// </summary>
        public ActionResult Reload()
        {
            clearList(CovidList);
            return View();
        }
        /* This method returns a view which informs the user that the data has been exported as a CSV file.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method returns a view which informs the user that the data has been exported as a CSV file.
        /// By Jonathan Cordingley
        /// </summary>
        /// <returns>A view which informs the user that the data has been exported as a CSV file.</returns>
        public ActionResult Export()
        {
            writeDataToCSV();
            return View();
        }
        /* This method returns a view which allows the user to input details and create a new record.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method returns a view which allows the user to input details and create a new record.
        /// By Jonathan Cordingley
        /// </summary>
        /// <returns>A view which allows the user to input details and create a new record.</returns>
        public ActionResult Create()
        {
            return View();
        }
        /* This method is called when the user clicks the create button in the Create view. It instantiates
         * a new Covid object with the specified values, and adds it to the list. Then it redirects the user
         * to the Index view.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is called when the user clicks the create button in the Create view. It instantiates
        /// a new Covid object with the specified values, and adds it to the list. Then it redirects the user
        /// to the Index view.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="covidNew"></param>
        /// <returns>The Index view.</returns>
        [HttpPost]
        public ActionResult Create(Covid covidNew)
        {
            covidNew.id = CovidList[CovidList.Count - 1].id + 1;
            CovidList.Add(covidNew);
            return RedirectToAction("Index");
        }
        /* This method is called when the user clicks the Edit button next to any record. It takes the id of that record
         * and searches for an object in the CovidList with the same id. Then it returns the view which shows the details
         * of that record, so the user may edit them.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is called when the user clicks the Edit button next to any record. It takes the id of that record
        /// and searches for an object in the CovidList with the same id.Then it returns the view which shows the details
        /// of that record, so the user may edit them.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A view which shows the details of the specified record, so the user may edit them.</returns>
        public ActionResult Edit(int id)
        {
            var covidToEdit = CovidList.Where(x => x.id == id).FirstOrDefault();
            return View(covidToEdit);
        }

        /* This method is called when the user clicks the save button in the Edit view.
         * It removes the original object being edited, and replaces it with the newly
         * edited one. It then sorts the list by id, and returns the user to the Index view.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is called when the user clicks the save button in the Edit view.
        /// It removes the original object being edited, and replaces it with the newly
        /// edited one. It then sorts the list by id, and returns the user to the Index view.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="covidToEdit"></param>
        /// <returns>The Index View.</returns>
        [HttpPost]
        public ActionResult Edit(Covid covidToEdit)
        {
            var covid = CovidList.Where(x => x.id == covidToEdit.id).FirstOrDefault();
            CovidList.Remove((Covid)covid);
            CovidList.Add(covidToEdit);
            CovidList.Sort((x, y) => x.id.CompareTo(y.id));
            return RedirectToAction("Index");
        }

        /* This method is called when the user clicks the Delete button next to any record. It takes the id of that record
         * and searches for an object in the CovidList with the same id. Then it returns the view which shows the details
         * of that record, and the user is asked if they want to delete it.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is called when the user clicks the Delete button next to any record. It takes the id of that record
        /// and searches for an object in the CovidList with the same id. Then it returns the view which shows the details
        /// of that record, and the user is asked if they want to delete it.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A view which shows the details of the specified record, so the user may delete it.</returns>
        public ActionResult Delete(int id)
        {
            var covidToDelete = CovidList.Where(x => x.id == id).FirstOrDefault();
            return View(covidToDelete);
        }

        /* This method is called when the user clicks the Delete button in the Delete view.
         * It removes the object. It then sorts the list by id, and returns the user to the Index view.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is called when the user clicks the Delete button in the Delete view.
        /// It removes the object. It then sorts the list by id, and returns the user to the Index view.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="covidToEdit"></param>
        /// <returns>The Index View.</returns>
        [HttpPost]
        public ActionResult Delete(Covid covidToDelete)
        {
            var delete = CovidList.Where(x => x.id == covidToDelete.id).FirstOrDefault();
            CovidList.Remove((Covid)delete);
            CovidList.Sort((x, y) => x.id.CompareTo(y.id));
            return RedirectToAction("Index");
        }
        /* This method is called when the user clicks the Details button next to any record.
         * It takes the id of that record and searches for an object in the CovidList with the same id. 
         * Then it returns the view which shows the details of that record.
         * By Jonathan Cordingley
         */
        /// <summary>
        /// This method is called when the user clicks the Details button next to any record.
        /// It takes the id of that record and searches for an object in the CovidList with the same id.
        /// Then it returns the view which shows the details of that record.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Details view, which shows the details of the record.</returns>
        public ActionResult Details(int id)
        {
            var detail = CovidList.Where(x => x.id == id).FirstOrDefault();
            return View(detail);
        }

    }
}
