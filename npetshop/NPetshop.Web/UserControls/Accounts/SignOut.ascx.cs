namespace NPetshop.Web.UserControls.Accounts
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using NPetshop.Presentation.Core;
	using NPetshop.Presentation.UserActions;

	/// <summary>
	/// Description r�sum�e de SigneOut.
	/// </summary>
	public class SignOut : NPetshop.Presentation.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton LinkButtonSignIn;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Placer ici le code utilisateur pour initialiser la page
		}

		private void LinkButtonSignIn_Click(object sender, System.EventArgs e)
		{
			AccountAction action = new AccountAction(Context);
			action.SignIn();
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
			this.LinkButtonSignIn.Click += new System.EventHandler(this.LinkButtonSignIn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
