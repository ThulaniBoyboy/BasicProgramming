using System;
using Xunit;
using System.IO;
using SqlBuilder;

namespace BasicProgrammingTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestConvertFormat1ToSql()
        {
            string expected = "insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values(400, 'Manmay', 'Mohanty', 'M', '07/03/1983', True)\r\ninsert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values(401, 'Marike', 'Fourie', 'F', '30/11/2001', False)\r\n";

            Program.ConvertFormat1ToSql("c:/users/user/desktop/test.txt", 400);
            using (StreamReader sr = new StreamReader(File.OpenRead("c:/users/user/desktop/test.sql")))
            {
                string actual = sr.ReadToEnd();


                Assert.Equal(expected, actual);
            }


        }

        [Fact]
        public void TestConvertFormat2ToSql()
        {
            string expected = "insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values(400, 'Manmay', 'Mohanty', 'M', '07/03/1983', True)\r\ninsert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values(401, 'Marike', 'Fourie', 'F', '30/11/2001', False)\r\n";

            Program.ConvertFormat2ToSql("c:/users/user/desktop/test1.txt", 400);
            using (StreamReader sr = new StreamReader(File.OpenRead("c:/users/user/desktop/test1.sql")))
            {
                string actual = sr.ReadToEnd();


                Assert.Equal(expected, actual);
            }


        }
        [Fact]
        public void TestConvertFormat3ToSql()
        {
            string expected = "insert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values(400, 'Manmay', 'Mohanty', 'M', '07/03/1983', True)\r\ninsert into customers(id, first_name, last_name, gender, date_of_birth, marital_status) values(401, 'Marike', 'Fourie', 'F', '30/11/2001', False)\r\n";

            Program.ConvertFormat3ToSql("c:/users/user/desktop/test2.txt", 400);
            using (StreamReader sr = new StreamReader(File.OpenRead("c:/users/user/desktop/test2.sql")))
            {
                string actual = sr.ReadToEnd();


                Assert.Equal(expected, actual);
            }


        }
    }
}
