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

    public CustomFile(string pathFile, string keySecurityPass, string keySaltBytes, string credentials)
    {
        this.Initialize(pathFile, keySecurityPass, keySaltBytes, credentials);
    }

    public CustomFile(string pathFile, string keySecurityPass, string keySaltBytes)
    {
        this.Initialize(pathFile, keySecurityPass, keySaltBytes);
    }
    #endregion

    #region Private
    /// <summary>
    /// Initialize Components.
    /// </summary>
    /// <param name="pathFile">path File with name and extensions.</param>
    /// <param name="credentials">Credentials to create.</param>
    /// <param name="keySecurityPass">Credentials to create.</param>
    /// <param name="keySaltBytes">Credentials to create.</param>
    private void Initialize(string pathFile, string keySecurityPass, string keySaltBytes, string credentials = null)
    {
        if (string.IsNullOrEmpty(keySecurityPass) || string.IsNullOrEmpty(keySaltBytes))
        {
            throw new ArgumentNullException("Crypto parameters is necesary");
        }

        this.Crypto = new AdvancesEncryptionStandard(keySecurityPass, keySaltBytes);
        this.PathFile = this.Crypto.Decode(pathFile);
        if (!string.IsNullOrEmpty(credentials))
        {
            this.Credentials = this.Crypto.Decode(credentials).ToSerialize<NetworkCredential>();
        }

        this.Load();
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
                if (this.Credentials != null)
                {
                    File.WriteAllText(this.PathFile, this.Crypto.Encode(this.Credentials.GetKeyString()));
                    return;
                }

                throw new ArgumentNullException("Crypto parameters is necesary");
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
