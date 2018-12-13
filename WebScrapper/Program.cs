using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using WebScrapper.models;

namespace WebScrapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("webdriver.gecko.driver", "/usr/local/Cellar/geckodriver/0.23.0");
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            DriverExtension webDriver = new DriverExtension();
            FirefoxOptions options = new FirefoxOptions();
            //options.AddArguments("--headless");
            //using (var driver = new FirefoxDriver(options))
            using (var driver = new ChromeDriver(@"/Users/namishmaheshwari/Documents/work/"))
            {
                // Go to the home page
                driver.Navigate().GoToUrl("http://oweb7-vm.usc.edu/CommBookNg/schools");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(55);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
                string selector = $"/html/body/app-root/div/div[2]/app-school-list/mat-card/mat-nav-list/mat-list-item";
                var elements = driver.FindElementsByXPath(selector);
                List<School> schools = new List<School>();
                foreach (IWebElement element in elements)
                {
                    string selectorSchoolName = "div/div[2]/span[1]/span";
                    string selectorSchoolDean = "div/div[2]/span[2]";
                    School school = new School();
                    school.Name = element.FindElement(By.XPath(selectorSchoolName)).Text;
                    school.Dean = element.FindElement(By.XPath(selectorSchoolDean)).Text.Split(":")[1].Trim();

                    element.Click();

                    string selectorStream = "/html/body/app-root/div/div[2]/app-degree-list/mat-card/mat-nav-list/mat-list-item";
                    var elementsStream = driver.FindElementsByXPath(selectorStream);
                    List<Stream> streamList = new List<Stream>();
                    for (int i = 1; i <= elementsStream.Count;i++){
                        Stream stream = new Stream();
                        stream.Name = driver.FindElementByXPath(selectorStream+"["+i+"]"+ "/div/div[2]/span").Text;

                        driver.FindElementByXPath(selectorStream + "[" + i + "]").Click();

                        string selectorMajor = "/html/body/app-root/div/div[2]/app-major-list/mat-card/mat-nav-list/mat-list-item";
                        var elementsMajor = driver.FindElementsByXPath(selectorMajor);
                        List<Major> majorList = new List<Major>();
                        for (int majorCount = 1; majorCount <= elementsMajor.Count;majorCount++){
                            Major major= new Major();
                            major.Name = driver.FindElementByXPath(selectorMajor+"["+majorCount+"]"+"/div/div[2]/span").Text;
                            webDriver.ClickElement(driver, By.XPath(selectorMajor + "[" + majorCount + "]"), 100);
                            driver.FindElementByXPath(selectorMajor + "[" + majorCount + "]").Click();


                            string selectorStudent = "/html/body/app-root/div/div[2]/app-degree-candidate-list/mat-card/mat-nav-list/mat-list-item";
                            var elementsStudent = driver.FindElementsByXPath(selectorStudent);
                            //var elementsStudent = webDriver.FindElements(driver, By.XPath(selectorStudent), 1000);
                            List<Student> studentList = new List<Student>();
                            for (int studentCount = 1; studentCount <= elementsStudent.Count; studentCount++)
                            {
                                Student student = new Student();

                                webDriver.ClickElement(driver, By.XPath(selectorStudent + "[" + studentCount + "]"), 1000);
                                driver.FindElementByXPath(selectorStudent+"["+studentCount+"]").Click();
                                string selectorStudentDetails = "/html/body/app-root/div/div[2]/app-degree-candidate-detail/mat-card/mat-card-content/div/div/mat-card";

                                webDriver.FindElement(driver, By.XPath(selectorStudentDetails + "/mat-card-title"), 1000);
                                student.Name= driver.FindElementByXPath(selectorStudentDetails+"/mat-card-title").Text;

                                string selectorStudentMajor = "/mat-list[1]/mat-list-item";
                                webDriver.FindElements(driver, By.XPath(selectorStudentDetails + selectorStudentMajor), 1000);
                                var elementsStudentMajors = driver.FindElementsByXPath(selectorStudentDetails + selectorStudentMajor);
                                foreach(IWebElement ele in elementsStudentMajors){
                                    webDriver.FindElement(driver, ele, By.XPath("div"), 1000);
                                    string maj=ele.FindElement(By.XPath("div")).Text;
                                    student.Major.Add(maj);
                                }

                                string selectorStudentSchool = "/mat-list[2]/mat-list-item";
                                webDriver.FindElements(driver,By.XPath(selectorStudentDetails + selectorStudentSchool),1000);
                                var elementsStudentSchools = driver.FindElementsByXPath(selectorStudentDetails + selectorStudentSchool);
                                foreach (IWebElement ele in elementsStudentSchools)
                                {
                                    webDriver.FindElement(driver, ele, By.XPath("div"), 1000);
                                    string sch = ele.FindElement(By.XPath("div")).Text;
                                    student.School.Add(sch);
                                }

                                string selectorStudentDegree = "/mat-list[3]/mat-list-item";
                                webDriver.FindElements(driver, By.XPath(selectorStudentDetails + selectorStudentDegree), 1000);
                                var elementsStudentDegree = driver.FindElementsByXPath(selectorStudentDetails + selectorStudentDegree);
                                foreach (IWebElement ele in elementsStudentDegree)
                                {
                                    webDriver.FindElement(driver,ele,By.XPath("div"),1000);
                                    string deg=ele.FindElement(By.XPath("div")).Text;
                                    student.Degree.Add(deg);
                                }

                                driver.Navigate().Back();
                                //driver.FindElementByXPath("/html/body/app-root/div/div[1]/mat-toolbar/mat-toolbar-row[2]/div/div[1]/button").Click();
                                studentList.Add(student);
                            }
                            driver.Navigate().Back();
                            //webDriver.ClickElement(driver, By.XPath("/html/body/app-root/div/div[1]/mat-toolbar/mat-toolbar-row[2]/div/div[1]/button"), 1000).Click();
                            //driver.FindElementByXPath("/html/body/app-root/div/div[1]/mat-toolbar/mat-toolbar-row[2]/div/div[1]/button").Click();
                            major.Students = studentList;
                            majorList.Add(major);
                        }
                        driver.Navigate().Back();
                        //webDriver.ClickElement(driver, By.XPath("/html/body/app-root/div/div[1]/mat-toolbar/mat-toolbar-row[2]/div/div[1]/button"),1000).Click();
                        //driver.FindElementByXPath("/html/body/app-root/div/div[1]/mat-toolbar/mat-toolbar-row[2]/div/div[1]/button").Click();
                        stream.Majors = majorList;
                        streamList.Add(stream);
                    }
                    driver.Navigate().Back();
                    //driver.FindElementByXPath("/html/body/app-root/div/div[1]/mat-toolbar/mat-toolbar-row[2]/div/div[1]/button/span/mat-icon").Click();
                    school.Streams = streamList;
                    schools.Add(school);
                    break;
                }


                System.IO.File.WriteAllText(@"/Users/namishmaheshwari/Documents/work/student-details-new.txt", string.Join(" \n ", schools));

                foreach (School school in schools){
                    Console.WriteLine(school.Name);
                    Console.WriteLine(school.Dean);
                    foreach(Stream stream in school.Streams){
                        Console.WriteLine(stream.Name);
                        foreach(Major major in stream.Majors){
                            Console.WriteLine(major.Name);
                            foreach(Student student in major.Students){
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
