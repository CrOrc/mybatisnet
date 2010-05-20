namespace NPetshop.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

		using NPetshop.Presentation.UserActions;

	/// <summary>
	/// Description r�sum�e de TopBar.
	/// </summary>
	public class TopBar : NPetshop.Presentation.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton LinkbuttonFish;
		protected System.Web.UI.WebControls.LinkButton LinkbuttonDogs;
		protected System.Web.UI.WebControls.LinkButton LinkbuttonReptiles;
		protected System.Web.UI.WebControls.LinkButton LinkbuttonCats;
		protected System.Web.UI.WebControls.LinkButton LinkbuttonBirds;
		protected System.Web.UI.HtmlControls.HtmlImage Img1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Placer ici le code utilisateur pour initialiser la page
		}

		public void LinkButton_Command(object sender, CommandEventArgs e)
		{
			CatalogAction action = new CatalogAction(Context);
			action.ShowProductsByCategory(e.CommandArgument.ToString());
			this.CurrentController.NextView = action.NextViewToDisplay;
		}

		#region Code g�n�r� par le Concepteur Web Form
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN�: Cet appel est requis par le Concepteur Web Form ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		M�thode requise pour la prise en charge du concepteur - ne modifiez pas
		///		le contenu de cette m�thode avec l'�diteur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
