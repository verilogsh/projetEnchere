using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Utility {
    public class IdGenerator {

        public static string getCategId()
        {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 2; i++)
            {
                list.Add(random.Next(10));
            }
            return "c" + String.Join("", list);
        }

        public static string getUserId() {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 8; i++) {
                list.Add(random.Next(10));
            }
            return "mb" + String.Join("", list);
        }

        public static string getObjetId() {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 13; i++) {
                list.Add(random.Next(10));
            }
            return "ob" + String.Join("",list);
        }

        public static string getEvaluationId() {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 8; i++) {
                list.Add(random.Next(10));
            }
            return "ev" + String.Join("", list);
        }

        public static string getEncherenId() {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 18; i++) {
                list.Add(random.Next(10));
            }
            return "en" + String.Join("", list);
        }
    }
}