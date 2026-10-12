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
                                                "    AND NoEmploye = @noEmploye " +
                                                "    AND IdCegep = @idCegep ", connexion);

            SqlParameter idDepartementParam = new SqlParameter("@idDepartement", SqlDbType.Int);
            SqlParameter noEmployeParam = new SqlParameter("@noEmploye", SqlDbType.Int);
            SqlParameter cegepParam = new SqlParameter("@idCegep", SqlDbType.Int);

            idDepartementParam.Value = departementRepository.Instance.ObtenirIdDepartement(nomCegep, nomDepartement);
            noEmployeParam.Value = noEmploye;
            cegepParam.Value = cegepRepository.Instance.ObtenirIdCegep(nomCegep);
            

            command.Parameters.Add(idDepartementParam);
            command.Parameters.Add(noEmployeParam);
            command.Parameters.Add(cegepParam);

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


        #endregion MethodesService
    }
}
