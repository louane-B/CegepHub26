using System.Data.SqlClient;

namespace ProjetCegep.DAOs
{
    /// <summary>
    /// Classe représentant un repository.
    /// </summary>
    public class Repository
    {
        #region AttributsPropriete

        /// <summary>
        /// La connexion.
        /// </summary>
        protected SqlConnection connexion;

        #endregion AttributsPropriete

        #region Constructeurs

        /// <summary>
        /// Constructeur de la classe.
        /// </summary>
        protected Repository()
        {
            connexion = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Cegep;Data Source=ZENBOOK-LOUANEB");
        }

        #endregion Constructeurs

        #region MethodesService

        /// <summary>
        /// Méthode permettant d'ouvrir la connexion.
        /// </summary>
        protected void OuvrirConnexion()
        {
            connexion.Open();
        }

        /// <summary>
        /// Méthode permettant de fermer la connexion.
        /// </summary>
        protected void FermerCloseConnexion()
        {
            connexion.Close();
        }

        #endregion MethodesService
    }
}
