using System;
using System.IO;


namespace SqlBuilder
{
   public class Program
    {
        static void Main()
        {
            Console.WriteLine("Please Select Format? 1 , 2 or 3");
            int format = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please provide path to text file: ");
            string path = Console.ReadLine();
            Console.WriteLine("Please provide id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            switch (format)
            {
                case 1:
                    ConvertFormat1ToSql(path, id);
                    break;
                case 2:
                    ConvertFormat2ToSql(path, id);
                    break;
                case 3:
                    ConvertFormat3ToSql(path, id);
                    break;
                default:
                    Console.WriteLine("Format doesn't exist");
                    break;
            }

           
        }

        public static void ConvertFormat1ToSql(string path, int id)
        {
            try
            {
                using (StreamReader sr = new StreamReader(File.OpenRead(path)))
                {

                    string[] myA = CleanFormat1(sr);

                    string newpath = ConvertPathToSql(path);
                    using (StreamWriter sw = new StreamWriter(File.Create(newpath)))
                    {

                        int x = 0;
                        for (int i = 0; i < myA.Length / 5; i++)
                            sw.WriteLine("insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values({0}, '{1}', '{2}', '{3}', '{4}', {5})", id++, myA[x++], myA[x++], myA[x++], myA[x++], myA[x++].Trim() == "Y"? true:false);

                        Console.WriteLine("Conversion Successfull!!!");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
              public static void ConvertFormat2ToSql(string path, int id)
        {
           try{
                using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                
                string[] myA = CleanFormat2(sr);

                string newpath = ConvertPathToSql(path);
                using (StreamWriter sw = new StreamWriter(File.Create(newpath)))
                {

                    int x = 0;
                    for (int i = 0; i < myA.Length / 5; i++)
                        sw.WriteLine("insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values({0}, '{1}', '{2}', '{5}', '{3}', {4})", id++, char.ToUpper(myA[x].Trim('"')[0]) + myA[x++].Substring(2).ToLower().Trim('"'), char.ToUpper(myA[x].Trim('"')[0]) + myA[x++].Substring(2).ToLower().Trim('"'), myA[x].Substring(9, 2)  + "/" + myA[x].Substring(6, 2) + "/" + myA[x++].Substring(1, 4), myA[x++].Trim('"') == "Married" ? true: false , myA[x++].Trim('\r').Trim('"') == "Male"? 'M': 'F');
                    Console.WriteLine("Conversion Successfull!!!");

                }
            }
        }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
}

public static void ConvertFormat3ToSql(string path, int id)
{
   try{
    using (StreamReader sr = new StreamReader(File.OpenRead(path)))
    {
        string[] myA = CleanFormat3(sr);


        string newpath = ConvertPathToSql(path);
        using (StreamWriter sw = new StreamWriter(File.Create(newpath)))
        {

            int x = 0;

            for (int i = 0; i < myA.Length / 5; i++)
                sw.WriteLine("insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values({0}, '{1}', '{2}', '{7}', '{3}/{4}/{5}', {6})", id++, char.ToUpper(myA[x].Trim('\r')[0]) + myA[x++].Trim('\r').Substring(1).ToLower(), char.ToUpper(myA[x].Trim('\r')[0]) + myA[x++].Substring(1).ToLower().Trim('\r'), myA[x++],myA[x].Length == 3 ? checkmonth(myA[x++]) : "Invalid Month"  , myA[x++].Trim('\r'), myA[x++].Trim() == "yes" ? true : false, myA[x++].Trim('\r') == "male"? "M" : "F");
            Console.WriteLine("Conversion Successfull!!!");


        }

    }
}
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static string checkmonth(string v)
        {
            switch (v)
            {
                case "Jan":
                    return "01";
                case "Feb":
                    return "02";
                case "Mar":
                    return "03";
                case "Apr":
                    return "04";
                case "May":
                    return "05";
                case "Jun":
                    return "06";
                case "Jul":
                    return "07";
                case "Aug":
                    return "08";
                case "Sep":
                    return "09";
                case "Oct":
                    return "10";
                case "Nov":
                    return "11";
                case "Dec":
                    return "12";
                default:
                    return "Invalid Month";

            }
        }

        private static string ConvertPathToSql(string path) { 
          return   path.Replace(".txt", ".sql");
    }
        
    private static string[] CleanFormat1 (StreamReader strmrdr) {
               
                string my = strmrdr.ReadToEnd();
                my = my.Replace("\n", ", ");
                my = my.Replace(",", "");
                return my.Split(' ');
    
    }
    
    private static string[] CleanFormat2 (StreamReader strmrdr) {
    
                string my = strmrdr.ReadToEnd();
                my = my.Replace("\n", "|");
                my = my.Replace("|", " ");
                my = my.Replace('-', '/');
                return my.Split(' ');
    }   
        
    private static string[] CleanFormat3 (StreamReader strmrdr) {  
        
                string my = strmrdr.ReadToEnd();
                my = my.Replace("\n", " ");
                my = my.Replace("#", "");
                string thingstoremove = " name: |surname: |dob: |married: |gender: ";
                my = System.Text.RegularExpressions.Regex.Replace(my ,thingstoremove, "");
                my = my.Trim();
                return my.Split(' ');
    }
    }
}
