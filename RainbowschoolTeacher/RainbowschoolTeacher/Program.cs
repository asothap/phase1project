using System;
using System.IO;

namespace RainbowschoolTeacher
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine(@"Enter the operation to do in teacher record
                                            1. Add                  eg 1-(name):(classsection),
                                            2.Verify 
                                                 using id           eg 2-id:(id)
                                                 using name         eg 2-name:(name)
                                                 using classsection eg 2-classsection:(classsection),
                                            3.Update
                                                 change name         eg 3-(id):name=(name) 
                                                 change classsection eg 3-(id):classsection=(classsection)
                                              No special character allowed");
                string input = Console.ReadLine();
                TeacherRecord teacherinfo = new TeacherRecord();
                string[] values = input.Split(new Char[] { '-', ':' });
                if (values.Length < 3)
                {
                    Console.WriteLine("Required field missing. Validate the input");
                    return;
                }
                switch (values[0])
                {
                    case "1":
                        teacherinfo.AddRecord(values[1], values[2]);
                        break;
                    case "2":
                        teacherinfo.FetchRecord(values[1] + "-" + values[2]);
                        break;
                    case "3":
                        teacherinfo.UpdateRecord(values[1], values[2]);
                        break;
                }
                Console.WriteLine("Please c to continue and other key to stop");
            } while (Console.ReadLine() == "c");
        }
    }

    internal class TeacherRecord
    {
        string fileName = Directory.GetCurrentDirectory() + "\\Data.txt";
        internal void AddRecord(string name, string classsec)
        {
            string datetime = $"{DateTime.Now.DayOfYear}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine($"{datetime},{name},{classsec}");
            }
            Console.WriteLine($"Added {name} with ID {datetime}");
        }
        internal void FetchRecord(string searchvalue)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (searchvalue.Contains("id") && line.Contains(searchvalue.Split('-')[1] + ","))
                {
                    Console.WriteLine(line);
                    break;
                }
                if (searchvalue.Contains("name") && line.Contains("," + searchvalue.Split('-')[1] + ","))
                {
                    Console.WriteLine(line);
                    continue;
                }
                if (searchvalue.Contains("classsection") && line.Contains("," + searchvalue.Split('-')[1]))
                {
                    Console.WriteLine(line);
                    continue;
                }
            }
            Console.WriteLine("Done");
        }
       internal void UpdateRecord(string updatefor, string updatevalue)
        {

            string[] arrLine = File.ReadAllLines(fileName);
            for (int i = 0; i < arrLine.Length; i++)
            {
                if (arrLine[i].Split(',')[0] == (updatefor))
                {
                    if (updatevalue.Contains("name"))
                    {                       
                        arrLine[i] = arrLine[i].Replace(arrLine[i].Split(",")[1], updatevalue.Replace("name=", ""));
                    }
                        if (updatevalue.Contains("classsection"))
                        arrLine[i] = arrLine[i].Replace(arrLine[i].Substring(arrLine[i].LastIndexOf(",") + 1), updatevalue.Replace("classsection=", ""));
                    break;
                }
            }
            File.WriteAllLines(fileName, arrLine);
            Console.WriteLine("Updated");
        }
    }
}
