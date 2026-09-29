using ProjetCegep.Modeles;
/// <summary>
/// 
/// </summary>
namespace ProjetCegep.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class CegepDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Nom { get; set; }
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
        /// <param name="nom"></param>
        /// <param name="adresse"></param>
        /// <param name="ville"></param>
        /// <param name="province"></param>
        /// <param name="codePostal"></param>
        /// <param name="telephone"></param>
        /// <param name="courriel"></param>
        public CegepDTO(string nom="", string adresse="", string ville="", string province="", string codePostal="", string telephone="", string courriel="")
        {
            Nom = nom;
            Adresse = adresse;
            Ville = ville;
            Province = province;
            CodePostal = codePostal;
            Telephone = telephone;
            Courriel = courriel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leCegep"></param>
        public CegepDTO(Cegep leCegep)
        {
            Nom = leCegep.Nom;
            Adresse = leCegep.Adresse;
            Ville = leCegep.Ville;
            Province = leCegep.Province;
            CodePostal = leCegep.CodePostal;
            Telephone = leCegep.Telephone;
            Courriel = leCegep.Courriel;
        }
    }
}
