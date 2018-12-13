using System.Collections.Generic;

namespace WebScrapper.models
{
    public class Student
    {
        public Student(){
            this.Major = new List<string>();
            this.School = new List<string>();
            this.Degree = new List<string>();
            this.additionalInfo = new List<string>();
        }
        public string Name
        {
            get;
            set;
        }
        public List<string> Major
        {
            get;
            set;
        }
        public List<string> School
        {
            get;
            set;
        }
        public List<string> Degree
        {
            get;
            set;
        }
        public List<string> additionalInfo{
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("Student Name: {0}\nStudent Major: {1}\nStudent School: {2}\nStudent Degree: {3}\nStudent AddInfo: {4}\n", 
                                 this.Name, string.Join(",", this.Major.ToString()), string.Join(",", this.School.ToString()),
                                 string.Join(",", this.Degree.ToString()), string.Join(",", this.additionalInfo.ToString()));
        }

    }
}