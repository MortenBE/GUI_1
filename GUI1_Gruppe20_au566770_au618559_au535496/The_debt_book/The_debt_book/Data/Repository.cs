using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using TheDebtBook.Models;

namespace The_debt_book
{
    public class Repository
    {
        internal static void ReadFile(string fileName, out ObservableCollection<Depts> depts)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Depts>));
            TextReader reader = new StreamReader(fileName);
            depts = (ObservableCollection<Depts>)serializer.Deserialize(reader);
            reader.Close();
        }

        internal static void SaveFile(string fileName, ObservableCollection<Depts> depts)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Depts>));
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, depts);
            writer.Close();
        }
    }
}
