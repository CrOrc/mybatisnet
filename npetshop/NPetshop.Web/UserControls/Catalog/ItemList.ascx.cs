namespace NPetshop.Web.UserControls.Catalog
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using IBatisNet.Common.Pagination;
	using NPetshop.Domain.Catalog;
	using NPetshop.Presentation.Core;
	using NPetshop.Presentation.UserActions;

	/// <summary>
	/// Description r�sum�e de Product.
	/// </summary>
	public class ItemList : NPetshop.Presentation.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton LinkbuttonPrev;
		protected System.Web.UI.WebControls.LinkButton LinkbuttonNext;
		protected System.Web.UI.WebControls.Repeater RepeaterItems;
		protected System.Web.UI.WebControls.Label LabelProduct;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Placer ici le code utilisateur pour initialiser la page
		}

		public override void DataBind()
		{
			NPetshop.Domain.Catalog.Product product = this.WebLocalSingleton.CurrentAction.Data[DataViews.PRODUCT] as NPetshop.Domain.Catalog.Product; 
			IPaginatedList itemList = this.WebLocalSingleton.CurrentList as IPaginatedList; 

			LabelProduct.Text = product.Name;
			RepeaterItems.DataSource = itemList; 
			RepeaterItems.DataBind();

			if (itemList.IsNextPageAvailable)
			{
				LinkbuttonNext.Visible = true;
				LinkbuttonNext.CommandArgument = product.Id;
			}
			else
			{
				LinkbuttonNext.Visible = false;
			}
			if (itemList.IsPreviousPageAvailable)
			{
				LinkbuttonPrev.Visible = true;
				LinkbuttonPrev.CommandArgument = product.Id;
			}
			else
			{
				LinkbuttonPrev.Visible = false;
			}
		}

		protected void RepeaterItems_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "ShowItem")
			{
				CatalogAction action = new CatalogAction(Context);
				action.ShowItem(e.CommandArgument.ToString());
				this.CurrentController.NextView = action.NextViewToDisplay;
			}
			else if (e.CommandName == "AddToCart")
			{
				ShoppinAction action = new ShoppinAction(Context);
				action.AddItemToCart(e.CommandArgument.ToString());
				this.CurrentController.NextView = action.NextViewToDisplay;
			}
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
