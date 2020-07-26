using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Datos;
using Capa_Datos.Entidad;

namespace Laboratorio6
{
    public partial class GridViewAutos : System.Web.UI.Page
    {
        GestionAutos objGestion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargaCarro();
            }

        }
        void Mensaje(string txtMensaje, bool Tipo)
        {
            if (Tipo)
            {
                lblExito.Text = txtMensaje;
                lblError.Text = "";
            }
            else
            {
                lblExito.Text = "";
                lblError.Text = txtMensaje;
            }
        }

        void cargaCarro()
        {
            DataTable datosCarro = new DataTable();
            objGestion = new GestionAutos();
            datosCarro = objGestion.cargaCarro();

            if (datosCarro.Rows.Count > 0)
            {
                gridAutos.DataSource = datosCarro;
                gridAutos.DataBind();
            }
            else
            {
                datosCarro.Rows.Add(datosCarro.NewRow());
                gridAutos.DataSource = datosCarro;
                gridAutos.DataBind();
                gridAutos.Rows[0].Cells.Clear();
                gridAutos.Rows[0].Cells.Add(new TableCell());
                gridAutos.Rows[0].Cells[0].ColumnSpan = datosCarro.Columns.Count;
                gridAutos.Rows[0].Cells[0].Text = "No hay datos que mostrar.";
                gridAutos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        protected void gridAutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                objGestion = new GestionAutos();
                Info_Carro objCarro = new Info_Carro();
                objCarro.IdCarro = Convert.ToInt32((gridAutos.FooterRow.FindControl("txtIdPie") as TextBox).Text.Trim());
                objCarro.Marca = (gridAutos.FooterRow.FindControl("txtMarcaPie") as TextBox).Text.Trim();
                objCarro.Modelo = (gridAutos.FooterRow.FindControl("txtModeloPie") as TextBox).Text.Trim();
                objCarro.Pais= (gridAutos.FooterRow.FindControl("txtPaisPie") as TextBox).Text.Trim();
                objCarro.Costo = Convert.ToDouble((gridAutos.FooterRow.FindControl("txtCostoPie") as TextBox).Text.Trim());
                int resultado = objGestion.registrarCarro(objCarro);

                if (resultado == 1)
                {
                    cargaCarro();
                    Mensaje("Registro con exito", true);
                }
                else
                {
                    Mensaje("Existe un error en el registro del carro", false);

                }

            }
        }

        protected void gridAutos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridAutos.EditIndex = e.NewEditIndex;
            cargaCarro();
        }

        protected void gridAutos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridAutos.EditIndex = -1;
            cargaCarro();
        }

        protected void gridAutos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            objGestion = new GestionAutos();
            Info_Carro objCarro = new Info_Carro();
            objCarro.IdCarro = Convert.ToInt32((gridAutos.Rows[e.RowIndex].FindControl("txtId") as TextBox).Text.Trim());
            objCarro.Marca = (gridAutos.Rows[e.RowIndex].FindControl("txtMarca") as TextBox).Text.Trim();
            objCarro.Modelo = (gridAutos.Rows[e.RowIndex].FindControl("txtModelo") as TextBox).Text.Trim();
            objCarro.Pais = (gridAutos.Rows[e.RowIndex].FindControl("txtPais") as TextBox).Text.Trim();
            objCarro.Costo = Convert.ToDouble((gridAutos.Rows[e.RowIndex].FindControl("txtCosto") as TextBox).Text.Trim());
            int resultado = objGestion.actualizarCarro(objCarro);
            gridAutos.EditIndex = -1;


            if (resultado == 1)
            {
                cargaCarro();
                Mensaje("Actualización con exito", true);
            }
            else
            {
                Mensaje("Existe un error en el registro del carro", false);

            }
        }
        protected void gridAutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            objGestion = new GestionAutos();
            Info_Carro objCarro = new Info_Carro();
            objCarro.IdCarro = Convert.ToInt32(gridAutos.DataKeys[e.RowIndex].Value.ToString());
            objGestion.eliminarCarro(objCarro);
            gridAutos.EditIndex = -1;
            cargaCarro();

            Mensaje("Elimino con exito", true);
        }
    }
}