using Porunga.Entities;

namespace Porunga.Base;

public class CustomFile
{
    #region Keys File Config
    /// <summary>
    /// Key Path File.
    /// </summary>
    private string PathFile;

    /// <summary>
    /// Credentials.
    /// </summary>
    private NetworkCredential Credentials;
    #endregion

    #region Constructor
    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    public CustomFile()
    {
        this.Initialize(null, null);
    }

    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    /// <param name="pathFile">path File with name and extensions.</param>
    public CustomFile(string pathFile)
    {
        this.Initialize(pathFile, null);
    }

    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    /// <param name="credentials">Credentials to create.</param>
    public CustomFile(NetworkCredential credentials)
    {
        this.Initialize(null, credentials);
    }

    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    /// <param name="pathFile">path File with name and extensions.</param>
    /// <param name="credentials">Credentials to create.</param>
    public CustomFile(string pathFile, NetworkCredential credentials)
    {
        this.Initialize(pathFile, credentials);
    }
    #endregion

    #region Private
    /// <summary>
    /// Initialize Components.
    /// </summary>
    /// <param name="pathFile">path File with name and extensions.</param>
    /// <param name="credentials">Credentials to create.</param>
    private void Initialize(string pathFile, NetworkCredential credentials)
    {
        this.PathFile = pathFile ?? @"C:\Setting\CustomFile.config";
        this.Fill(credentials);
        this.Load();
    }

    /// <summary>
    /// Fill Custom.
    /// </summary>
    /// <param name="credentials">Credentials to create.</param>
    private void Fill(NetworkCredential credentials)
    {
        if (credentials == null)
        {
            this.Credentials = new NetworkCredential
            {
                Database = "DatabaseName",
                Driver = "System.Data.SqlClient or MySql.Data.MySqlClient",
                Pass = "Pass",
                Sectional = "Code Sectional",
                Server = "Server",
                User = "UserName",
                Zone = "Code Zone"
            };
        }
        else
        {
            this.Credentials = credentials;
        }
    }
    #endregion




    private void Load()
    {
        try
        {
            if (!File.Exists(this.PathFile))
            {
                File.WriteAllText(this.PathFile, this.Credentials.GetKeyString());
                return;
            }

            this.Credentials = this.PathFile.ReadXmlFileString().ToSerialize<NetworkCredential>();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
