using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CST8333ProjectByJonathanCordingley.Models
{
    /* This class is used to store the data of one record from the dataset. Each variable is named after the column it will hold the data from [1].
    By Jonathan Cordingley 
    */
    /// <summary>
    /// This class is used to store the data of one record from the dataset. Each variable is named after the column it will hold the data from.
    /// By Jonathan Cordingley
    /// </summary>
    public class Covid
    {
        public int id { get; set; } //Not from dataset - used as a unique identifier for each record.
        public int pruid { get; set; } //A
        public string prname { get; set; } //B
        public string prnameFR { get; set; } //C
        public DateTime date { get; set; } //D
        public int numconf { get; set; } //F
        public int numprob { get; set; } //G
        public int numdeaths { get; set; } //H
        public int numtotal { get; set; } //I
        public int numtoday { get; set; } //N
        public double ratetotal { get; set; } //P

        /*Constructor method for the Covid class. A Covid object represents a record from the dataset.
          By Jonathan Cordingley
        */
        /// <summary>
        /// Constructor method for the Covid class. A Covid object represents a record from the dataset.
        /// By Jonathan Cordingley
        /// </summary>
        public Covid()
        {

        }

        /*Constructor method for the Covid class. A Covid object represents a record from the dataset.
        By Jonathan Cordingley
        */
        /// <summary>
        /// Constructor method for the Covid class. A Covid object represents a record from the dataset.
        /// By Jonathan Cordingley
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pruid"></param>
        /// <param name="prname"></param>
        /// <param name="prnameFR"></param>
        /// <param name="date"></param>
        /// <param name="numconf"></param>
        /// <param name="numprob"></param>
        /// <param name="numdeaths"></param>
        /// <param name="numtotal"></param>
        /// <param name="numtoday"></param>
        /// <param name="ratetotal"></param>
        public Covid(int id, int pruid, string prname, string prnameFR, DateTime date, int numconf, int numprob, int numdeaths, int numtotal, int numtoday, double ratetotal)
        {
            this.id = id;
            this.pruid = pruid;
            this.prname = prname;
            this.prnameFR = prnameFR;
            this.date = date;
            this.numconf = numconf;
            this.numprob = numprob;
            this.numdeaths = numdeaths;
            this.numtotal = numtotal;
            this.numtoday = numtoday;
            this.ratetotal = ratetotal;
        }


    }
}