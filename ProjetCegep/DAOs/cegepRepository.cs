using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetCegep.DAOs
{
    /// <summary>
    /// Classe représentant le répository d'un film.
    /// </summary>
    public class cegepRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static cegepRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static cegepRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new cegepRepository();
                }
                //...on retourne l'instance unique.
                return instance;
            }
        }

        #endregion AttributsProprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur privée du repository.
        /// </summary>
        private cegepRepository() : base() { }

        #endregion

        /// <summary>
        /// Méthode de service permettant d'obtenir le ID d'un Cegep selon ses informatiques uniques.
        /// </summary>
        /// <param name="Nom">Le nom du Cegep.</param>
        /// <param name="CodePostal">Le code Postal du Cegep.</param>
        /// <returns>Le ID du Cegep.</returns>
        public int ObtenirIdCegep(string nom)
        {
            SqlCommand command = new SqlCommand(" SELECT Id " +
                                                " FROM Cegeps " +
                                                " WHERE Nom = @nom ", connexion);

            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);

            nomParam.Value = nom;

            command.Parameters.Add(nomParam);

            int id;

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                reader.Close();
                return id;
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur lors de l'obtention d'un id de cegep par son nom...");
            }
            finally
            {
                FermerCloseConnexion();
            }
            return -1;
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir un cegep selon ses informations uniques.
        /// </summary>
        /// <param name="nom">Nom du Cegep.</param>
        /// <returns>Le DTO du Cegep.</returns>
        public CegepDTO ObtenirCegep(string nom)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                " FROM Cegeps " +
                                                " WHERE Nom = @nom ", connexion);

            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);

            nomParam.Value = nom;

            command.Parameters.Add(nomParam);

            CegepDTO unCegep = new CegepDTO();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                unCegep = new CegepDTO(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
                reader.Close();
                return unCegep;
            }
            catch(Exception)
            {
                Console.WriteLine("Erreur lors de l'obtention d'un cegep par son nom...");
                return null;
            }
            finally
            {
                FermerCloseConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'ajouter un cegep.
        /// </summary>
        /// <param name="cegepDTO">Le DTO du cegep.</param>
        public void AjouterCegep(CegepDTO cegepDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " INSERT INTO Cegeps (Nom, Adresse, Ville, Province, CodePostal, Telephone, Courriel) " +
                                  " VALUES (@nom, @adresse, @ville, @province, @codePostal, @telephone, @courriel) ";

            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);
            SqlParameter adresseParam = new SqlParameter("@adresse", SqlDbType.VarChar, 50);
            SqlParameter villeParam = new SqlParameter("@ville", SqlDbType.VarChar, 50);
            SqlParameter provinceParam = new SqlParameter("@province", SqlDbType.VarChar, 50);
            SqlParameter codePostalParam = new SqlParameter("@codePostal", SqlDbType.VarChar, 50);
            SqlParameter telephoneParam = new SqlParameter("@telephone", SqlDbType.VarChar, 50);
            SqlParameter courrielParam = new SqlParameter("@courriel", SqlDbType.VarChar, 50);

            nomParam.Value = cegepDTO.Nom;
            adresseParam.Value = cegepDTO.Adresse;
            villeParam.Value = cegepDTO.Ville;
            provinceParam.Value = cegepDTO.Province;
            codePostalParam.Value = cegepDTO.CodePostal;
            telephoneParam.Value = cegepDTO.Telephone;
            courrielParam.Value = cegepDTO.Courriel;

            command.Parameters.Add(nomParam);
            command.Parameters.Add(adresseParam);
            command.Parameters.Add(villeParam);
            command.Parameters.Add(provinceParam);
            command.Parameters.Add(codePostalParam);
            command.Parameters.Add(telephoneParam);
            command.Parameters.Add(courrielParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch(Exception)
            {
                Console.WriteLine("Erreur lors de l'ajout d'un cegep...");
            }
            finally
            {
                FermerCloseConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant de modifier un cegep.
        /// </summary>
        /// <param name="cegepDTO">Le DTO du cegep.</param>
        public void ModifierCegep(CegepDTO cegepDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " UPDATE Cegeps " +
                                  "   SET Adresse = @adresse " +
                                  "       Ville = @ville " +
                                  "       Province = @Province " +
                                  "       CodePostal = @codePostal " +
                                  "       Telephone = @telephone " +
                                  "       Courriel = @courriel " +
                                  "   WHERE Nom = @nom ";

            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);
            SqlParameter adresseParam = new SqlParameter("@adresse", SqlDbType.VarChar, 50);
            SqlParameter villeParam = new SqlParameter("@ville", SqlDbType.VarChar, 50);
            SqlParameter provinceParam = new SqlParameter("@province", SqlDbType.VarChar, 50);
            SqlParameter codePostalParam = new SqlParameter("@codePostal", SqlDbType.VarChar, 50);
            SqlParameter telephoneParam = new SqlParameter("@telephone", SqlDbType.VarChar, 50);
            SqlParameter courrielParam = new SqlParameter("@courriel", SqlDbType.VarChar, 50);

            nomParam.Value = cegepDTO.Nom;
            adresseParam.Value = cegepDTO.Adresse;
            villeParam.Value = cegepDTO.Ville;
            provinceParam.Value = cegepDTO.Province;
            codePostalParam.Value = cegepDTO.CodePostal;
            telephoneParam.Value = cegepDTO.Telephone;
            courrielParam.Value = cegepDTO.Courriel;

            command.Parameters.Add(nomParam);
            command.Parameters.Add(adresseParam);
            command.Parameters.Add(villeParam);
            command.Parameters.Add(provinceParam);
            command.Parameters.Add(codePostalParam);
            command.Parameters.Add(telephoneParam);
            command.Parameters.Add(courrielParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur lors de la modification du cegep...");
            }
            finally
            {
                FermerCloseConnexion();
            }
        }
    }
}
