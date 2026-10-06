using System.IO;
using System.Xml.Serialization;
using ProjetCegep.Modeles;
using ProjetCegep.DTOs;
using System.Collections.Generic;

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
        private Cegep monCegep;

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
            monCegep = null;
        }

        #endregion Contructeurs

        #region MethodesCegep

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cegep"></param>
        /// <returns></returns>
        public bool CreerCegep(CegepDTO cegep)
        {
            monCegep = new Cegep(cegep.Nom, cegep.Adresse, cegep.Ville, cegep.Province, cegep.CodePostal, cegep.Telephone, cegep.Courriel);
            return monCegep != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cegep"></param>
        /// <returns></returns>
        public bool ModifierCegep(CegepDTO cegep)
        {
            if (monCegep.Nom.Equals(cegep.Nom))
                if (monCegep.Adresse != cegep.Adresse ||
                    monCegep.Ville != cegep.Ville ||
                    monCegep.Province != cegep.Province ||
                    monCegep.CodePostal != cegep.CodePostal ||
                    monCegep.Telephone != cegep.Telephone ||
                    monCegep.CodePostal != cegep.CodePostal)
                {
                    monCegep.Adresse = cegep.Adresse;
                    monCegep.Ville = cegep.Ville;
                    monCegep.Province = cegep.Province;
                    monCegep.CodePostal = cegep.CodePostal;
                    monCegep.Telephone = cegep.Telephone;
                    monCegep.CodePostal = cegep.CodePostal;
                    return true;
                }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SupprimerCegep()
        {
            monCegep = null;
            return monCegep == null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CegepDTO ObtenirCegep()
        {
            if (monCegep != null)
                return new CegepDTO(monCegep);
            return null;
        }

        #endregion MethodesCegep

        #region MethodesDepartement

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DepartementDTO> ObtenirListeDepartement()
        {
            List<DepartementDTO> liste = new List<DepartementDTO>();
            foreach (Departement departement in monCegep.ObtenirListeDepartement())
            {
                liste.Add(new DepartementDTO(departement));
            }
            return liste;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        public DepartementDTO ObtenirDepartement(DepartementDTO departement)
        {
            return new DepartementDTO(monCegep.ObtenirDepartement(new Departement(departement.No, departement.Nom, departement.Description)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        public bool AjouterDepartement(DepartementDTO departement)
        {
            return monCegep.AjouterDepartement(new Departement(departement.No, departement.Nom, departement.Description));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        public bool SupprimerDepartement(DepartementDTO departement)
        {
            return monCegep.EnleverDepartement(new Departement(unNom: departement.Nom));
        }

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
            Departement leDepartement = monCegep.ObtenirDepartement(new Departement(unNom:departement.Nom));
            foreach (Enseignant enseignant in leDepartement.ObtenirListeEnseignant())
            {
                liste.Add(new EnseignantDTO(enseignant));
            }
            return liste;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public EnseignantDTO ObtenirEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
        {
            return new EnseignantDTO(monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).ObtenirEnseignant(new Enseignant(unNoEmploye:enseignant.NoEmploye)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public bool AjouterEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
        {
            return monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).AjouterEnseignant(new Enseignant(enseignant.NoEmploye, enseignant.Nom, enseignant.Prenom, enseignant.Adresse, enseignant.Ville, enseignant.Province, enseignant.CodePostal, enseignant.Telephone, enseignant.Courriel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public bool ModifierEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
        {
            Enseignant lenseignant = monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).ObtenirEnseignant(new Enseignant(unNoEmploye:enseignant.NoEmploye));

            lenseignant.Nom = enseignant.Nom;
            lenseignant.Prenom = enseignant.Prenom;
            lenseignant.Adresse = enseignant.Adresse;
            lenseignant.Ville = enseignant.Ville;
            lenseignant.Province = enseignant.Province;
            lenseignant.CodePostal = enseignant.CodePostal;
            lenseignant.Telephone = enseignant.Telephone;
            lenseignant.Courriel = enseignant.Courriel;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public bool SupprimerEnseignant(DepartementDTO departement, EnseignantDTO enseignant)
        {
            return monCegep.ObtenirDepartement(new Departement(unNom: departement.Nom)).EnleverEnseignant(new Enseignant(enseignant.NoEmploye));
        }

        #endregion MethodesEnseignant

        #region MethodesSerialisationXML
        
        /// <summary>
        /// 
        /// </summary>
        public void ChargerDonneesFichier()
        {
            if (File.Exists("Cegep.xml"))
            {
                XmlSerializer leFichierCegep = new XmlSerializer(typeof(Cegep));
                FileStream fichierLogique;

                fichierLogique = File.OpenRead("Cegep.xml");
                monCegep = (Cegep)leFichierCegep.Deserialize(fichierLogique);
                fichierLogique.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SauvegarderDonneesFichier()
        {
            if (File.Exists("Cegep.xml"))
            {
                File.Delete("Cegep.xml");
            }
            XmlSerializer leFichierCegep = new XmlSerializer(typeof(Cegep));
            FileStream fichierLogique;

            using (fichierLogique = File.OpenWrite("Cegep.xml"))
            {
                leFichierCegep.Serialize(fichierLogique, monCegep);
            }
        }
        #endregion MethodesSerialisationXML
    }
}
