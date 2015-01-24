using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Kalendarz_Kraków_Arena
{
    public partial class DBconnect
    {

        public DBconnect()
        {

        }
        private string file = "any.txt";
        public void encode(string s)
        {

            string wynik = "";
            string[] tab = new string[300];
            int j = 0;
            foreach (char i in s)
            {
                if ((int)i % 2 == 0)
                {
                    wynik += (int)i + 9;
                    tab[j] = wynik + "f";
                    wynik = "";
                    j++;
                }
                else
                {
                    wynik += (int)i + 6;
                    tab[j] = wynik + "b";
                    wynik = "";
                    j++;
                }

            }

            System.IO.File.WriteAllLines(@file, tab);
        }
        public string decode()
        {
            string[] s = System.IO.File.ReadAllLines(@file);
            string wynik = "";
            int integer = 0;
            foreach (string str in s)
            {
                if (str != "")
                {
                    if (str[str.Length - 1] == 'f')
                    {
                        integer = Convert.ToInt16(str.TrimEnd('f'));
                        wynik += (char)(integer - 9);
                    }
                    else
                    {
                        integer = Convert.ToInt16(str.TrimEnd('b'));
                        wynik += (char)(integer - 6);
                    }
                }
            }
            return wynik;
        }
        public bool isConnected()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(this.decode());
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

    }
}
