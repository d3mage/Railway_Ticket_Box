using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DAL.Provider
{
    public class XmlProvider<T> : IProvider<T>
    {
        public void Write(List<T> data, string connection)
        {
            using FileStream fs = new FileStream(connection, FileMode.OpenOrCreate);
            XmlSerializer formatter = new XmlSerializer(data.GetType());
            try
            {
                formatter.Serialize(fs, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> Read(string connection)
        {
            List<T> data;
            using (FileStream fs = new FileStream(connection, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
                try
                {
                    data = (List<T>)formatter.Deserialize(fs);
                }
                catch (InvalidOperationException ex)
                {
                    throw ex;
                }
            }
            return data;
        }
    }
}
