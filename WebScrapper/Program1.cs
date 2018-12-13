using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using WebScrapper.models;

namespace WebScrapper
{
    class Program1
    {
        public static void Main1(string[] args)
        {
            DriverExtension webDriver = new DriverExtension();
            using (var driver = new ChromeDriver(@"/Users/namishmaheshwari/Documents/work/"))
            {
                // Go to the home page
                driver.Navigate().GoToUrl("https://camel2.usc.edu/commbook/");
                driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
                IWebElement user1 = driver.FindElementByCssSelector("#content > div > div.panel-body > div");

                string selector = $"//*[@id=\"content\"]/div/div[2]/div/div";
                var elements = driver.FindElementsByXPath(selector);
                List<School> schools = new List<School>();

                foreach (IWebElement element in elements)
                {
                    string[] str = new string[element.FindElements(By.XPath("div[1]/div/a/span")).Count];
                    int i = 0;
                    foreach (IWebElement schoolElement in element.FindElements(By.XPath("div[1]/div/a/span")))
                    {
                        str[i] = schoolElement.Text;
                        i++;
                    }
                    School school = new School();
                    school.Name=str[0];
                    school.Dean = str[1];
                    //click
                    element.FindElement(By.XPath("div[1]/div/a")).Click();

                    List<Stream> streamList=new List<Stream>();
                    foreach (IWebElement stream in element.FindElements(By.XPath("div[2]/div/ul/li"))){
                        Stream stream1 = new Stream();
                        stream1.Name = stream.FindElement(By.XPath("a")).Text;
                        stream.FindElement(By.XPath("a")).Click();

                        string majorSelector= $"//*[@id=\"content\"]/div/div[2]/ul/li";
                        var majorElement = driver.FindElementsByXPath(majorSelector);
                        List<Major> majors = new List<Major>();
                        for (int majorCount = 1; majorCount <= majorElement.Count; majorCount++){
                            IWebElement majorIWebElement = driver.FindElement(By.XPath(majorSelector + "[" + majorCount + "]"));
                            Major major= new Major();
                            major.Name=majorIWebElement.FindElement(By.XPath("a")).Text;
                            majorIWebElement.FindElement(By.XPath("a")).Click();
                            string studentSelector= $"//*[@id=\"content\"]/div/div[2]/ul/li";
                            List<Student> students = new List<Student>();
                            try
                            {
                                webDriver.FindOrThrow(driver, By.XPath("//*[@id=\"content\"]/div/div[2]/ul/span"), 20);
                                Console.WriteLine("No Student Data found for the School:{0} Stream: {1} major:{2}", school.Name, stream1.Name, major.Name);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                var studentElement = driver.FindElementsByXPath(studentSelector);
                                for (int studentCount = 1; studentCount <= studentElement.Count; studentCount++)
                                {

                                    webDriver.ClickElement(driver, By.XPath(studentSelector + "[" + studentCount + "]"), 100);
                                    driver.FindElement(By.XPath(studentSelector + "[" + studentCount + "]")).Click();
                                    Student student = new Student();

                                    webDriver.FindElement(driver, By.XPath("//*[@id=\"content\"]/div/ul/li[1]/p[1]/span/span"), 100);
                                    student.Name = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[1]/p[1]/span/span")).Text;


                                    webDriver.FindElements(driver, By.XPath("//*[@id=\"content\"]/div/ul/li[2]/p/span"), 100);
                                    var majorStudentElement = driver.FindElements(By.XPath("//*[@id=\"content\"]/div/ul/li[2]/p/span"));
                                    foreach (IWebElement majorStudent in majorStudentElement)
                                    {
                                        webDriver.FindElement(driver, majorStudent, By.XPath("span"), 100);
                                        string maj = majorStudent.FindElement(By.XPath("span")).Text;
                                        student.Major.Add(maj);
                                    }

                                    webDriver.FindElements(driver, By.XPath("//*[@id=\"content\"]/div/ul/li[3]/p/span"), 100);
                                    var schoolStudentElement = driver.FindElements(By.XPath("//*[@id=\"content\"]/div/ul/li[3]/p/span"));
                                    foreach (IWebElement schoolStudent in schoolStudentElement)
                                    {
                                        webDriver.FindElement(driver, schoolStudent, By.XPath("span"), 100);
                                        string sch = schoolStudent.FindElement(By.XPath("span")).Text;
                                        student.School.Add(sch);
                                    }

                                    webDriver.FindElements(driver, By.XPath("//*[@id=\"content\"]/div/ul/li[4]/p/span"), 100);
                                    var degreeStudentElement = driver.FindElements(By.XPath("//*[@id=\"content\"]/div/ul/li[4]/p/span"));
                                    foreach (IWebElement degreeStudent in degreeStudentElement)
                                    {
                                        webDriver.FindElement(driver, degreeStudent, By.XPath("span"), 100);
                                        string deg = degreeStudent.FindElement(By.XPath("span")).Text;
                                        student.Degree.Add(deg);
                                    }

                                    string additionalInfoSelector = $"//*[@id=\"content\"]/div/ul/li[5]/p/span";
                                    //webDriver.FindElements(driver, By.XPath(additionalInfoSelector), 1000);
                                    var additionalInfoElement = driver.FindElementsByXPath(additionalInfoSelector);
                                    List<string> additionalInfoList = new List<string>();
                                    foreach (IWebElement additionalInfoIWebElement in additionalInfoElement)
                                    {
                                        webDriver.FindElement(driver, additionalInfoIWebElement, By.XPath("span"), 100);
                                        additionalInfoList.Add(additionalInfoIWebElement.FindElement(By.XPath("span")).Text);
                                    }
                                    student.additionalInfo = additionalInfoList;
                                    students.Add(student);
                                    webDriver.ClickElement(driver, By.XPath("//*[@id=\"breadcrumb\"]/ol/li[3]/a"), 100);
                                    driver.FindElement(By.XPath("//*[@id=\"breadcrumb\"]/ol/li[3]/a")).Click();

                                }
                            }
                            major.Students=students;
                            majors.Add(major);
                            webDriver.ClickElement(driver, By.XPath("//*[@id=\"breadcrumb\"]/ol/li[2]/a"), 100);
                            driver.FindElement(By.XPath("//*[@id=\"breadcrumb\"]/ol/li[2]/a")).Click();

                        }
                        stream1.Majors = majors;
                        streamList.Add(stream1);

                    }
                    school.Streams = streamList;
                    schools.Add(school);
                    break;
                }

                System.IO.File.WriteAllText(@"/Users/namishmaheshwari/Documents/work/student-details.txt", string.Join(" \n ", schools));

                foreach (School school in schools)
                {
                    Console.WriteLine(school.Name);
                    Console.WriteLine(school.Dean);
                    foreach (Stream stream in school.Streams)
                    {
                        Console.WriteLine(stream.Name);
                        foreach (Major major in stream.Majors)
                        {
                            Console.WriteLine(major.Name);
                            foreach (Student student in major.Students)
                            {
                                Console.WriteLine(student.Name);
                                Console.WriteLine(string.Join(",", student.Degree));
                                Console.WriteLine(string.Join(",", student.Major));
                                Console.WriteLine(string.Join(",", student.School));
                                Console.WriteLine(string.Join(",", student.additionalInfo));
                            }
                        }
                    }
                }

            }
        }
    }
}
