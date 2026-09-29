using ProjetCegep.Modeles;

/// <summary>
/// 
/// </summary>
namespace ProjetCegep.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class EnseignantDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int NoEmploye { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Prenom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Adresse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Ville { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CodePostal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Courriel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DateEmbauche { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DateArret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="adresse"></param>
        /// <param name="ville"></param>
        /// <param name="province"></param>
        /// <param name="codePostal"></param>
        /// <param name="telephone"></param>
        /// <param name="courriel"></param>
        /// <param name="dateEmbauche"></param>
        /// <param name="dateArret"></param>
        public EnseignantDTO(int no=0000000, string nom="", string prenom="", string adresse="", string ville="", string province="", string codePostal="", string telephone="", string courriel="", string dateEmbauche="", string dateArret="")
        {
            NoEmploye = no;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Ville = ville;
            Province = province;
            CodePostal = codePostal;
            Telephone = telephone;
            Courriel = courriel;
            DateEmbauche = dateEmbauche;
            DateArret = dateArret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lenseignant"></param>
        public EnseignantDTO(Enseignant lenseignant)
        {
            NoEmploye = lenseignant.NoEmploye;
            Nom = lenseignant.Nom;
            Prenom = lenseignant.Prenom;
            Adresse = lenseignant.Adresse;
            Ville = lenseignant.Ville;
            Province = lenseignant.Province;
            CodePostal = lenseignant.CodePostal;
            Telephone = lenseignant.Telephone;
            Courriel = lenseignant.Courriel;
        }
    }
}
