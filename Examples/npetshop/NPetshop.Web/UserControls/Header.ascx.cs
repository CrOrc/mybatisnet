namespace NPetshop.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Description r�sum�e de Header.
	/// </summary>
	public class Header : System.Web.UI.UserControl
	{
		protected System.Web.UI.HtmlControls.HtmlImage space0;
		protected System.Web.UI.HtmlControls.HtmlImage space1;
		protected System.Web.UI.HtmlControls.HtmlImage title;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Placer ici le code utilisateur pour initialiser la page
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
