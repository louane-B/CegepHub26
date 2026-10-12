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
        public void CreerCegep(CegepDTO cegep)
         {
            CegepDTO cegepBD = ObtenirCegep(cegep.Nom);

            if(cegepBD != null)
            {
                throw new Exception("Erreur - Ce cégep existe déjà.");
            }

            cegepRepository.Instance.AjouterCegep(cegep);
                
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
            if (cegepDTOBD != null)
            {
                if (cegepDTO.Nom.Equals("Cégep Rivière-du-Loup"))
                {
                    if (cegepDTO.Adresse != cegepBD.Adresse ||
                        cegepDTO.Ville != cegepBD.Ville ||
                        cegepDTO.Province != cegepBD.Province ||
                        cegepDTO.CodePostal != cegepBD.CodePostal ||
                        cegepDTO.Telephone != cegepBD.Telephone ||
                        cegepDTO.Courriel != cegepBD.Courriel)
                    {
                        cegepRepository.Instance.ModifierCegep(cegepDTO);
                    }
                    else
                        throw new Exception("Erreur - Veuiller modifier au moins une valeur.");
                }
                else
                    throw new Exception("Erreur - Vous devez utiliser le Cégep Rivière-du-Loup...");
            }
        }

        /// <summary>
        /// Méthode de service permettant de supprimer un cegep
        /// </summary>
        /// <param name="cegepDTO">Le DTO du Cegep</param>
        /// <returns></returns>
        public void SupprimerCegep(CegepDTO cegepDTO)
        {
            CegepDTO cegepBD = ObtenirCegep(cegepDTO.Nom);

            if(cegepBD == null)
            {
                throw new Exception("Erreur - Ce cégep n'existe pas.");
            }
            
            cegepRepository.Instance.SupprimerCegep(cegepDTO);
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
        public List<DepartementDTO> ObtenirListeDepartement(string nomCegep)
        {
            CegepDTO leCegepDTO = cegepRepository.Instance.ObtenirCegep(nomCegep);
            Cegep leCegep = new Cegep(leCegepDTO.Nom, leCegepDTO.Adresse, leCegepDTO.Ville, leCegepDTO.Province, leCegepDTO.CodePostal, leCegepDTO.Telephone, leCegepDTO.Courriel);

            List<DepartementDTO> departementDTOs = departementRepository.Instance.ObtenirListeDepartement(nomCegep);
            foreach(DepartementDTO departement in departementDTOs)
            {
                leCegep.AjouterDepartement(new Departement(departement.No, departement.Nom, departement.Description));
            }

            List<DepartementDTO> listeRetourDepartementDTO = new List<DepartementDTO>();
            foreach(var departement in leCegep.ObtenirListeDepartement())
            {
                listeRetourDepartementDTO.Add(new DepartementDTO(departement));
            }

            return listeRetourDepartementDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        public DepartementDTO ObtenirDepartement(string nomCegep, string nomDepartement)
        {
            DepartementDTO dto = departementRepository.Instance.ObtenirDepartement(nomCegep, nomDepartement);
            if(dto == null)
            {
                return null;
            }

            Departement departement = new Departement(dto.No, dto.Nom, dto.Description);

            return new DepartementDTO(departement);
        }

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
        public void SupprimerDepartement(CegepDTO cegep,DepartementDTO departement)
        {
            CegepDTO cegepDTO = cegepRepository.Instance.ObtenirCegep(cegep.Nom);
            if (cegepDTO == null)
                throw new Exception("Erreur - Le cégep n'existe pas.");

            Cegep cegepModele = new Cegep(cegepDTO.Nom, cegepDTO.Adresse, cegepDTO.Ville, cegepDTO.Province, cegepDTO.CodePostal, cegepDTO.Telephone, cegepDTO.Courriel);

            List<DepartementDTO> listeDepartement = departementRepository.Instance.ObtenirListeDepartement(cegep.Nom);
            foreach(DepartementDTO departementItem in listeDepartement)
            {
                cegepModele.AjouterDepartement(new Departement(departementItem.No, departementItem.Nom, departementItem.Description));
            }

            if (cegepModele.EnleverDepartement(new Departement(departement.No, departement.Nom, departement.Description)) == false)
            {
                throw new Exception("Erreur - Le département n'existe pas dans ce cégep.");
            }
            departementRepository.Instance.SupprimerDepartement(cegepDTO.Nom, departement);
        }

        #endregion MethodesDepartement

        #region MethodesEnseignant

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        public List<EnseignantDTO> ObtenirListeEnseignant(string nomCegep, string nomDepartement)
        {
            DepartementDTO leDepartementDTO = departementRepository.Instance.ObtenirDepartement(nomCegep, nomDepartement);
            Departement leDepartement = new Departement(leDepartementDTO.No, leDepartementDTO.Nom, leDepartementDTO.Description);

            List<EnseignantDTO> enseignantDTOs = enseignantRepository.Instance.ObtenirListeEnseignant(nomCegep,nomDepartement);
            foreach (EnseignantDTO enseignant in enseignantDTOs)
            {
                leDepartement.AjouterEnseignant(new Enseignant(enseignant.NoEmploye, enseignant.Prenom, enseignant.Nom, enseignant.Adresse, enseignant.Ville, enseignant.Province, enseignant.CodePostal, enseignant.Telephone, enseignant.Courriel));
            }

            List<EnseignantDTO> listeRetourEnseignantDTO = new List<EnseignantDTO>();
            foreach (var enseignant in leDepartement.ObtenirListeEnseignant())
            {
                listeRetourEnseignantDTO.Add(new EnseignantDTO(enseignant));
            }

            return listeRetourEnseignantDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public EnseignantDTO ObtenirEnseignant(string nomCegep, DepartementDTO departement, EnseignantDTO enseignant)
        {
            EnseignantDTO enseignantDTO = enseignantRepository.Instance.ObtenirEnseignant(nomCegep, departement.Nom, enseignant.NoEmploye);
            if (enseignantDTO == null)
            {
                return null;
            }

            Enseignant enseignantModele = new Enseignant(enseignantDTO.NoEmploye, enseignantDTO.Prenom, enseignantDTO.Nom, enseignantDTO.Adresse, enseignantDTO.Ville, enseignantDTO.Province, enseignantDTO.CodePostal, enseignantDTO.Telephone, enseignantDTO.Courriel);

            return new EnseignantDTO(enseignantModele);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public void AjouterEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
        {
            DepartementDTO departementDTO = ObtenirDepartement("Cégep Rivière-du-Loup", departement.Nom);
            Departement departementModele = new Departement(departementDTO.No, departementDTO.Nom, departementDTO.Description);
            List<EnseignantDTO> listeEnseignant = enseignantRepository.Instance.ObtenirListeEnseignant("Cégep Rivière-du-Loup", departement.Nom);

            foreach (EnseignantDTO enseignantDTO in listeEnseignant)
            {
                departementModele.AjouterEnseignant(new Enseignant(enseignantDTO.NoEmploye, enseignantDTO.Prenom, enseignantDTO.Nom, enseignantDTO.Adresse, enseignantDTO.Ville, enseignantDTO.Province, enseignantDTO.CodePostal, enseignantDTO.Telephone, enseignantDTO.Courriel));
            }

            departementModele.AjouterEnseignant(new Enseignant(enseignant.NoEmploye, enseignant.Prenom, enseignant.Nom, enseignant.Adresse, enseignant.Ville, enseignant.Province, enseignant.CodePostal, enseignant.Telephone, enseignant.Courriel));

            enseignantRepository.Instance.AjouterEnseignant("Cégep Rivière-du-Loup", departement.Nom, enseignant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public void ModifierEnseignant(CegepDTO cegepDTO,DepartementDTO departement, EnseignantDTO enseignant)
        {
            EnseignantDTO enseignantDTOBD = ObtenirEnseignant(cegepDTO.Nom, departement, enseignant);
            Enseignant enseignantBD = new Enseignant(enseignantDTOBD.NoEmploye, enseignantDTOBD.Prenom, enseignantDTOBD.Nom, enseignantDTOBD.Adresse, enseignantDTOBD.Ville, enseignantDTOBD.Province, enseignantDTOBD.CodePostal, enseignantDTOBD.Telephone, enseignantDTOBD.Courriel);
            if (enseignantDTOBD != null)
            {
                if (enseignant.NoEmploye.Equals(enseignantBD.NoEmploye))
                {
                    if (enseignant.Nom != enseignantBD.Nom ||
                        enseignant.Prenom != enseignantBD.Prenom ||
                        enseignant.Adresse != enseignantBD.Adresse ||
                        enseignant.Ville != enseignantBD.Ville ||
                        enseignant.Province != enseignantBD.Province ||
                        enseignant.CodePostal != enseignantBD.CodePostal ||
                        enseignant.Telephone != enseignantBD.Telephone ||
                        enseignant.Courriel != enseignantBD.Courriel)
                    {
                        enseignantRepository.Instance.ModifierEnseignant(cegepDTO.Nom, departement.Nom, enseignant);
                    }
                    else
                        throw new Exception("Erreur - Veuiller modifier au moins une valeur.");
                }
                else
                    throw new Exception("Erreur - Vous devez utiliser le Cégep Rivière-du-Loup...");
            }
        }

        /// <summary>
        /// Méthode de service permettant de supprimer un enseignant
        /// </summary>
        /// <param name="departement">Le DTO du département de l'enseignant</param>
        /// <param name="enseignant">Le DTO de l'enseignant a supprimer.</param>
        public void SupprimerEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
        {
            DepartementDTO departementDTO = departementRepository.Instance.ObtenirDepartement("Cégep Rivière-du-Loup", departement.Nom);
            Departement departementModele = new Departement(departementDTO.No, departementDTO.Nom, departementDTO.Description);

            List<EnseignantDTO> listeEnseignant = enseignantRepository.Instance.ObtenirListeEnseignant("Cégep Rivière-du-Loup", departement.Nom);
            foreach (EnseignantDTO enseignantItem in listeEnseignant)
            {
                departementModele.AjouterEnseignant(new Enseignant(enseignantItem.NoEmploye, enseignantItem.Nom, enseignantItem.Prenom, enseignantItem.Adresse, enseignantItem.Ville, enseignantItem.Province, enseignantItem.CodePostal, enseignantItem.Telephone, enseignantItem.Courriel));
            }

            if (departementModele.EnleverEnseignant(new Enseignant(unNoEmploye: enseignant.NoEmploye)) == false)
            {
                throw new Exception("Erreur - Le département n'existe pas dans ce cégep.");
            }
            enseignantRepository.Instance.SupprimerEnseignant("Cégep Rivière-du-Loup", departement.Nom, enseignant);
        }

        #endregion MethodesEnseignant

}
}
