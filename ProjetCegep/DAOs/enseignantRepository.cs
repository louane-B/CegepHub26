using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCegep.DAOs
{
    public class enseignantRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static enseignantRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static enseignantRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new enseignantRepository();
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
        private enseignantRepository() : base() { }

        #endregion

        #region MethodesService

        public int ObtenirIdEnseignant(string nomCegep, string nomDepartement, int noEmploye )
        {
            SqlCommand command = new SqlCommand(" SELECT Id" +
                                                " FROM Enseignants" +
                                                " WHERE IdDepartement = @idDepartement " +
                                                "    AND NoEmploye = @noEmploye ", connexion);

            SqlParameter idDepartementParam = new SqlParameter("@idDepartement", SqlDbType.Int);
            SqlParameter noEmployeParam = new SqlParameter("@noEmploye", SqlDbType.Int);

            idDepartementParam.Value = departementRepository.Instance.ObtenirIdDepartement(nomCegep, nomDepartement);
            noEmployeParam.Value = noEmploye;
            

            command.Parameters.Add(idDepartementParam);
            command.Parameters.Add(noEmployeParam);

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
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            finally
            {
                FermerCloseConnexion();
            }
            return -1;
        }

        public EnseignantDTO ObtenirEnseignant(string nomCegep, string nomDepartement, int noEmploye)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                " FROM Enseignants " +
                                                " WHERE IdDepartement = @idDepartement " +
                                                "    AND NoEmploye = @noEmploye ", connexion);

            SqlParameter idDepartementParam = new SqlParameter("@idDepartement", SqlDbType.Int);
            SqlParameter noEmployeParam = new SqlParameter("@noEmploye", SqlDbType.Int);

            idDepartementParam.Value = departementRepository.Instance.ObtenirIdDepartement(nomCegep, nomDepartement);
            noEmployeParam.Value = noEmploye;

            command.Parameters.Add(idDepartementParam);
            command.Parameters.Add(noEmployeParam);

            EnseignantDTO unEnseignant = new EnseignantDTO();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                unEnseignant = new EnseignantDTO(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11));
                reader.Close();
                return unEnseignant;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            finally
            {
                FermerCloseConnexion();
            }
            return null;
        }

        public List<EnseignantDTO> ObtenirListeEnseignant(string nomCegep, string nomDepartement)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                " FROM Enseignants " +
                                                " WHERE IdDepartement = @idDepartement ", connexion);

            SqlParameter idDepartementparam = new SqlParameter("@idDepartement", SqlDbType.Int);

            idDepartementparam.Value = departementRepository.Instance.ObtenirIdDepartement(nomCegep, nomDepartement);

            command.Parameters.Add(idDepartementparam);

            List<EnseignantDTO> liste = new List<EnseignantDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    liste.Add(new EnseignantDTO(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11)));

                }
                reader.Close();
                return liste;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            finally
            {
                FermerCloseConnexion();
            }
            return liste;
        }

        public void AjouterEnseignant(string nomCegep, string nomDepartement, EnseignantDTO enseignantDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " INSERT INTO Enseiqnants (NoEmploye, Nom, Prenom, Adresse, Ville, Province, CodePostal, Telephone, Courriel, IdDepartement) " +
                                  " VALUES (@noEmploye, @nom, @prenom, @adresse, @ville, @province, @codePostal, @telephone, @courriel, @idDepartement) ";

            SqlParameter noEmployeEnseignantParam = new SqlParameter("@noEmploye", SqlDbType.Int);
            SqlParameter nomEnseignantParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);
            SqlParameter prenomEnseignantParam = new SqlParameter("@prenom", SqlDbType.VarChar, 50);
            SqlParameter adresseEnseignantParam = new SqlParameter("@adresse", SqlDbType.VarChar, 50);
            SqlParameter villeEnseignantParam = new SqlParameter("@ville", SqlDbType.VarChar, 50);
            SqlParameter provinceEnseignantParam = new SqlParameter("@province", SqlDbType.VarChar, 50);
            SqlParameter codePostalEnseignantParam = new SqlParameter("@codePostal", SqlDbType.VarChar, 50);
            SqlParameter telephoneEnseignantParam = new SqlParameter("@telephone", SqlDbType.VarChar, 50);
            SqlParameter courrielEnseignantParam = new SqlParameter("@courriel", SqlDbType.VarChar, 50);
            SqlParameter DepartementParam = new SqlParameter("@idDepartement", SqlDbType.Int);

            noEmployeEnseignantParam.Value = enseignantDTO.NoEmploye;
            nomEnseignantParam.Value = enseignantDTO.Nom;
            prenomEnseignantParam.Value = enseignantDTO.Prenom;
            adresseEnseignantParam.Value = enseignantDTO.Adresse;
            villeEnseignantParam.Value = enseignantDTO.Ville;
            provinceEnseignantParam.Value = enseignantDTO.Province;
            codePostalEnseignantParam.Value = enseignantDTO.CodePostal;
            telephoneEnseignantParam.Value = enseignantDTO.Telephone;
            courrielEnseignantParam.Value = enseignantDTO.Courriel;
            DepartementParam.Value = departementRepository.Instance.ObtenirIdDepartement(nomCegep, nomDepartement);

            command.Parameters.Add(noEmployeEnseignantParam);
            command.Parameters.Add(nomEnseignantParam);
            command.Parameters.Add(prenomEnseignantParam);
            command.Parameters.Add(adresseEnseignantParam);
            command.Parameters.Add(villeEnseignantParam);
            command.Parameters.Add(provinceEnseignantParam);
            command.Parameters.Add(codePostalEnseignantParam);
            command.Parameters.Add(telephoneEnseignantParam);
            command.Parameters.Add(courrielEnseignantParam);
            command.Parameters.Add(DepartementParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            finally
            {
                FermerCloseConnexion();
            }
        }

        #endregion MethodesService
    }
}
