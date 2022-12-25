namespace Porunga.Resources;

/// <summary>
/// Class Type <seealso cref="DefaultDatabase"/>.
/// </summary>
internal class DefaultDatabase
{
    /// <summary>
    /// Database
    /// </summary>
    internal string Database { get { return "DatabaseName"; } }

    /// <summary>
    /// Driver
    /// </summary>
    internal string Driver { get { return "System.Data.SqlClient or MySql.Data.MySqlClient"; } }

    /// <summary>
    /// Pass
    /// </summary>
    internal string Pass { get { return "Pass"; } }

    /// <summary>
    /// Sectional
    /// </summary>
    internal string Sectional { get { return "Code Sectional"; } }

    /// <summary>
    /// Server
    /// </summary>
    internal string Server { get { return @"Server"; } }

    /// <summary>
    /// User
    /// </summary>
    internal string User { get { return "UserName"; } }

    /// <summary>
    /// Zone
    /// </summary>
    internal string Zone { get { return "Code Zone"; } }
}