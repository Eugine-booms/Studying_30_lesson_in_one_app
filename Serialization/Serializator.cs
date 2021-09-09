using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace LessonInOne.Serialization
{
    public static class Serializator
    {
        /// <summary>
        /// Json Не работает с readOnly полями. Обязательно помечать: 
        /// классы атрибутом [DataContract] и нужные члены класса [DataMember]
        /// </summary>
        /// <typeparam name="T"> Тип сохраняемых данных </typeparam>
        /// <param name="obj"> Сохраняемые данные </param>
        /// <param name="path"> Путь к файлу </param>
        internal static void JsonSerializator<T>(T obj, string path)
        {
            var jsonFormater = new DataContractJsonSerializer(typeof(T));
            using (var fileStream = new FileStream(path, FileMode.Create))
            {

                jsonFormater.WriteObject(fileStream, obj);
            }
        }
        internal static T JsonDeserializator<T>(string path)
        {
            var jsonFormater = new DataContractJsonSerializer(typeof(T));
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0)
                {
                    return (T)jsonFormater.ReadObject(fileStream);
                }
                return default;
            }

        }
        internal static void BinarySerializator(object obj, string path)
        {
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                var binFormatter = new BinaryFormatter();
                binFormatter.Serialize(stream, obj);
            }
        }
        internal static T BinaryDeserializator<T>(string path)
        {
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                var binFormatter = new BinaryFormatter();
                if (stream.Length > 0)
                {
                    return (T)binFormatter.Deserialize(stream);
                }
                return default;
            }
        }
        /// <summary>
        /// Не работает с Private и readOnly полями и полями без setter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"> Данные для серилизации </param>
        /// <param name="path"> Путь к файлу</param>
        internal static void XMLSerializator<T>(T obj, string path)
        {
            var xmlFormatter = new XmlSerializer(typeof(T));
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                xmlFormatter.Serialize(filestream, obj);
            }
        }
        /// <summary>
        /// Не работает с Private и readOnly полями и полями без setter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"> Данные для серилизации </param>
        /// <param name="path"> Путь к файлу</param>
        internal static T XMLDeserializator<T>(string path)
        {
            var xmlFormatter = new XmlSerializer(typeof(T));
            using (var filestream = new FileStream(path, FileMode.Open))
            {
                if (filestream.Length > 0)
                {
                    return (T)xmlFormatter.Deserialize(filestream);
                }
                return default;
            }
        }
        /// <summary>
        /// Не умеет работать с List, необходимо перевести List в Array
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        internal static void SoapSerializator(object obj, string path)
        {

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                var soapFormater = new SoapFormatter();
                soapFormater.Serialize(fileStream, obj);
            }
        }
        internal static T SoapDeserializator<T>(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var soapFormater = new SoapFormatter();
                if (fileStream.Length > 0)
                {
                    return (T)soapFormater.Deserialize(fileStream);
                }
                return default;
            }
        }

    }
}

