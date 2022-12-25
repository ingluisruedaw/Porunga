using Porunga.Configuration;
using Porunga.Entities;
using Porunga.Resources;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Porunga.Base;

public class CustomFile
{
    #region Keys File Config
    /// <summary>
    /// Key Path File.
    /// </summary>
    private string PathFile;

    /// <summary>
    /// CustomConfiguration
    /// </summary>
    private CustomConfiguration Custom;

    private DefaultDatabase Default;
    #endregion

    /// <summary>
    /// Constructor of class <seealso cref="CustomFile"/>.
    /// </summary>
    public CustomFile()
    {
        this.Initialize();
    }

    /// <summary>
    /// Constructor of class <seealso cref="CustomFile"/>.
    /// </summary>
    public CustomFile(string pathFile)
    {
        this.Initialize(pathFile);
    }

    private void Initialize(string pathFile = null)
    {
        this.Default = new DefaultDatabase();
        this.PathFile = pathFile ?? @"C:\Setting\CustomFile.config";
        this.FillCustom();
        this.LoadDatabaseFile();
    }

    //public CustomConfiguration GetCustomConfiguration()
    //{
    //    return this.Custom;
    //}

    private void FillCustom()
    {
        this.Custom = new CustomConfiguration
        {
            Default = new DefaultFile
            {
                Database = this.Default.Database,
                Driver = this.Default.Driver,
                Pass = this.Default.Pass,
                Sectional = this.Default.Sectional,
                Server = this.Default.Server,
                User = this.Default.User,
                Zone = this.Default.Zone
            }
        };
    }

    private void LoadDatabaseFile()
    {
        try
        {
            if (!File.Exists(this.PathFile))
            {
                this.CreateDatabaseFile(this.PathFile, this.GetKeyString(this.Custom, typeof(CustomConfiguration)));
                return;
            }

            var cus = this.ReadXmlFileString(this.PathFile);
            this.Custom = this.ToSerialize<CustomConfiguration>(cus);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CreateDatabaseFile(string path, string contents)
    {
        this.WriteFile(path, contents);
    }

    private void WriteFile(string path, string contents)
    {
        File.WriteAllText(path, contents);
    }

    private string GetKeyString(object? key, Type type)
    {
        try
        {
            string pubKeyString;
            {
                var sw = new StringWriter();
                var xs = new XmlSerializer(type);
                xs.Serialize(sw, key);
                pubKeyString = sw.ToString();
            }

            XDocument doc = XDocument.Parse(pubKeyString);
            return doc.ToString();
        }
        catch (XmlException ex)
        {
            throw ex;
        }
    }

    private string ReadXmlFileString(string path)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText(path));
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);
            return sw.ToString();
        }
        catch (XmlException ex)
        {
            throw ex;
        }
    }

    private T ToSerialize<T>(string content)
    {
        var sr = new StringReader(content);
        var xs = new XmlSerializer(typeof(T));
        return (T)xs.Deserialize(sr);
    }
}
