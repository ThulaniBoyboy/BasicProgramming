using System;
using System.IO;
using System.Collections.Generic;

namespace SqlBuilder
{
    class Program
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
                    Console.ReadLine();
                    break;
            }
           
        }

        public static void ConvertFormat1ToSql(string path, int id)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                
                string[] myA = CleanFormat1(sr);
                
                string newpath = ConvertPathToSql(path);
                using (StreamWriter sw = new StreamWriter(File.Create(newpath)))
                {

                    int x = 0;
                    for (int i = 0; i < myA.Length / 5; i++)
                        sw.WriteLine("insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values({0}, '{1}', '{2}', '{3}', '{4}', {5})", id++, myA[x++], myA[x++], myA[x++], myA[x++], myA[x++]);



                }  
            }
        }
        
        public static void ConvertFormat2ToSql(string path, int id)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                
                string[] myA = CleanFormat2(sr);

                string newpath = ConvertPathToSql(path);
                using (StreamWriter sw = new StreamWriter(File.Create(newpath)))
                {

                    int x = 0;
                    for (int i = 0; i < myA.Length / 5; i++)
                        sw.WriteLine("insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values({0}, '{1}', '{2}', '{3}', '{5}', {4})", id++, myA[x++].Trim('"'), myA[x++].Trim('"'), myA[x++].Trim('"'), myA[x++].Trim('"') == "Married" ? true: false , myA[x++].Trim('"'));

                }
            }
        }
        public static void ConvertFormat3ToSql(string path, int id)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                string[] myA = CleanFormat3(sr);


                string newpath = ConvertPathToSql(path);
                using (StreamWriter sw = new StreamWriter(File.Create(newpath)))
                {

                    int x = 0;
                   
                    for (int i = 0; i < myA.Length / 5; i++)
                    sw.WriteLine("insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values({0}, '{1}', '{2}', '{3}/{4}/{5}', '{7}', {6})", id++, myA[x++], myA[x++], myA[x++], myA[x++], myA[x++], myA[x++].Trim() == "yes" ? true : false, myA[x++]);


                }

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
                my = my.Replace('|', ' ');
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
