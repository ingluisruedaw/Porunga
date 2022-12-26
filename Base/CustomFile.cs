using Porunga.Entities;
using Atenea.Crypto.Symmetric;
using System.Text;

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

    AdvancesEncryptionStandard Crypto;

    #endregion

    #region Constructor
    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    public CustomFile()
    {
        this.Initialize(null, null, null, null);
    }

    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    /// <param name="pathFile">path File with name and extensions.</param>
    public CustomFile(string pathFile)
    {
        this.Initialize(pathFile, null, null, null);
    }

    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    /// <param name="credentials">Credentials to create.</param>
    public CustomFile(NetworkCredential credentials)
    {
        this.Initialize(null, credentials, null, null);
    }

    /// <summary>
    /// Construtor of the class <seealso cref="CustomFile"/>.
    /// </summary>
    /// <param name="pathFile">path File with name and extensions.</param>
    /// <param name="credentials">Credentials to create.</param>
    public CustomFile(string pathFile, NetworkCredential credentials)
    {
        this.Initialize(pathFile, credentials, null, null);
    }

    public CustomFile(string pathFile, NetworkCredential credentials, string keySecurityPass, string keySaltBytes)
    {
        this.Initialize(pathFile, credentials, keySecurityPass, keySaltBytes);
    }
    #endregion

    #region Private
    /// <summary>
    /// Initialize Components.
    /// </summary>
    /// <param name="pathFile">path File with name and extensions.</param>
    /// <param name="credentials">Credentials to create.</param>
    private void Initialize(string pathFile, NetworkCredential credentials, string keySecurityPass, string keySaltBytes)
    {
        this.Crypto = (string.IsNullOrEmpty(keySecurityPass) || string.IsNullOrEmpty(keySaltBytes))
            ? new AdvancesEncryptionStandard()
            : new AdvancesEncryptionStandard(keySecurityPass, keySaltBytes);        
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

    /// <summary>
    /// Load.
    /// </summary>
    private void Load()
    {
        try
        {
            if (!File.Exists(this.PathFile))
            {
                File.WriteAllText(this.PathFile, this.Crypto.Encode(this.Credentials.GetKeyString()));
                return;
            }

            this.Credentials = this.Crypto.Decode(File.ReadAllText(this.PathFile)).ReadXmlString().ToSerialize<NetworkCredential>();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion





}
