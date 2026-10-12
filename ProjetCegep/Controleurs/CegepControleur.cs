using System.IO;
using System.Xml.Serialization;
using ProjetCegep.Modeles;
using ProjetCegep.DTOs;
using System.Collections.Generic;
using ProjetCegep.DAOs;
using System;

/// <summary>
/// Namespace pour les classes de type Controleur.
/// </summary>
namespace ProjetCegep.Controleurs
{
    /// <summary>
    /// Classe représentant le controleur de l'application.
    /// </summary>
    public class CegepControleur
    {
        #region AttributsProprietes

        /// <summary>
        /// 
        /// </summary>
        private static CegepControleur instance;


        /// <summary>
        /// 
        /// </summary>
        public static CegepControleur Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CegepControleur();
                }
                return instance;
            }
        }

        #endregion AttributsProprietes

        #region Contructeurs

        /// <summary>
        /// 
        /// </summary>
        private CegepControleur()
        {

        }

        #endregion Contructeurs

        #region MethodesCegep

        /// <summary>
        /// Méthode de service permettant d'ajouter un cegep
        /// </summary>
        /// <param name="cegep">Le DTO du Cégep</param>
        /// <returns></returns>
        public bool CreerCegep(CegepDTO cegep)
         {
            if(ObtenirCegep(cegep.Nom) == null)
            {
                cegepRepository.Instance.AjouterCegep(cegep);
                return true;
            }
            return false;
         }

        /// <summary>
        /// Métode de service permettant de modifier le Cégep.
        /// </summary>
        /// <param name="cegep">Le DTO du Cégep</param>
        /// <returns></returns>
        public void ModifierCegep(CegepDTO cegepDTO)
        {
            CegepDTO cegepDTOBD = ObtenirCegep(cegepDTO.Nom);
            Cegep cegepBD = new Cegep(cegepDTOBD.Nom, cegepDTOBD.Adresse, cegepDTOBD.Ville, cegepDTOBD.Province, cegepDTOBD.CodePostal, cegepDTOBD.Telephone, cegepDTOBD.Courriel);

            if (cegepDTO.Nom.Equals("Cégep Rivière-du-Loup"))
                if (cegepDTO.Adresse != cegepBD.Adresse ||
                    cegepDTO.Ville != cegepBD.Ville ||
                    cegepDTO.Province != cegepBD.Province ||
                    cegepDTO.CodePostal != cegepBD.CodePostal ||
                    cegepDTO.Telephone != cegepBD.Telephone ||
                    cegepDTO.CodePostal != cegepBD.CodePostal)
                    cegepRepository.Instance.ModifierCegep(cegepDTO);
                else
                    throw new Exception("Erreur - Veuiller modifier au moins une valeur.");
            else
                throw new Exception("Erreur - Vous devez utiliser le Cégep Rivière-du-Loup...");
        }

        /// <summary>
        /// Méthode de service permettant de supprimer un cegep
        /// </summary>
        /// <param name="cegepDTO">Le DTO du Cegep</param>
        /// <returns></returns>
        public bool SupprimerCegep(CegepDTO cegepDTO)
        {
            if (ObtenirCegep(cegepDTO.Nom) != null)
            {
                cegepRepository.Instance.SupprimerCegep(cegepDTO);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir un cegep
        /// </summary>
        /// <param name="nom">Le nom du Cegep</param>
        /// <returns></returns>
        public CegepDTO ObtenirCegep(string nom)
        {
            CegepDTO cegepDTO = cegepRepository.Instance.ObtenirCegep(nom);
            return cegepDTO;
        }

        #endregion MethodesCegep

        #region MethodesDepartement

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public List<DepartementDTO> ObtenirListeDepartement()
        //{
            List<DepartementDTO> liste = new List<DepartementDTO>();
        //   foreach (Departement departement in monCegep.ObtenirListeDepartement())
        //   {
        //       liste.Add(new DepartementDTO(departement));
        //   }
        //   return liste;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        //public DepartementDTO ObtenirDepartement(DepartementDTO departement)
        //{
        //   return new DepartementDTO(monCegep.ObtenirDepartement(new Departement(departement.No, departement.Nom, departement.Description)));
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        public void AjouterDepartement(DepartementDTO departement, string nom)
        { 
            CegepDTO cegepDTO = ObtenirCegep("Cégep Rivière-du-Loup");
            Cegep cegep = new Cegep(cegepDTO.Nom, cegepDTO.Adresse, cegepDTO.Ville, cegepDTO.Province, cegepDTO.CodePostal, cegepDTO.Telephone, cegepDTO.Courriel);
            List<DepartementDTO> listeDepartement = departementRepository.Instance.ObtenirListeDepartement("Cégep Rivière-du-Loup");

            foreach (DepartementDTO departementDTO in listeDepartement)
            {
                cegep.AjouterDepartement(new Departement(departementDTO.No, departementDTO.Nom, departementDTO.Description));
            }

            cegep.AjouterDepartement(new Departement(departement.No, departement.Nom, departement.Description));

            departementRepository.Instance.AjouterDepartement("Cégep Rivière-du-Loup", departement);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
       // public bool SupprimerDepartement(DepartementDTO departement)
       // {
        //    return monCegep.EnleverDepartement(new Departement(unNom: departement.Nom));
       // }

        #endregion MethodesDepartement

        #region MethodesEnseignant

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        public List<EnseignantDTO> ObtenirListeEnseignant(DepartementDTO departement)
        {
            List<EnseignantDTO> liste = new List<EnseignantDTO>();
       //     Departement leDepartement = monCegep.ObtenirDepartement(new Departement(unNom:departement.Nom));
       //     foreach (Enseignant enseignant in leDepartement.ObtenirListeEnseignant())
            {
       //         liste.Add(new EnseignantDTO(enseignant));
            }
            return liste;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
       // public EnseignantDTO ObtenirEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
       // {
       //     return new EnseignantDTO(monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).ObtenirEnseignant(new Enseignant(unNoEmploye:enseignant.NoEmploye)));
       // }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
      //  public bool AjouterEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
      //  {
      //      return monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).AjouterEnseignant(new Enseignant(enseignant.NoEmploye, enseignant.Nom, enseignant.Prenom, enseignant.Adresse, enseignant.Ville, enseignant.Province, enseignant.CodePostal, enseignant.Telephone, enseignant.Courriel));
      //  }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
       // public bool ModifierEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
       // {
         //   Enseignant lenseignant = monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).ObtenirEnseignant(new Enseignant(unNoEmploye:enseignant.NoEmploye));

       //     lenseignant.Nom = enseignant.Nom;
       //     lenseignant.Prenom = enseignant.Prenom;
       //     lenseignant.Adresse = enseignant.Adresse;
      //      lenseignant.Ville = enseignant.Ville;
      //      lenseignant.Province = enseignant.Province;
      //      lenseignant.CodePostal = enseignant.CodePostal;
      //      lenseignant.Telephone = enseignant.Telephone;
      //      lenseignant.Courriel = enseignant.Courriel;
      //      return true;
      //  }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        //public bool SupprimerEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
        //{
        //    return monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).EnleverEnseignant(new Enseignant(enseignant.NoEmploye));
        //}

        #endregion MethodesEnseignant
        
    }
}
