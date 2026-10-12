using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjetCegep.DAOs
{
    public class departementRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static departementRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static departementRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new departementRepository();
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
        private departementRepository() : base() { }

        #endregion

        #region MethodesService

        public int ObtenirIdDepartement(string nomCegep, string nomDepartement)
        {
            SqlCommand command = new SqlCommand(" SELECT Id" +
                                                " FROM Departements" +
                                                " WHERE IdCegep = @idCegep " +
                                                "    AND Nom = @nom ", connexion);

            SqlParameter cegepParam = new SqlParameter("@idCegep", SqlDbType.Int);
            SqlParameter nomDepartementParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);

            cegepParam.Value = cegepRepository.Instance.ObtenirIdCegep(nomCegep);
            nomDepartementParam.Value = nomDepartement;

            command.Parameters.Add(cegepParam);
            command.Parameters.Add(nomDepartementParam);

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

        public DepartementDTO ObtenirDepartement(string nomCegep, string nomDepartement)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                " FROM Departements " +
                                                " WHERE IdCegep = @idCegep " +
                                                "    AND Nom = @nom ", connexion);

            SqlParameter cegepParam = new SqlParameter("@idCegep", SqlDbType.Int);
            SqlParameter nomDepartementParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);

            cegepParam.Value = cegepRepository.Instance.ObtenirIdCegep(nomCegep);
            nomDepartementParam.Value = nomDepartement;

            command.Parameters.Add(cegepParam);
            command.Parameters.Add(nomDepartementParam);

            DepartementDTO unDepartement = new DepartementDTO();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                unDepartement = new DepartementDTO(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                reader.Close();
                return unDepartement;
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

        public List<DepartementDTO> ObtenirListeDepartement(string nomCegep)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                " FROM Departements " +
                                                " WHERE IdCegep = @idCegep; ", connexion);

            SqlParameter idparam = new SqlParameter("@idCegep", SqlDbType.Int);

            idparam.Value = cegepRepository.Instance.ObtenirIdCegep(nomCegep);

            command.Parameters.Add(idparam);

            List<DepartementDTO> liste = new List<DepartementDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    liste.Add(new DepartementDTO(reader.GetString(1), reader.GetString(2), reader.GetString(3)));

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

        public void AjouterDepartement(string nomCegep, DepartementDTO departementDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " INSERT INTO Departements (No, Nom, Description, IdCegep) " +
                                  " VALUES (@no, @nom, @description, @idCegep); ";

            SqlParameter noDepartementParam = new SqlParameter("@no", SqlDbType.VarChar, 50);
            SqlParameter nomDepartementParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);
            SqlParameter descriptionDepartementParam = new SqlParameter("@description", SqlDbType.VarChar, 50);
            SqlParameter cegepParam = new SqlParameter("@idCegep", SqlDbType.Int);

            noDepartementParam.Value = departementDTO.No;
            nomDepartementParam.Value = departementDTO.Nom;
            descriptionDepartementParam.Value = departementDTO.Description;
            cegepParam.Value = cegepRepository.Instance.ObtenirIdCegep(nomCegep);

            command.Parameters.Add(noDepartementParam);
            command.Parameters.Add(nomDepartementParam);
            command.Parameters.Add(descriptionDepartementParam);
            command.Parameters.Add(cegepParam);

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

        /// <summary>
        /// Méthode de service permettant de modifier un cegep.
        /// </summary>
        /// <param name="nomCegep">Le nom du Cegep</param>
        /// <param name="departementDTO">Le DTO du departement.</param>
        public void ModifierDepartement(string nomCegep, DepartementDTO departementDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " UPDATE Departements " +
                                  "   SET No = @no " +
                                  "       Description = @description " +
                                  "   WHERE Nom = @nom " +
                                  "      AND IdCegep = @idCegep ";

            SqlParameter nomDepartementParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);
            SqlParameter noDepartementParam = new SqlParameter("@no", SqlDbType.VarChar, 50);
            SqlParameter descriptionDepartementParam = new SqlParameter("@description", SqlDbType.VarChar, 50);
            SqlParameter cegepParam = new SqlParameter("@idCegep", SqlDbType.Int);
            
            nomDepartementParam.Value = departementDTO.Nom;
            noDepartementParam.Value = departementDTO.No;
            descriptionDepartementParam.Value = departementDTO.Description;
            cegepParam.Value = cegepRepository.Instance.ObtenirIdCegep(nomCegep);
            

            command.Parameters.Add(nomDepartementParam);
            command.Parameters.Add(noDepartementParam);
            command.Parameters.Add(descriptionDepartementParam);
            command.Parameters.Add(cegepParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur lors de la modification du departement...");
            }
            finally
            {
                FermerCloseConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant de supprimer un cegep.
        /// </summary>
        /// <param name="cegepDTO">Le DTO du cegep.</param>
        public void SupprimerDepartement(string nomCegep,DepartementDTO departementDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = "DELETE Departements WHERE Id = @id AND IdCegep = @idCegep";

            SqlParameter idDepartementParam = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter idCegepParam = new SqlParameter("@idCegep", SqlDbType.Int);

            idDepartementParam.Value = ObtenirIdDepartement(nomCegep,departementDTO.Nom);
            idCegepParam.Value = cegepRepository.Instance.ObtenirIdCegep(nomCegep);

            command.Parameters.Add(idDepartementParam);
            command.Parameters.Add(idCegepParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur lors de la supression d'un departement...");
            }
            finally
            {
                FermerCloseConnexion();
            }
        }

        #endregion MethodesService
    }
}
