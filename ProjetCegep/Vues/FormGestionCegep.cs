using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

/// <summary>
/// Namespace pour les classes de type Vue.
/// </summary>
namespace ProjetCegep.Vues
{
    /// <summary>
    /// Classe représentant la vue de l'application.
    /// </summary>
   public partial class FormGestionCegep : Form
   {
        #region Constructeur

        /// <summary>
        /// Constructeur du formulaire Gestion Cégep.
        /// </summary>
        public FormGestionCegep()
        {
            InitializeComponent();
            RemplirListes();
        }

        #endregion Constructeur

        #region OngletEnseignants

        /// <summary>
        /// Méthode de service permettant de mettre à jour les différentes listes d'enseignants.
        /// </summary>
        /// <param name="unDepartement">Le département qui à été sélectionné.</param>
        public void AfficherListeEnseignantGestionEnseignant(DepartementDTO unDepartement)
        {
            lbxEnseignantsSaisie.Items.Clear();
            try
            {
                foreach (EnseignantDTO enseignant in CegepControleur.Instance.ObtenirListeEnseignant("Cégep Rivière-du-Loup", unDepartement.Nom))
                {
                    lbxEnseignantsSaisie.Items.Add(enseignant.NoEmploye + "  " + enseignant.Prenom + " " + enseignant.Nom);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Méthode de service permettant d'ajouter un enseignant dans un département qui a été sélectionné dans la liste.
        /// </summary>
        /// <param name="sender">Objet d'envoi.</param>
        /// <param name="e">Paramètres d'envoi.</param>
        private void BtnAjouterEnseignant_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbxDepartementEnseignant.Text == "")
                {
                    MessageBox.Show("Veuiller sélectionner un departement");
                    return;
                }

                DepartementDTO monDepartement = CegepControleur.Instance.ObtenirDepartement("Cégep Rivière-du-Loup", cbxDepartementEnseignant.Text);

                EnseignantDTO nouvelEnseignant = new EnseignantDTO(int.Parse(edtNoEmploye.Text), edtNomEnseignant.Text, edtPrenomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, EdtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text);

                CegepControleur.Instance.AjouterEnseignant(monDepartement, nouvelEnseignant);

                MessageBox.Show("L'enseignant " + edtNoEmploye.Text + " a été ajouté !!!");
                AfficherListeEnseignantGestionEnseignant(monDepartement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Méthode qui permet d'afficher la liste des enseignants après avoir sélectionner un département dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxDepartementEnseignant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nomDepartement = cbxDepartementEnseignant.Text;
                if (string.IsNullOrWhiteSpace(nomDepartement))
                    return;

                DepartementDTO monDepartement = CegepControleur.Instance.ObtenirDepartement("Cégep Rivière-du-Loup", nomDepartement);

                AfficherListeEnseignantGestionEnseignant(monDepartement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Méthode qui permet de modifier un enseignant déjà dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifierEnseignant_Click(object sender, EventArgs e)
        {
            try
            {
                string nomDepartement = cbxDepartementEnseignant.Text;
                if (string.IsNullOrWhiteSpace(nomDepartement))
                {
                    MessageBox.Show("Veuillez sélectionner un département.");
                    return;
                }

                DepartementDTO monDepartement = CegepControleur.Instance.ObtenirDepartement("Cégep Rivière-du-Loup", nomDepartement);

                EnseignantDTO enseigantOriginal = CegepControleur.Instance.ObtenirEnseignant("Cégep Rivière-du-Loup", monDepartement, new EnseignantDTO(no: int.Parse(edtNoEmploye.Text)));

                EnseignantDTO enseignantModifie = new EnseignantDTO(int.Parse(edtNoEmploye.Text), edtPrenomEnseignant.Text, edtNomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, EdtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text);

                CegepDTO cegepDTO = CegepControleur.Instance.ObtenirCegep("Cégep Rivière-du-Loup");

                CegepControleur.Instance.ModifierEnseignant(cegepDTO, monDepartement, enseignantModifie);

                MessageBox.Show("L'enseignant a été modifié avec succès.");
                AfficherListeEnseignantGestionEnseignant(monDepartement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Méthode pour supprimer un enseignant de la liste de sont département
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimerEnseignant_Click(object sender, EventArgs e)
        {
            try
            {
                string nomDepartement = cbxDepartementEnseignant.Text;
                if (string.IsNullOrWhiteSpace(nomDepartement))
                {
                    MessageBox.Show("Veuillez sélectionner un département.");
                    return;
                }

                DepartementDTO monDepartement = CegepControleur.Instance.ObtenirDepartement("Cégep Rivière-du-Loup", nomDepartement);

                EnseignantDTO enseignantDTO = new EnseignantDTO(no: int.Parse(edtNoEmploye.Text));

                EnseignantDTO enseignantTrouve = CegepControleur.Instance.ObtenirEnseignant("Cégep Rivière-du-Loup", monDepartement, enseignantDTO);

                CegepControleur.Instance.SupprimerEnseignant(monDepartement, enseignantDTO);

                MessageBox.Show("L'enseignant " + edtNoEmploye.Text + " a été supprimé !!!");
                AfficherListeEnseignantGestionEnseignant(monDepartement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion OngletEnseignants

        #region OngletDepartements

        /// <summary>
        /// Méthode qui permet d'ajouter un départemetn à la liste des départements du cégep
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouterDepartement_Click(object sender, EventArgs e)
        {
            try
            { 

                DepartementDTO unDepartement = new DepartementDTO(edtNoDepartement.Text, edtNomDepartement.Text, edtDescriptionDepartement.Text);
                CegepControleur.Instance.AjouterDepartement(unDepartement, "Cégep Rivière-du-Loup");

                MessageBox.Show(unDepartement.Nom + "\n a bien été crée.");
                RemplirListes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le département existe déjà et n'a pas été crée." + ex.Message);
            }
            Refresh();
        }

        /// <summary>
        /// Métode qui permet de supprimer un département de la liste des département du cégep
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimerGestionDepartement_Click(object sender, EventArgs e)
        {
            try
            {
                CegepDTO cegepDTO = CegepControleur.Instance.ObtenirCegep("Cégep Rivière-du-Loup");
                DepartementDTO departementDTO = new DepartementDTO(edtNoDepartement.Text, edtNomDepartement.Text, edtDescriptionDepartement.Text);

                CegepControleur.Instance.SupprimerDepartement(cegepDTO, departementDTO);

                MessageBox.Show(departementDTO.Nom + "\n a bien été enlevé.");
                RemplirListes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le département entré n'est pas dans la liste et n'a pas pu être enlevé." + ex.Message);
            }
            Refresh();
        }

        #endregion OngletDepartements

        #region OngletCegep

        /// <summary>
        /// Méthode qui permet de créer un cégep avec le constructeur paramétré.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouterCegep_Click(object sender, EventArgs e)
        {
            try
            {
                CegepControleur.Instance.CreerCegep(new CegepDTO(edtNomCegep.Text, edtAdresseCegep.Text, edtVilleCegep.Text, edtProvinceCegep.Text, edtCodePostalCegep.Text, edtTelephoneCegep.Text, edtCourrielCegep.Text));
                MessageBox.Show(edtNomCegep.Text + "\n a bien été crée.");
                RemplirListes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Méthode qui permet de modifier les informations du cégep
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifierCegep_Click(object sender, EventArgs e)
        {
            try
            {
                CegepControleur.Instance.ModifierCegep(new CegepDTO(edtNomCegep.Text, edtAdresseCegep.Text, edtVilleCegep.Text, edtProvinceCegep.Text, edtCodePostalCegep.Text, edtTelephoneCegep.Text, edtCourrielCegep.Text));
                MessageBox.Show(edtNomCegep.Text + "\n a bien été modifié.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Méthode qui permet de suprimmer l'objet cégep
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimerCegep_Click(object sender, EventArgs e)
        {
            try
            {
                CegepDTO cegepDTO = CegepControleur.Instance.ObtenirCegep("Cégep Rivière-du-Loup");
                string nomCegep = cegepDTO.Nom;
                CegepControleur.Instance.SupprimerCegep(cegepDTO);

                MessageBox.Show(nomCegep + " a bien été supprimé.");
                RemplirListes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion OngletCegep

        #region Utilitaires

        /// <summary>
        /// Permet de remplir les différentes listes du formulaire 
        /// </summary>
        public void RemplirListes()
        {
            lbxDepartement.DataSource = null;
            lbxDepartementInfoCegep.DataSource = null;
            cbxDepartementEnseignant.DataSource = null;

            try
            {
                List<DepartementDTO> liste = CegepControleur.Instance.ObtenirListeDepartement("Cégep Rivière-du-Loup");


                lbxDepartement.DataSource = liste;
                lbxDepartement.DisplayMember = "Nom";

                lbxDepartementInfoCegep.DataSource = liste;
                lbxDepartementInfoCegep.DisplayMember = "Nom";

                cbxDepartementEnseignant.DataSource = liste;
                cbxDepartementEnseignant.DisplayMember = "Nom";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGestionCegep_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuitterToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Méthode qui permet d'enregistrer le cégep et les listes dans un fichier XML.  Par la suite, on quitte l'application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion Utilitaires
    }
}
